using AntiCaptchaApi.Net.Models;
using AntiCaptchaApi.Net.Models.Solutions;
using AntiCaptchaApi.Net.Requests;
using OpenQA.Selenium;
using Selenium.AntiCaptcha.Constants;

namespace Selenium.AntiCaptcha.solvers;

internal class HCaptchaProxylessSolver  : Solver<HCaptchaProxylessRequest, HCaptchaSolution>
{
    protected override HCaptchaProxylessRequest BuildRequest(IWebDriver driver, string? url, string? siteKey, IWebElement? imageElement, string? userAgent, ProxyConfig proxyConfig) =>
        new()
        {
            WebsiteUrl = url ?? driver.Url,
            WebsiteKey = siteKey,
            UserAgent = userAgent ?? AnticaptchaDefaultValues.UserAgent
        };


    protected override void FillResponseElement(IWebDriver driver, HCaptchaSolution solution, IWebElement? responseElement)
    {
        if (responseElement == null)
        {
            responseElement = driver.FindElement(By.Name("h-captcha-response"));
        }

        responseElement.SendKeys(solution.GRecaptchaResponse);
    }
}