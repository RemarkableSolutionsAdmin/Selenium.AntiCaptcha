using OpenQA.Selenium;
using RemarkableSolutions.Anticaptcha.Api.Responses;

namespace RemarkableSolutions.Selenium.AntiCaptcha.solutions
{
    internal class FunCaptchaSolver : Solver
    {
        protected override void FillResponseElement(IWebDriver driver, TaskResultResponse.SolutionData solution, IWebElement? responseElement)
        {
            throw new NotImplementedException();
        }

        protected override string GetSiteKey(IWebDriver driver)
        {
            return driver.FindElement(By.Id("funcaptcha")).GetAttribute("data-pkey");
        }

        internal override void Solve(IWebDriver driver, string clientKey, string? url, string? siteKey, IWebElement? responseElement, IWebElement? submitElement)
        {
            throw new NotImplementedException();
        }
    }
}
