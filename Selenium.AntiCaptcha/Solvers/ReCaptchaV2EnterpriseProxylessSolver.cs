using AntiCaptchaApi.Net.Requests;
//using AntiCaptchaApi.Net.Requests.Abstractions.Interfaces;
using OpenQA.Selenium;
using Selenium.AntiCaptcha.Models;
using Selenium.AntiCaptcha.Solvers.Base;

namespace Selenium.AntiCaptcha.Solvers
{
    internal class ReCaptchaV2EnterpriseProxylessSolver : RecaptchaSolverBase <RecaptchaV2EnterpriseProxylessRequest>
    {
        protected override RecaptchaV2EnterpriseProxylessRequest BuildRequest(SolverArguments arguments)
        {
            //var x = (IRecaptchaV2EnterpriseProxylessRequest)arguments;
            return new RecaptchaV2EnterpriseProxylessRequest
            {
                WebsiteUrl = arguments.WebsiteUrl,
                WebsiteKey = arguments.WebsiteKey,
            };
        }

        public ReCaptchaV2EnterpriseProxylessSolver(string clientKey, IWebDriver driver, SolverConfig solverConfig) : base(clientKey, driver, solverConfig)
        {
        }
    }
}
