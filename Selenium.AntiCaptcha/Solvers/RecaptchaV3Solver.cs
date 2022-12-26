using AntiCaptchaApi.Net.Models.Solutions;
using AntiCaptchaApi.Net.Requests;
using OpenQA.Selenium;
using Selenium.AntiCaptcha.Constants;
using Selenium.AntiCaptcha.Models;
using Selenium.AntiCaptcha.Solvers.Base;

namespace Selenium.AntiCaptcha.Solvers
{
    internal class RecaptchaV3Solver : RecaptchaSolverBase<RecaptchaV3Request>
    {
        protected override RecaptchaV3Request BuildRequest(SolverAdditionalArguments additionalArguments)
        {
            return new RecaptchaV3Request
            {
                WebsiteUrl = additionalArguments.Url,
                WebsiteKey = additionalArguments.SiteKey,
                MinScore = additionalArguments.MinScore!.Value,
                PageAction = additionalArguments.PageAction,
                IsEnterprise = additionalArguments.IsEnterprise!.Value,
                ApiDomain = additionalArguments.ApiDomain
            };
        }

        protected override async Task<SolverAdditionalArguments> FillMissingAdditionalArguments(
            SolverAdditionalArguments solverAdditionalArguments)
        {
            return await base.FillMissingAdditionalArguments(solverAdditionalArguments) with
            {
                MinScore = solverAdditionalArguments.MinScore ?? AnticaptchaDefaultValues.MinScore,
                IsEnterprise = solverAdditionalArguments.IsEnterprise ?? false
            };
        }

        public RecaptchaV3Solver(string clientKey, IWebDriver driver) : base(clientKey, driver)
        {
        }
    }
}
