using OpenQA.Selenium;
using RemarkableSolutions.Anticaptcha.Api.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RemarkableSolutions.Selenium.AntiCaptcha.solvers
{
    internal class ReCaptchaV3Solver : Solver
    {
        protected override void FillResponseElement(IWebDriver driver, TaskResultResponse.SolutionData solution, IWebElement? responseElement)
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
