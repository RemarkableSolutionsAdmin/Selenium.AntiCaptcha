using OpenQA.Selenium;
using System;
using AntiCaptchaApi.Models;
using AntiCaptchaApi.Models.Solutions;

namespace Selenium.AntiCaptcha.solvers
{
    internal class ReCaptchaV3Solver : Solver
    {
        protected override void FillResponseElement(IWebDriver driver, RawSolution solution, IWebElement? responseElement)
        {
            throw new NotImplementedException();
        }

        protected override string GetSiteKey(IWebDriver driver)
        {
            throw new NotImplementedException();
        }

        internal override void Solve(IWebDriver driver, string clientKey, string? url, string? siteKey, IWebElement? responseElement,
            IWebElement? submitElement, IWebElement? imageElement)
        {
            throw new NotImplementedException();
        }
    }
}
