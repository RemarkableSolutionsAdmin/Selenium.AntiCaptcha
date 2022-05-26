using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RemarkableSolutions.Selenium.AntiCaptcha.solutions
{
    internal class ReCaptchaV3Solver : Solver
    {
        protected override string GetSiteKey(IWebDriver driver)
        {
            throw new NotImplementedException();
        }

        internal override void Solve(IWebDriver driver, string clientKey, string? url, string? siteKey, IWebElement? submitElement)
        {
            throw new NotImplementedException();
        }
    }
}
