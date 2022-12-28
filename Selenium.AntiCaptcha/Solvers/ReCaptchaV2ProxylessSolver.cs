using AntiCaptchaApi.Net.Requests;
using OpenQA.Selenium;
using Selenium.AntiCaptcha.Models;
using Selenium.AntiCaptcha.Solvers.Base;

namespace Selenium.AntiCaptcha.Solvers
{
    internal class ReCaptchaV2ProxylessSolver : RecaptchaSolverBase <RecaptchaV2ProxylessRequest>
    {
        protected override RecaptchaV2ProxylessRequest BuildRequest(SolverArguments arguments)
        {
            return new RecaptchaV2ProxylessRequest
            {
                WebsiteUrl = arguments.WebsiteUrl,
                WebsiteKey = arguments.WebsiteKey
            };
        }

        public ReCaptchaV2ProxylessSolver(string clientKey, IWebDriver driver, SolverConfig solverConfig) : base(clientKey, driver, solverConfig)
        {
        }
    }
}
