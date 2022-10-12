using AntiCaptchaApi.Net.Models;
using OpenQA.Selenium;
using AntiCaptchaApi.Net.Models.Solutions;
using AntiCaptchaApi.Net.Requests;
using AntiCaptchaApi.Net.Responses;

namespace Selenium.AntiCaptcha.solvers
{
    internal class FunCaptchaSolver : Solver<FunCaptchaRequest, FunCaptchaSolution>
    {
        protected override string GetSiteKey(IWebDriver driver, int waitingTime = 1000)
        {
            return driver.FindElement(By.Id("funcaptcha")).GetAttribute("data-pkey");
        }

        protected override FunCaptchaRequest BuildRequest(IWebDriver driver, string? url, string? siteKey, IWebElement? imageElement, string? userAgent, ProxyConfig proxyConfig)
        {
            throw new NotImplementedException();
        }
        
        internal override TaskResultResponse<FunCaptchaSolution> Solve(IWebDriver driver, string clientKey, string? url, string? siteKey,
            IWebElement? responseElement,
            IWebElement? submitElement, IWebElement? imageElement, string? userAgent, ProxyConfig proxyConfig)
        {
            throw new NotImplementedException();
        }
    }
}
