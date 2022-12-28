using AntiCaptchaApi.Net.Requests;
using OpenQA.Selenium;
using Selenium.AntiCaptcha.Models;
using Selenium.AntiCaptcha.Solvers.Base;

namespace Selenium.AntiCaptcha.Solvers
{
    internal class ReCaptchaV3EnterpriseSolver : RecaptchaSolverBase<RecaptchaV3EnterpriseRequest>
    {
        protected override RecaptchaV3EnterpriseRequest BuildRequest(SolverArguments arguments)
        {
            return new RecaptchaV3EnterpriseRequest
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
                MinScore = solverArguments.MinScore,
                IsEnterprise = solverArguments.IsEnterprise ?? true
            };
        }

        public ReCaptchaV3EnterpriseSolver(string clientKey, IWebDriver driver, SolverConfig solverConfig) : base(clientKey, driver, solverConfig)
        {
        }
    }
}
