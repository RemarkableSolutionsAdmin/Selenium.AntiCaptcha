using System.Text.RegularExpressions;
using AntiCaptchaApi.Net;
using AntiCaptchaApi.Net.Enums;
using AntiCaptchaApi.Net.Models;
using AntiCaptchaApi.Net.Models.Solutions;
using AntiCaptchaApi.Net.Requests.Abstractions;
using AntiCaptchaApi.Net.Responses;
using Newtonsoft.Json.Linq;
using OpenQA.Selenium;
using Selenium.AntiCaptcha.Internal.Extensions;
using Selenium.AntiCaptcha.Internal.Helpers;
using Selenium.AntiCaptcha.Models;

namespace Selenium.AntiCaptcha.Solvers.Base;

internal abstract class Solver<TRequest, TSolution> : ISolver <TSolution>
    where TRequest: CaptchaRequest<TSolution>
    where TSolution: BaseSolution, new()
{
    private const int WaitingStepTime = 500;
    
    protected virtual string GetSiteKey(IWebDriver driver)
    {   
        var pageSource = driver.GetAllPageSource();

        var patterns = new List<string>
        {
            "sitekey(?:&quot;:&quot;|\".*:.*\"|=[^\\D]+?){1}([\\w\\d-]+)",
            "gt=(.*?)&",
            "captcha_id=(.*?)&",
        };

        var result = pageSource.GetFirstRegexThatFits(true, patterns.ToArray());
        return result != null ? result.Groups[1].Value : string.Empty;
    }

    protected async Task<string> AcquireSiteKey(IWebDriver driver, int maxPageLoadWaitingTimeInMs)
    {
        var timePassedInMs = 0;
        while (true)
        {
            var result = GetSiteKey(driver);

            if (!string.IsNullOrEmpty(result) || timePassedInMs >= maxPageLoadWaitingTimeInMs) 
                return result;

            await Task.Delay(WaitingStepTime);
            timePassedInMs += WaitingStepTime;
        }
    }


    protected abstract TRequest BuildRequest(SolverAdditionalArguments additionalArguments);

    protected virtual void FillResponseElement(IWebDriver driver, TSolution solution, IWebElement? responseElement)
    {
        
    }

    protected virtual async Task<SolverAdditionalArguments> FillMissingAdditionalArguments(IWebDriver driver, SolverAdditionalArguments solverAdditionalArguments)
    {
        return solverAdditionalArguments with
        {
            Url = solverAdditionalArguments.Url ?? driver.Url,
            SiteKey = string.IsNullOrEmpty(solverAdditionalArguments.SiteKey) ?
                await AcquireSiteKey(driver, solverAdditionalArguments.MaxPageLoadWaitingTimeInMilliseconds) : null,
            UserAgent = solverAdditionalArguments.UserAgent ?? Constants.AnticaptchaDefaultValues.UserAgent
        };
    }
    
    public async Task<TaskResultResponse<TSolution>> SolveAsync(IWebDriver driver, string clientKey, SolverAdditionalArguments additionalArguments)
    {
        var client = new AnticaptchaClient(clientKey);
        additionalArguments = await FillMissingAdditionalArguments(driver, additionalArguments);
        var request = BuildRequest(additionalArguments);
        var result = await client.SolveCaptchaAsync(request, maxSeconds: additionalArguments.MaxTimeOutTimeInSeconds);

        if (result.Status == TaskStatusType.Ready && result.Solution.IsValid())
        {
            var cookies = (result.Solution as AntiGateSolution)?.Cookies; //TODO: Should add interface to Solution which returns Cookies.
            if (cookies != null)
                AddCookies(driver, cookies);

            if (additionalArguments.ResponseElement != null)
            {
                FillResponseElement(driver, result.Solution, additionalArguments.ResponseElement);
            }
            
            additionalArguments.SubmitElement?.Click();
        }

        return result;
    }

    private static void AddCookies(IWebDriver driver, JObject? cookies)
    {
        if (cookies is not { Count: > 0 }) 
            return;
        driver.Manage().Cookies.DeleteAllCookies();
        foreach (var cookie in cookies)
        {
            if (!string.IsNullOrEmpty(cookie.Key) && !string.IsNullOrEmpty(cookie.Value?.ToString()))
                driver.Manage().Cookies.AddCookie(new Cookie(cookie.Key, cookie.Value.ToString()));
        }
    }
}