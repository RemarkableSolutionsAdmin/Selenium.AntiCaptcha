using AntiCaptchaApi.Net.Requests;
using AntiCaptchaApi.Net.Requests.Abstractions.Interfaces;
using OpenQA.Selenium;
using Selenium.AntiCaptcha.Models;
using Selenium.AntiCaptcha.Solvers.Base;

namespace Selenium.AntiCaptcha.Solvers
{
    internal class TurnstileProxylessCaptchaSolver : TurnstileSolverBase <ITurnstileCaptchaProxylessRequest>
    {
        protected override ITurnstileCaptchaProxylessRequest BuildRequest(SolverArguments arguments) => 
            new TurnstileCaptchaProxylessRequest(arguments);

        public TurnstileProxylessCaptchaSolver(string clientKey, IWebDriver driver, SolverConfig solverConfig) : base(clientKey, driver, solverConfig)
        {
        }
    }
}
