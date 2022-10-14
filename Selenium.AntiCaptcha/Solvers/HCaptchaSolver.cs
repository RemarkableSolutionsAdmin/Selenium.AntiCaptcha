using AntiCaptchaApi.Net.Models;
using AntiCaptchaApi.Net.Models.Solutions;
using AntiCaptchaApi.Net.Requests;
using OpenQA.Selenium;
using Selenium.AntiCaptcha.Constants;
using Selenium.AntiCaptcha.Solvers.Base;

namespace Selenium.AntiCaptcha.Solvers
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

        protected override string GetSiteKey(IWebDriver driver, int waitingTime = 1000, int tries = 3) => 
            driver.FindElement(By.ClassName("h-captcha")).GetAttribute("data-sitekey");
    }
}
