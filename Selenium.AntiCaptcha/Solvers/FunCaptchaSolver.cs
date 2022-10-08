using System;
using OpenQA.Selenium;
using AntiCaptchaApi.Models;

namespace Selenium.AntiCaptcha.solvers
{
    internal class FunCaptchaSolver : Solver
    {
        protected override void FillResponseElement(IWebDriver driver, SolutionData solution, IWebElement? responseElement)
        {
            throw new NotImplementedException();
        }

        protected override string GetSiteKey(IWebDriver driver)
        {
            return driver.FindElement(By.Id("funcaptcha")).GetAttribute("data-pkey");
        }
        
        internal override void Solve(IWebDriver driver, string clientKey, string? url, string? siteKey, IWebElement? responseElement,
            IWebElement? submitElement, IWebElement? imageElement)
        {
            throw new NotImplementedException();
        }
    }
}
