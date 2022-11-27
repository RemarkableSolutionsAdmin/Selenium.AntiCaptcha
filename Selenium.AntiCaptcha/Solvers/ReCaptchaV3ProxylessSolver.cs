using AntiCaptchaApi.Net.Models;
using AntiCaptchaApi.Net.Models.Solutions;
using AntiCaptchaApi.Net.Requests;
using OpenQA.Selenium;
using Selenium.AntiCaptcha.Constants;
using Selenium.AntiCaptcha.Models;
using Selenium.AntiCaptcha.Solvers.Base;

namespace Selenium.AntiCaptcha.Solvers
{
    internal class ReCaptchaV3ProxylessSolver : RecaptchaSolverBase<RecaptchaV3ProxylessRequest, RecaptchaSolution>
    {
        protected override RecaptchaV3ProxylessRequest BuildRequest(SolverAdditionalArguments additionalArguments)
        {
            return new RecaptchaV3ProxylessRequest
            {
                WebsiteUrl = additionalArguments.Url,
                WebsiteKey = additionalArguments.SiteKey,
                MinScore = additionalArguments.MinScore!.Value,
                PageAction = additionalArguments.PageAction,
                IsEnterprise = additionalArguments.IsEnterprise!.Value,
                ApiDomain = additionalArguments.ApiDomain
            };
        }

        protected override async Task<SolverAdditionalArguments> FillMissingAdditionalArguments(IWebDriver driver,
            SolverAdditionalArguments solverAdditionalArguments)
        {
            return await base.FillMissingAdditionalArguments(driver, solverAdditionalArguments) with
            {
                MinScore = solverAdditionalArguments.MinScore ?? AnticaptchaDefaultValues.MinScore,
                IsEnterprise = solverAdditionalArguments.IsEnterprise ?? false
            };
        }
    }
}
