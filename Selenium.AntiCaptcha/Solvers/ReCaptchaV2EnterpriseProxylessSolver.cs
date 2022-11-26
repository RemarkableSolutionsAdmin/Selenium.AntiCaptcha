using AntiCaptchaApi.Net.Models;
using AntiCaptchaApi.Net.Models.Solutions;
using AntiCaptchaApi.Net.Requests;
using Selenium.AntiCaptcha.Models;
using Selenium.AntiCaptcha.Solvers.Base;

namespace Selenium.AntiCaptcha.Solvers
{
    internal class ReCaptchaV2EnterpriseProxylessSolver : RecaptchaSolverBase <RecaptchaV2EnterpriseProxylessRequest, RecaptchaSolution>
    {
        protected override RecaptchaV2EnterpriseProxylessRequest BuildRequest(SolverAdditionalArguments additionalArguments)
        {
            return new RecaptchaV2EnterpriseProxylessRequest
            {
                WebsiteUrl = additionalArguments.Url,
                WebsiteKey = additionalArguments.SiteKey,
            };
        }
    }
}
