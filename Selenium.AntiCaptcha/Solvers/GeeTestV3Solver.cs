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
        protected override GeeTestV3Request BuildRequest(SolverAdditionalArguments additionalArguments)
        {
            return  new GeeTestV3Request
            {
                WebsiteUrl = additionalArguments.Url,
                Challenge = additionalArguments.Challenge,
                GeetestApiServerSubdomain = additionalArguments.GeetestApiServerSubdomain,
                GeetestGetLib = additionalArguments.GeetestGetLib,
                Gt = additionalArguments.Gt,
                ProxyConfig = additionalArguments.ProxyConfig,
                UserAgent = additionalArguments.UserAgent
            };
        }

        public GeeTestV3Solver(string clientKey, IWebDriver driver) : base(clientKey, driver)
        {
        }
    }
}
