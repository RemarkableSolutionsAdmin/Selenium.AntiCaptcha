using System.Text.RegularExpressions;
using AntiCaptchaApi.Net.Models;
using AntiCaptchaApi.Net.Models.Solutions;
using AntiCaptchaApi.Net.Requests;
using OpenQA.Selenium;
using Selenium.AntiCaptcha.Internal.Extensions;
using Selenium.AntiCaptcha.Models;
using Selenium.AntiCaptcha.Solvers.Base;

namespace Selenium.AntiCaptcha.Solvers
{
    internal class GeeTestV3ProxylessSolver : Solver<GeeTestV3ProxylessRequest, GeeTestV3Solution>
    {
        protected override GeeTestV3ProxylessRequest BuildRequest(SolverAdditionalArguments additionalArguments)
        {
            return  new GeeTestV3ProxylessRequest
            {
                WebsiteUrl = additionalArguments.Url,
                Challenge = additionalArguments.Challenge,
                GeetestApiServerSubdomain = additionalArguments.GeetestApiServerSubdomain,
                GeetestGetLib = additionalArguments.GeetestGetLib,
                Gt = additionalArguments.Gt,
            };
        }

        protected override async Task<SolverAdditionalArguments> FillMissingAdditionalArguments(IWebDriver driver,
            SolverAdditionalArguments solverAdditionalArguments)
        {
            return await base.FillMissingAdditionalArguments(driver, solverAdditionalArguments)
                with
                {
                    Challenge = solverAdditionalArguments.Challenge ?? GetChallenge(driver),
                    Gt = solverAdditionalArguments.Gt ?? await AcquireSiteKey(driver, solverAdditionalArguments.MaxPageLoadWaitingTimeInMilliseconds),
                };
        }

        private static string GetChallenge(IWebDriver driver)
        {
            var pageSource = driver.GetAllPageSource();
            var regex = new Regex("challenge=(.*?)&");
            return regex.Match(pageSource).Groups[1].Value;
        }
    }
}
