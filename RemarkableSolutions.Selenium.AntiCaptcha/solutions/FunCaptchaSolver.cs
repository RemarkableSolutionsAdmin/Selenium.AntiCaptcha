using OpenQA.Selenium;

namespace RemarkableSolutions.Selenium.AntiCaptcha.solutions
{
    internal class FunCaptchaSolver : Solver
    {
        internal override void Solve(IWebDriver driver, string clientKey, string? url, string? siteKey, IWebElement? submitElement)
        {
        }
        protected override string GetSiteKey(IWebDriver driver)
        {
            return driver.FindElement(By.Id("funcaptcha")).GetAttribute("data-pkey");
        }
    }
}
