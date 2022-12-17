using AntiCaptchaApi.Net.Models.Solutions;
using AntiCaptchaApi.Net.Requests;
using OpenQA.Selenium;
using Selenium.AntiCaptcha.Models;
using Selenium.AntiCaptcha.Solvers.Base;

namespace Selenium.AntiCaptcha.Solvers
{
    internal class ReCaptchaV2Solver : RecaptchaSolverBase <RecaptchaV2Request, RecaptchaSolution>
    {
        protected override RecaptchaV2Request BuildRequest(SolverAdditionalArguments additionalArguments)
        {
            return new RecaptchaV2Request
            {
                WebsiteUrl = additionalArguments.Url,
                WebsiteKey = additionalArguments.SiteKey,
                UserAgent = additionalArguments.UserAgent,
                ProxyConfig = additionalArguments.ProxyConfig,
            };
        }

        public ReCaptchaV2Solver(string clientKey, IWebDriver driver) : base(clientKey, driver)
        {
        }
    }
}
