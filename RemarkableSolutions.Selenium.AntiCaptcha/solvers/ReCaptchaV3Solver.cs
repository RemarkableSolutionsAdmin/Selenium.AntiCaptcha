using OpenQA.Selenium;
using System;
using RemarkableSolutions.Anticaptcha.Api.Models;

namespace RemarkableSolutions.Selenium.AntiCaptcha.solvers
{
    internal class ReCaptchaV3Solver : Solver
    {
        protected override void FillResponseElement(IWebDriver driver, SolutionData solution, IWebElement? responseElement)
        {
            throw new NotImplementedException();
        }

        protected override string GetSiteKey(IWebDriver driver)
        {
            throw new NotImplementedException();
        }

        internal override void Solve(IWebDriver driver, string clientKey, string? url, string? siteKey, IWebElement? responseElement, IWebElement? submitElement)
        {
            throw new NotImplementedException();
        }
    }
}
