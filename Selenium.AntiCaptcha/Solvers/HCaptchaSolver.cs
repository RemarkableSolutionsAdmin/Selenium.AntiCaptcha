using OpenQA.Selenium;
using AntiCaptchaApi.Net;
using AntiCaptchaApi.Net.Enums;
using AntiCaptchaApi.Net.Models;
using AntiCaptchaApi.Net.Models.Solutions;
using AntiCaptchaApi.Net.Requests;
using AntiCaptchaApi.Net.Responses;
using Selenium.AntiCaptcha.Constants;

namespace Selenium.AntiCaptcha.solvers
{
    internal class HCaptchaSolver : Solver<HCaptchaRequest, HCaptchaSolution>
    {
        protected override HCaptchaRequest BuildRequest(IWebDriver driver, string? url, string? siteKey, IWebElement? imageElement, string? userAgent, ProxyConfig proxyConfig)
        {
            return new HCaptchaRequest
            {
                WebsiteUrl = url ?? driver.Url,
                WebsiteKey = siteKey,
                UserAgent = userAgent ?? AnticaptchaDefaultValues.UserAgent,
                ProxyConfig = proxyConfig,
            };
        }

        protected override void FillResponseElement(IWebDriver driver, HCaptchaSolution solution, IWebElement? responseElement)
        {
            responseElement ??= driver.FindElement(By.Name("h-captcha-response"));
            responseElement.SendKeys(solution.GRecaptchaResponse);
        }

        protected override string GetSiteKey(IWebDriver driver, int waitingTime = 1000) => 
            driver.FindElement(By.ClassName("h-captcha")).GetAttribute("data-sitekey");
    }
}
