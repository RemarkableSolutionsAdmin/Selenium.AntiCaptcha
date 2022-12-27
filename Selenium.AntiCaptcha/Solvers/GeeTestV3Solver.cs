using System.Text.RegularExpressions;
using AntiCaptchaApi.Net.Models.Solutions;
using AntiCaptchaApi.Net.Requests;
using OpenQA.Selenium;
using Selenium.AntiCaptcha.Internal.Extensions;
using Selenium.AntiCaptcha.Models;
using Selenium.AntiCaptcha.Solvers.Base;

namespace Selenium.AntiCaptcha.Solvers
{
    internal class GeeTestV3Solver : GeeTestV3SolverBase<GeeTestV3Request>
    {
        protected override GeeTestV3Request BuildRequest(SolverArguments arguments)
        {
            return  new GeeTestV3Request
            {
                WebsiteUrl = arguments.Url,
                Challenge = arguments.Challenge,
                GeetestApiServerSubdomain = arguments.GeetestApiServerSubdomain,
                GeetestGetLib = arguments.GeetestGetLib,
                Gt = arguments.Gt,
                ProxyConfig = arguments.ProxyConfig,
                UserAgent = arguments.UserAgent
            };
        }

        public GeeTestV3Solver(string clientKey, IWebDriver driver, SolverConfig solverConfig) : base(clientKey, driver, solverConfig)
        {
        }
    }
}
