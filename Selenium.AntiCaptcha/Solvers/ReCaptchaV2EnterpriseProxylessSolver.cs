using AntiCaptchaApi.Net.Models.Solutions;
using AntiCaptchaApi.Net.Requests;
using OpenQA.Selenium;
using Selenium.AntiCaptcha.Models;
using Selenium.AntiCaptcha.Solvers.Base;

namespace Selenium.AntiCaptcha.Solvers
{
    internal class ReCaptchaV2EnterpriseProxylessSolver : RecaptchaSolverBase <RecaptchaV2EnterpriseProxylessRequest>
    {
        protected override RecaptchaV2EnterpriseProxylessRequest BuildRequest(SolverAdditionalArguments additionalArguments)
        {
            return new RecaptchaV2EnterpriseProxylessRequest
            {
                WebsiteUrl = additionalArguments.Url,
                WebsiteKey = additionalArguments.SiteKey,
            };
        }

        public ReCaptchaV2EnterpriseProxylessSolver(string clientKey, IWebDriver driver) : base(clientKey, driver)
        {
        }
    }
}
