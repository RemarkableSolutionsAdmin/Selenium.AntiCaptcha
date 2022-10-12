using System.Text.RegularExpressions;
using AntiCaptchaApi.Net;
using AntiCaptchaApi.Net.Enums;
using AntiCaptchaApi.Net.Models;
using OpenQA.Selenium;
using AntiCaptchaApi.Net.Models.Solutions;
using AntiCaptchaApi.Net.Requests.Abstractions;
using AntiCaptchaApi.Net.Responses;
using Newtonsoft.Json.Linq;

namespace Selenium.AntiCaptcha.solvers;

internal abstract class Solver<TRequest, TSolution>
    where TRequest: CaptchaRequest<TSolution>
    where TSolution: BaseSolution, new()
{
    protected virtual string GetSiteKey(IWebDriver driver, int waitingTime = 1000)
    {
        Thread.Sleep(waitingTime);
        
        var regex = new Regex("gt=(.*?)&");
        var gt = regex.Match(driver.PageSource).Groups[1].Value;

        if (!string.IsNullOrEmpty(gt))
            return gt;
        
        regex = new Regex("captcha_id=(.*?)&");
        var captchaIdRegexGroups = regex.Match(driver.PageSource).Groups;
        gt = captchaIdRegexGroups[1].Value;

        
        if (!string.IsNullOrEmpty(gt))
            return gt;

        regex = new Regex("sitekey=(.*?)&");
        var siteKeyCaptchaGroups = regex.Match(driver.PageSource).Groups;
        gt = siteKeyCaptchaGroups[1].Value;

        if (string.IsNullOrEmpty(gt))
            GetSiteKey(driver, waitingTime);
        return gt;
    }

    protected abstract TRequest BuildRequest(IWebDriver driver, string? url, string? siteKey,
        IWebElement? imageElement, string? userAgent, ProxyConfig proxyConfig);

    protected virtual void FillResponseElement(IWebDriver driver, TSolution solution, IWebElement? responseElement)
    {
        
    }

    internal virtual TaskResultResponse<TSolution> Solve(IWebDriver driver, string clientKey, string? url, string? siteKey,
        IWebElement? responseElement,
        IWebElement? submitElement, IWebElement? imageElement, string? userAgent, ProxyConfig proxyConfig)
    {
        var client = new AnticaptchaClient(clientKey);
        siteKey ??= GetSiteKey(driver);
        var request = BuildRequest(driver, url, siteKey, imageElement, userAgent, proxyConfig);
        var result = client.SolveCaptcha(request);

        if (result.Status == TaskStatusType.Ready && result.Solution.IsValid())
        {
            //TODO! Cookies!
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

    protected static void AddCookies(IWebDriver driver, JObject Cookies)
    {
        if (Cookies != null && Cookies.Count > 0)
        {
            driver.Manage().Cookies.DeleteAllCookies();
            foreach (var cookie in Cookies)
            {
                if (!string.IsNullOrEmpty(cookie.Key) && !string.IsNullOrEmpty(cookie.Value?.ToString()))
                    driver.Manage().Cookies.AddCookie(new Cookie(cookie.Key, cookie.Value.ToString()));
            }
        }
    }
}