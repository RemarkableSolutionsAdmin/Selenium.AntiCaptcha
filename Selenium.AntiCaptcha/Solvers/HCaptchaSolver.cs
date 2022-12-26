using AntiCaptchaApi.Net.Models.Solutions;
using AntiCaptchaApi.Net.Requests;
using OpenQA.Selenium;
using Selenium.AntiCaptcha.Models;
using Selenium.AntiCaptcha.Solvers.Base;

namespace Selenium.AntiCaptcha.Solvers
{
    internal class HCaptchaSolver : HCaptchaSolverBase<HCaptchaRequest>
    {
        protected override HCaptchaRequest BuildRequest(SolverAdditionalArguments additionalArguments)
        {
            return new HCaptchaRequest
            {
                WebsiteUrl = additionalArguments.Url,
                WebsiteKey = additionalArguments.SiteKey,
                UserAgent = additionalArguments.UserAgent,
                ProxyConfig = additionalArguments.ProxyConfig,
                IsInvisible = additionalArguments.IsInvisible,
                EnterprisePayload = additionalArguments.EnterprisePayload
            };
        }

        public HCaptchaSolver(string clientKey, IWebDriver driver) : base(clientKey, driver)
        {
        }
    }
}
