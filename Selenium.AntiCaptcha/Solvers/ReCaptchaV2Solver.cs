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
                WebsiteUrl = arguments.WebsiteUrl,
                WebsiteKey = arguments.WebsiteKey,
                UserAgent = arguments.UserAgent,
                ProxyConfig = arguments.ProxyConfig,
            };
        }

        public ReCaptchaV2Solver(string clientKey, IWebDriver driver, SolverConfig solverConfig) : base(clientKey, driver, solverConfig)
        {
        }
    }
}
