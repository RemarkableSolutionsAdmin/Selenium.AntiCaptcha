using AntiCaptchaApi.Net.Models.Solutions;
using AntiCaptchaApi.Net.Requests;
using OpenQA.Selenium;
using Selenium.AntiCaptcha.Models;
using Selenium.AntiCaptcha.Solvers.Base;

namespace Selenium.AntiCaptcha.Solvers
{
    internal class ReCaptchaV2Solver : RecaptchaSolverBase <RecaptchaV2Request>
    {
        protected override RecaptchaV2Request BuildRequest(SolverArguments arguments)
        {
            return new RecaptchaV2Request
            {
                WebsiteUrl = arguments.Url,
                WebsiteKey = arguments.SiteKey,
                UserAgent = arguments.UserAgent,
                ProxyConfig = arguments.ProxyConfig,
            };
        }

        public ReCaptchaV2Solver(string clientKey, IWebDriver driver, SolverConfig solverConfig) : base(clientKey, driver, solverConfig)
        {
        }
    }
}
