using AntiCaptchaApi.Net.Requests;
using OpenQA.Selenium;
using Selenium.AntiCaptcha.Models;
using Selenium.AntiCaptcha.Solvers.Base;

namespace Selenium.AntiCaptcha.Solvers
{
    internal class RecaptchaV3Solver : RecaptchaSolverBase<RecaptchaV3Request>
    {
        protected override RecaptchaV3Request BuildRequest(SolverArguments arguments)
        {
            return new RecaptchaV3Request
            {
                WebsiteUrl = arguments.WebsiteUrl,
                WebsiteKey = arguments.WebsiteKey,
                MinScore = arguments.MinScore,
                PageAction = arguments.PageAction,
                IsEnterprise = arguments.IsEnterprise,
                ApiDomain = arguments.ApiDomain
            };
        }

        protected override async Task<SolverArguments> FillMissingSolverArguments(
            SolverArguments solverArguments)
        {
            return await base.FillMissingSolverArguments(solverArguments) with
            {
                IsEnterprise = solverArguments.IsEnterprise ?? false
            };
        }

        public RecaptchaV3Solver(string clientKey, IWebDriver driver, SolverConfig solverConfig) : base(clientKey, driver, solverConfig)
        {
        }
    }
}
