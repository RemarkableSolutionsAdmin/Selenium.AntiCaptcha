using AntiCaptchaApi.Net.Models;
using AntiCaptchaApi.Net.Models.Solutions;
using AntiCaptchaApi.Net.Requests;
using Selenium.AntiCaptcha.Models;
using Selenium.AntiCaptcha.Solvers.Base;

namespace Selenium.AntiCaptcha.Solvers
{
    internal class FunCaptchaProxylessSolver : FunCaptchaSolverBase<FunCaptchaProxylessRequest, FunCaptchaSolution>
    {
        protected override FunCaptchaProxylessRequest BuildRequest(SolverAdditionalArguments additionalArguments)
        {
            return new FunCaptchaProxylessRequest
            {
                WebsiteUrl = additionalArguments.Url,
                WebsitePublicKey = additionalArguments.SiteKey,
                Data = additionalArguments.Data,
                FunCaptchaApiJsSubdomain = additionalArguments.FunCaptchaApiJsSubdomain
            };
        }
    }
}
