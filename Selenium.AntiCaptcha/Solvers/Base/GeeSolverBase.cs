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
    protected GeeSolverBase(string clientKey, IWebDriver driver) : base(clientKey, driver)
    {
    }
    
    protected override async Task<SolverAdditionalArguments> FillMissingAdditionalArguments(
        SolverAdditionalArguments solverAdditionalArguments)
    {
        return await base.FillMissingAdditionalArguments(solverAdditionalArguments)
            with
            {
                Gt = solverAdditionalArguments.Gt ?? await AcquireGt(solverAdditionalArguments.MaxPageLoadWaitingTimeInMilliseconds),
                Challenge = solverAdditionalArguments.Challenge ?? GetChallenge(Driver)
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

    private async Task<string> AcquireGt(int maxPageLoadWaitingTimeInMs)
    {
        var timePassedInMs = 0;
        while (true)
        {
            var result = GetGt();

            if (!string.IsNullOrEmpty(result) || timePassedInMs >= maxPageLoadWaitingTimeInMs) 
                return result;

            await Task.Delay(WaitingStepTime);
            timePassedInMs += WaitingStepTime;
        }
    }
        
    private string GetChallenge(IWebDriver driver)
    {
        var regex = new Regex("challenge=(.*?)&");
        return regex.Match(driver.GetAllPageSource()).Groups[1].Value;
    }

}