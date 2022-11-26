using AntiCaptchaApi.Net.Models;
using AntiCaptchaApi.Net.Models.Solutions;
using AntiCaptchaApi.Net.Requests;
using Selenium.AntiCaptcha.Models;
using Selenium.AntiCaptcha.Solvers.Base;

namespace Selenium.AntiCaptcha.Solvers
{
    internal class ReCaptchaV2EnterpriseSolver : RecaptchaSolverBase <RecaptchaV2EnterpriseRequest, RecaptchaSolution>
    {
        protected override RecaptchaV2EnterpriseRequest BuildRequest(SolverAdditionalArguments additionalArguments)
        {
            return new RecaptchaV2EnterpriseRequest
            {
                WebsiteUrl = additionalArguments.Url,
                WebsiteKey = additionalArguments.SiteKey,
                UserAgent = additionalArguments.UserAgent,
                ProxyConfig = additionalArguments.ProxyConfig,
            };
        }
    }
}
