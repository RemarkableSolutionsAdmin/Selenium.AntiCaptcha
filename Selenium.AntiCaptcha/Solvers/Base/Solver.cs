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

namespace Selenium.AntiCaptcha.Solvers.Base;

internal abstract class Solver<TRequest, TSolution> : ISolver <TSolution>
    where TRequest: CaptchaRequest<TSolution>
    where TSolution: BaseSolution, new()
{
    protected virtual string GetSiteKey(IWebDriver driver, int waitingTime = 1000, int tries = 3)
    {
        if (tries <= 0)
            return string.Empty;
        
        Thread.Sleep(waitingTime);
        var pageSource = driver.GetAllPageSource();
        var regex = new Regex("gt=(.*?)&");
        var gt = regex.Match(pageSource).Groups[1].Value;

        if (!string.IsNullOrEmpty(gt))
            return gt;
        
        regex = new Regex("captcha_id=(.*?)&");
        var captchaIdRegexGroups = regex.Match(pageSource).Groups;
        gt = captchaIdRegexGroups[1].Value;

        
        if (!string.IsNullOrEmpty(gt))
            return gt;

        regex = new Regex("sitekey=(.*?)&");
        var siteKeyCaptchaGroups = regex.Match(pageSource).Groups;
        gt = siteKeyCaptchaGroups[1].Value;

        if (string.IsNullOrEmpty(gt))
            GetSiteKey(driver, waitingTime, --tries);
        return gt;
    }

    protected abstract TRequest BuildRequest(IWebDriver driver, string? url, string? siteKey,
        IWebElement? imageElement, string? userAgent, ProxyConfig? proxyConfig);

    protected virtual void FillResponseElement(IWebDriver driver, TSolution solution, IWebElement? responseElement)
    {
        
    }
    public TaskResultResponse<TSolution> Solve(IWebDriver driver, string clientKey, string? url, string? siteKey, IWebElement? responseElement,
        IWebElement? submitElement, IWebElement? imageElement, string? userAgent, ProxyConfig? proxyConfig)
    {
        var client = new AnticaptchaClient(clientKey);
        siteKey ??= GetSiteKey(driver);
        var request = BuildRequest(driver, url, siteKey, imageElement, userAgent, proxyConfig);
        var result = client.SolveCaptcha(request);

        if (result.Status == TaskStatusType.Ready && result.Solution.IsValid())
        {
            var jObject = (result.Solution as AntiGateSolution)?.Cookies; //Should add interface to Solution which returns Cookies.
            if (jObject != null)
                AddCookies(driver, jObject);

            if (responseElement != null)
            {
                FillResponseElement(driver, result.Solution, responseElement);
            }
            
            submitElement?.Click();
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