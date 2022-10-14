using AntiCaptchaApi.Net.Models;
using AntiCaptchaApi.Net.Models.Solutions;
using AntiCaptchaApi.Net.Requests;
using OpenQA.Selenium;
using Selenium.AntiCaptcha.Solvers.Base;

namespace Selenium.AntiCaptcha.Solvers
{
    internal class ReCaptchaV2EnterpriseProxylessSolver : RecaptchaSolverBase <RecaptchaV2EnterpriseProxylessRequest, RecaptchaSolution>
    {
        protected override RecaptchaV2EnterpriseProxylessRequest BuildRequest(IWebDriver driver, string? url, string? siteKey, IWebElement? imageElement, string? userAgent, ProxyConfig proxyConfig)
        {
            return new RecaptchaV2EnterpriseProxylessRequest
            {
                WebsiteUrl = url ?? driver.Url,
                WebsiteKey = siteKey,
            };
        }
    }
}
