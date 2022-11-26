using System.Text.RegularExpressions;
using AntiCaptchaApi.Net.Models;
using AntiCaptchaApi.Net.Models.Solutions;
using AntiCaptchaApi.Net.Requests;
using OpenQA.Selenium;
using Selenium.AntiCaptcha.Models;
using Selenium.AntiCaptcha.Solvers.Base;

namespace Selenium.AntiCaptcha.Solvers
{
    internal class GeeTestV4Solver : Solver<GeeTestV4Request, GeeTestV4Solution>
    {
        protected override GeeTestV4Request BuildRequest(SolverAdditionalArguments additionalArguments)
        {
            return  new GeeTestV4Request
            {
                WebsiteUrl = additionalArguments.Url,
                Gt = additionalArguments.Gt,
                InitParameters = additionalArguments.InitParameters,
                ProxyConfig = additionalArguments.ProxyConfig,
                UserAgent = additionalArguments.UserAgent,
                Challenge = additionalArguments.Challenge
            };
        }
        
        protected override SolverAdditionalArguments FillMissingAdditionalArguments(IWebDriver driver, SolverAdditionalArguments solverAdditionalArguments)
        {
            return base.FillMissingAdditionalArguments(driver, solverAdditionalArguments)
                with
                {
                    Gt = solverAdditionalArguments.Gt ?? AcquireSiteKey(driver, 0, solverAdditionalArguments.MaxPageLoadWaitingTimeInMilliseconds),
                    Challenge = solverAdditionalArguments.Challenge ?? GetChallenge(driver)
                };
        }

        private string GetChallenge(IWebDriver driver)
        {
            var regex = new Regex("challenge=(.*?)&");
            return regex.Match(driver.PageSource).Groups[1].Value;
        }
    }
}
