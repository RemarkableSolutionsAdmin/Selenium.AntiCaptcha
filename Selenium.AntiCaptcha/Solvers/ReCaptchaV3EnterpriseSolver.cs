using AntiCaptchaApi.Net.Models;
using AntiCaptchaApi.Net.Models.Solutions;
using AntiCaptchaApi.Net.Requests;
using OpenQA.Selenium;
using Selenium.AntiCaptcha.Constants;
using Selenium.AntiCaptcha.Models;
using Selenium.AntiCaptcha.Solvers.Base;

namespace Selenium.AntiCaptcha.Solvers
{
    internal class ReCaptchaV3EnterpriseSolver : RecaptchaSolverBase<RecaptchaV3EnterpriseRequest, RecaptchaSolution>
    {
        protected override RecaptchaV3EnterpriseRequest BuildRequest(SolverAdditionalArguments additionalArguments)
        {
            return new RecaptchaV3EnterpriseRequest
            {
                WebsiteUrl = additionalArguments.Url,
                WebsiteKey = additionalArguments.SiteKey,
                MinScore = additionalArguments.MinScore!.Value,
                PageAction = additionalArguments.PageAction,
                IsEnterprise = additionalArguments.IsEnterprise!.Value,
                ApiDomain = additionalArguments.ApiDomain
            };
        }

        protected override SolverAdditionalArguments FillMissingAdditionalArguments(IWebDriver driver, SolverAdditionalArguments solverAdditionalArguments)
        {
            return base.FillMissingAdditionalArguments(driver, solverAdditionalArguments) with
            {
                MinScore = solverAdditionalArguments.MinScore ?? AnticaptchaDefaultValues.MinScore,
                IsEnterprise = solverAdditionalArguments.IsEnterprise ?? true
            };
        }
    }
}
