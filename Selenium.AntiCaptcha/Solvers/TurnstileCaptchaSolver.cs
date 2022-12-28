using AntiCaptchaApi.Net.Requests;
using OpenQA.Selenium;
using Selenium.AntiCaptcha.Models;
using Selenium.AntiCaptcha.Solvers.Base;

namespace Selenium.AntiCaptcha.Solvers
{
    internal class TurnstileCaptchaSolver : TurnstileSolverBase <TurnstileCaptchaRequest>
    {
        protected override TurnstileCaptchaRequest BuildRequest(SolverArguments arguments)
        {
            return new TurnstileCaptchaRequest
            {
                WebsiteUrl = arguments.WebsiteUrl,
                WebsiteKey = arguments.WebsiteKey,
                UserAgent = arguments.UserAgent,
                ProxyConfig = arguments.ProxyConfig,
            };
        }

        public TurnstileCaptchaSolver(string clientKey, IWebDriver driver, SolverConfig solverConfig) : base(clientKey, driver, solverConfig)
        {
        }
    }
}
