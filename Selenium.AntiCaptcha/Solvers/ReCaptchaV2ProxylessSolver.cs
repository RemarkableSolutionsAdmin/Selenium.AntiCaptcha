using AntiCaptchaApi.Net.Models;
using AntiCaptchaApi.Net.Models.Solutions;
using AntiCaptchaApi.Net.Requests;
using OpenQA.Selenium;
using Selenium.AntiCaptcha.Solvers.Base;

namespace Selenium.AntiCaptcha.Solvers
{
    internal class ReCaptchaV2ProxylessSolver : RecaptchaSolverBase <RecaptchaV2ProxylessRequest, RecaptchaSolution>
    {
        protected override RecaptchaV2ProxylessRequest BuildRequest(IWebDriver driver, string? url, string? siteKey, IWebElement? imageElement, string? userAgent, ProxyConfig proxyConfig)
        {
            return new RecaptchaV2ProxylessRequest
            {
                WebsiteUrl = url ?? driver.Url,
                WebsiteKey = siteKey
            };
        }
    }
}
