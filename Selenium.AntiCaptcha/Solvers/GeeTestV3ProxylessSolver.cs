using System.Text.RegularExpressions;
using AntiCaptchaApi.Net.Models.Solutions;
using AntiCaptchaApi.Net.Requests;
using OpenQA.Selenium;
using Selenium.AntiCaptcha.Internal.Extensions;
using Selenium.AntiCaptcha.Models;
using Selenium.AntiCaptcha.Solvers.Base;

namespace Selenium.AntiCaptcha.Solvers
{
    internal class GeeTestV3ProxylessSolver : GeeTestV3SolverBase<GeeTestV3ProxylessRequest>
    {
        protected override GeeTestV3ProxylessRequest BuildRequest(SolverArguments arguments)
        {
            return  new GeeTestV3ProxylessRequest
            {
                WebsiteUrl = arguments.Url,
                Challenge = arguments.Challenge,
                GeetestApiServerSubdomain = arguments.GeetestApiServerSubdomain,
                GeetestGetLib = arguments.GeetestGetLib,
                Gt = arguments.Gt,
            };
        }

        public GeeTestV3ProxylessSolver(string clientKey, IWebDriver driver, SolverConfig solverConfig) : base(clientKey, driver, solverConfig)
        {
        }
    }
}
