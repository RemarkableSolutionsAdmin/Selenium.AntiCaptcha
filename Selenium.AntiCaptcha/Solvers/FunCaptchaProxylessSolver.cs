using AntiCaptchaApi.Net.Models;
using AntiCaptchaApi.Net.Models.Solutions;
using AntiCaptchaApi.Net.Requests;
using OpenQA.Selenium;
using Selenium.AntiCaptcha.Solvers.Base;

namespace Selenium.AntiCaptcha.Solvers
{
    internal class FunCaptchaProxylessSolver : FunCaptchaSolverBase<FunCaptchaProxylessRequest, FunCaptchaSolution>
    {
        protected override FunCaptchaProxylessRequest BuildRequest(IWebDriver driver, string? url, string? siteKey, IWebElement? imageElement, string? userAgent, ProxyConfig proxyConfig)
        {
            return new FunCaptchaProxylessRequest
            {
                WebsiteUrl = url ?? driver.Url,
                WebsitePublicKey = siteKey,
                Data = null, //TODO.
                FunCaptchaApiJsSubdomain = "test", //TODO
            };
        }
    }
}
