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
        protected override GeeTestV4ProxylessRequest BuildRequest(SolverArguments arguments)
        {
            return  new GeeTestV4ProxylessRequest
            {
                WebsiteUrl = arguments.Url,
                Challenge = arguments.Challenge,
                Gt = arguments.Gt,
                InitParameters = arguments.InitParameters
            };
        }

        public GeeTestV4ProxylessSolver(string clientKey, IWebDriver driver, SolverConfig solverConfig) : base(clientKey, driver, solverConfig)
        {
        }
    }
}
