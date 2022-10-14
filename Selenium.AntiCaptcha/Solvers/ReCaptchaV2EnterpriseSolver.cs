using AntiCaptchaApi.Net.Models;
using AntiCaptchaApi.Net.Models.Solutions;
using AntiCaptchaApi.Net.Requests;
using OpenQA.Selenium;
using Selenium.AntiCaptcha.Solvers.Base;

namespace Selenium.AntiCaptcha.Solvers
{
    internal class ReCaptchaV2EnterpriseSolver : RecaptchaSolverBase <RecaptchaV2EnterpriseRequest, RecaptchaSolution>
    {
        protected override RecaptchaV2EnterpriseRequest BuildRequest(IWebDriver driver, string? url, string? siteKey, IWebElement? imageElement, string? userAgent, ProxyConfig proxyConfig)
        {
            return new RecaptchaV2EnterpriseRequest
            {
                WebsiteUrl = url ?? driver.Url,
                WebsiteKey = siteKey,
                UserAgent = userAgent ?? Constants.AnticaptchaDefaultValues.UserAgent,
                ProxyConfig = proxyConfig,
            };
        }
    }
}
