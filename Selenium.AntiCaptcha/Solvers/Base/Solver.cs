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
using Selenium.AntiCaptcha.Models;

namespace Selenium.AntiCaptcha.Solvers.Base;

internal abstract class Solver<TRequest, TSolution> : ISolver <TSolution>
    where TRequest: CaptchaRequest<TSolution>
    where TSolution: BaseSolution, new()
{
    private const uint WaitingStepTime = 500;
    
    protected virtual string GetSiteKey(IWebDriver driver)
    {   
        var pageSource = driver.GetAllPageSource();
        var regex = new Regex("gt=(.*?)&");
        var result = regex.Match(pageSource).Groups[1].Value;

        if (!string.IsNullOrEmpty(result))
            return result;
        
        regex = new Regex("captcha_id=(.*?)&");
        var captchaIdRegexGroups = regex.Match(pageSource).Groups;
        result = captchaIdRegexGroups[1].Value;

        
        if (!string.IsNullOrEmpty(result))
            return result;

        regex = new Regex("sitekey=(.*?)&");
        var siteKeyCaptchaGroups = regex.Match(pageSource).Groups;
        return siteKeyCaptchaGroups[1].Value;
    }

    protected string AcquireSiteKey(IWebDriver driver, uint timePassedInMs, uint maxPageLoadWaitingTimeInMs)
    {
        var result = GetSiteKey(driver);

        if (!string.IsNullOrEmpty(result) || timePassedInMs >= maxPageLoadWaitingTimeInMs)
            return result;
        
        return AcquireSiteKey(driver, timePassedInMs + WaitingStepTime, maxPageLoadWaitingTimeInMs);
    }


    protected abstract TRequest BuildRequest(SolverAdditionalArguments additionalArguments);

    protected virtual void FillResponseElement(IWebDriver driver, TSolution solution, IWebElement? responseElement)
    {
        
    }

    protected virtual SolverAdditionalArguments FillMissingAdditionalArguments(IWebDriver driver, SolverAdditionalArguments solverAdditionalArguments)
    {
        return solverAdditionalArguments with
        {
            Url = solverAdditionalArguments.Url ?? driver.Url,
            SiteKey = solverAdditionalArguments.SiteKey ?? AcquireSiteKey(driver, 0, WaitingStepTime),
            UserAgent = solverAdditionalArguments.UserAgent ?? Constants.AnticaptchaDefaultValues.UserAgent
        };
    }
    
    public TaskResultResponse<TSolution> Solve(IWebDriver driver, string clientKey, SolverAdditionalArguments additionalArguments)
    {
        var client = new AnticaptchaClient(clientKey);
        additionalArguments = FillMissingAdditionalArguments(driver, additionalArguments);
        var request = BuildRequest(additionalArguments);
        var result = client.SolveCaptcha(request);

        if (result.Status == TaskStatusType.Ready && result.Solution.IsValid())
        {
            var jObject = (result.Solution as AntiGateSolution)?.Cookies; //Should add interface to Solution which returns Cookies.
            if (jObject != null)
                AddCookies(driver, jObject);

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