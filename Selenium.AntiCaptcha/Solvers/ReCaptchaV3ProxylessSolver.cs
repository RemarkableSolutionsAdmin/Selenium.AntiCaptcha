using AntiCaptchaApi.Net.Models;
using AntiCaptchaApi.Net.Models.Solutions;
using AntiCaptchaApi.Net.Requests;
using OpenQA.Selenium;
using Selenium.AntiCaptcha.Solvers.Base;

namespace Selenium.AntiCaptcha.Solvers
{
    internal class ReCaptchaV3ProxylessSolver : RecaptchaSolverBase<RecaptchaV3ProxylessRequest, RecaptchaSolution>
    {
        protected override RecaptchaV3ProxylessRequest BuildRequest(IWebDriver driver, string? url, string? siteKey, IWebElement? imageElement, string? userAgent, ProxyConfig proxyConfig)
        {
            return new RecaptchaV3ProxylessRequest
            {
                WebsiteUrl = url ?? driver.Url,
                WebsiteKey = siteKey,
                MinScore = 0.3, //TODO
                PageAction = null, //TODO
                IsEnterprise = false, // TODO
                ApiDomain = null //TODO
            };
        }
    }
}
