using System.Text.RegularExpressions;
using AntiCaptchaApi.Net.Models.Solutions;
using AntiCaptchaApi.Net.Requests;
using OpenQA.Selenium;
using Selenium.AntiCaptcha.Models;
using Selenium.AntiCaptcha.Solvers.Base;

namespace Selenium.AntiCaptcha.Solvers
{
    internal class GeeTestV4ProxylessSolver : GeeTestV4SolverBase<GeeTestV4ProxylessRequest>
    {
        protected override GeeTestV4ProxylessRequest BuildRequest(SolverAdditionalArguments additionalArguments)
        {
            return  new GeeTestV4ProxylessRequest
            {
                WebsiteUrl = additionalArguments.Url,
                Challenge = additionalArguments.Challenge,
                Gt = additionalArguments.Gt,
                InitParameters = additionalArguments.InitParameters
            };
        }

        public GeeTestV4ProxylessSolver(string clientKey, IWebDriver driver) : base(clientKey, driver)
        {
        }
    }
}
