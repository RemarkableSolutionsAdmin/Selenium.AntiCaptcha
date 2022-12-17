using AntiCaptchaApi.Net.Models.Solutions;
using AntiCaptchaApi.Net.Requests;
using OpenQA.Selenium;
using Selenium.AntiCaptcha.Models;
using Selenium.AntiCaptcha.Solvers.Base;

namespace Selenium.AntiCaptcha.Solvers
{
    internal class ReCaptchaV2ProxylessSolver : RecaptchaSolverBase <RecaptchaV2ProxylessRequest, RecaptchaSolution>
    {
        protected override RecaptchaV2ProxylessRequest BuildRequest(SolverAdditionalArguments additionalArguments)
        {
            return new RecaptchaV2ProxylessRequest
            {
                WebsiteUrl = additionalArguments.Url,
                WebsiteKey = additionalArguments.SiteKey
            };
        }

        public ReCaptchaV2ProxylessSolver(string clientKey, IWebDriver driver) : base(clientKey, driver)
        {
        }
    }
}
