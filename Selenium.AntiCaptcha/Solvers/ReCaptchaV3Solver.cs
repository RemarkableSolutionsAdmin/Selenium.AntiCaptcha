using AntiCaptchaApi.Net.Models;
using OpenQA.Selenium;
using AntiCaptchaApi.Net.Models.Solutions;
using AntiCaptchaApi.Net.Requests;
using AntiCaptchaApi.Net.Responses;

namespace Selenium.AntiCaptcha.solvers
{
    internal class ReCaptchaV3Solver : Solver<RecaptchaV3ProxylessRequest, RecaptchaSolution>
    {

        protected override string GetSiteKey(IWebDriver driver, int waitingTime = 1000)
        {
            throw new NotImplementedException();
        }

        protected override RecaptchaV3ProxylessRequest BuildRequest(IWebDriver driver, string? url, string? siteKey, IWebElement? imageElement, string? userAgent, ProxyConfig proxyConfig)
        {
            throw new NotImplementedException();
        }

        protected override void FillResponseElement(IWebDriver driver, RecaptchaSolution solution, IWebElement? responseElement)
        {
            throw new NotImplementedException();
        }

        internal override TaskResultResponse<RecaptchaSolution> Solve(IWebDriver driver, string clientKey, string? url, string? siteKey,
            IWebElement? responseElement,
            IWebElement? submitElement, IWebElement? imageElement, string? userAgent, ProxyConfig proxyConfig)
        {
            throw new NotImplementedException();
        }
    }
}
