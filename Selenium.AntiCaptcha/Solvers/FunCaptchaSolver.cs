using System.Text.RegularExpressions;
using AntiCaptchaApi.Net.Models;
using AntiCaptchaApi.Net.Models.Solutions;
using AntiCaptchaApi.Net.Requests;
using OpenQA.Selenium;
using Selenium.AntiCaptcha.Internal.Extensions;
using Selenium.AntiCaptcha.Solvers.Base;

namespace Selenium.AntiCaptcha.Solvers
{
    internal class FunCaptchaSolver : FunCaptchaSolverBase<FunCaptchaRequest, FunCaptchaSolution>
    {

        protected override FunCaptchaRequest BuildRequest(IWebDriver driver, string? url, string? siteKey, IWebElement? imageElement, string? userAgent, ProxyConfig proxyConfig)
        {
            return new FunCaptchaRequest
            {
                WebsiteUrl = url ?? driver.Url,
                WebsitePublicKey = siteKey,
                FunCaptchaApiJsSubdomain = null, // TODO.
                Data = null, //TODO.
                UserAgent = userAgent ?? Constants.AnticaptchaDefaultValues.UserAgent,
                ProxyConfig = proxyConfig
            };
        }
    }
}
