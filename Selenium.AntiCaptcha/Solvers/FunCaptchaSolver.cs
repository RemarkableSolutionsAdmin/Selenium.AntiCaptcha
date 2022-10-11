using OpenQA.Selenium;
using AntiCaptchaApi.Net.Models.Solutions;
using AntiCaptchaApi.Net.Responses;

namespace Selenium.AntiCaptcha.solvers
{
    internal class FunCaptchaSolver : Solver<FunCaptchaSolution>
    {
        protected override string GetSiteKey(IWebDriver driver)
        {
            return driver.FindElement(By.Id("funcaptcha")).GetAttribute("data-pkey");
        }

        protected override void FillResponseElement(IWebDriver driver, FunCaptchaSolution solution, IWebElement? responseElement)
        {
            throw new NotImplementedException();
        }

        internal override TaskResultResponse<FunCaptchaSolution> Solve(IWebDriver driver, string clientKey, string? url, string? siteKey,
            IWebElement? responseElement,
            IWebElement? submitElement, IWebElement? imageElement)
        {
            throw new NotImplementedException();
        }
    }
}
