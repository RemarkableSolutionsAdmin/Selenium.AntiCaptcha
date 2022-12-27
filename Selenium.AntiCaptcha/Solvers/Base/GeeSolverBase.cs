using System.Text.RegularExpressions;
using AntiCaptchaApi.Net.Models.Solutions;
using AntiCaptchaApi.Net.Requests.Abstractions;
using OpenQA.Selenium;
using Selenium.AntiCaptcha.Internal.Extensions;
using Selenium.AntiCaptcha.Models;

namespace Selenium.AntiCaptcha.Solvers.Base;

public abstract class GeeSolverBase<TRequest, TSolution> : Solver <TRequest, TSolution>
    where TRequest: CaptchaRequest<TSolution>
    where TSolution: BaseSolution, new()
{
    protected GeeSolverBase(string clientKey, IWebDriver driver, SolverConfig solverConfig) : base(clientKey, driver, solverConfig)
    {
    }
    
    protected override async Task<SolverArguments> FillMissingAdditionalArguments(
        SolverArguments solverArguments)
    {
        return await base.FillMissingAdditionalArguments(solverArguments)
            with
            {
                Gt = solverArguments.Gt ?? await AcquireGt(),
                Challenge = solverArguments.Challenge ?? GetChallenge(Driver)
            };
    }

    private string GetGt()
    {   
        var pageSource = Driver.GetAllPageSource();

        var patterns = new List<string>
        {
            "gt=(\\w{32}?)",
            "\"gt\"\\W+\"(\\w{32})\"",
            "captcha_id=(\\w{32}?)",
            "\"captcha_id\"\\W+\"(\\w{32})\"",
        };

        var result = pageSource.GetFirstRegexThatFits(true, patterns.ToArray());
        return result != null ? result.Groups[1].Value : string.Empty;
    }

    private async Task<string> AcquireGt()
    {
        var timePassedInMs = 0;
        while (true)
        {
            var result = GetGt();

            if (!string.IsNullOrEmpty(result) || timePassedInMs >= SolverConfig.MaxPageLoadWaitingTimeInMilliseconds) 
                return result;

            await Task.Delay(SolverConfig.WaitingStepTimeInMilliseconds);
            timePassedInMs += SolverConfig.WaitingStepTimeInMilliseconds;
        }
    }
        
    private string GetChallenge(IWebDriver driver)
    {
        var regex = new Regex("challenge=(.*?)&");
        return regex.Match(driver.GetAllPageSource()).Groups[1].Value;
    }

}