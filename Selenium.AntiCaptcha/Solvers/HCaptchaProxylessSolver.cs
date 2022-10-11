using System.Text.RegularExpressions;
using AntiCaptchaApi.Net;
using AntiCaptchaApi.Net.Enums;
using AntiCaptchaApi.Net.Models;
using AntiCaptchaApi.Net.Models.Solutions;
using AntiCaptchaApi.Net.Requests;
using AntiCaptchaApi.Net.Responses;
using OpenQA.Selenium;
using Selenium.AntiCaptcha.Constants;

namespace Selenium.AntiCaptcha.solvers;

internal class HCaptchaProxylessSolver  : Solver<HCaptchaSolution>
{
    protected override void FillResponseElement(IWebDriver driver, HCaptchaSolution solution, IWebElement? responseElement)
    {
        if (responseElement == null)
        {
            responseElement = driver.FindElement(By.Name("h-captcha-response"));
        }

        responseElement.SendKeys(solution.GRecaptchaResponse);
    }
    protected override string GetSiteKey(IWebDriver driver, int waitingTime = 1000)
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

    internal override TaskResultResponse<HCaptchaSolution> Solve(IWebDriver driver, string clientKey, string? url, string? siteKey,
        IWebElement? responseElement, IWebElement? submitElement, IWebElement? imageElement, string? userAgent, ProxyConfig proxyConfig)
    {
        var client = new AnticaptchaClient(clientKey);
        siteKey ??= GetSiteKey(driver);

        var captchaRequest = new HCaptchaProxylessRequest
        {
            WebsiteUrl = url ?? driver.Url,
            WebsiteKey = siteKey,
            UserAgent = userAgent ?? AnticaptchaDefaultValues.UserAgent
        };

        var result = client.SolveCaptcha<HCaptchaProxylessRequest, HCaptchaSolution>(captchaRequest);

        if (result.Status == TaskStatusType.Ready)
        {
            
            //FillResponseElement(driver, result.Solution, responseElement);
        }

        submitElement?.Click();
        return result;
    }
}