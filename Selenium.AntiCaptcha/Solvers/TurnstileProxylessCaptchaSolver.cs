using System.Text.RegularExpressions;
using AntiCaptchaApi.Net.Models.Solutions;
using AntiCaptchaApi.Net.Requests;
using OpenQA.Selenium;
using Selenium.AntiCaptcha.Internal.Extensions;
using Selenium.AntiCaptcha.Models;
using Selenium.AntiCaptcha.Solvers.Base;

namespace Selenium.AntiCaptcha.Solvers
{
    internal class TurnstileProxylessCaptchaSolver : TurnstileSolverBase <TurnstileCaptchaProxylessRequest>
    {
        protected override TurnstileCaptchaProxylessRequest BuildRequest(SolverArguments arguments)
        {
            return new TurnstileCaptchaProxylessRequest
            {
                WebsiteUrl = arguments.Url,
                WebsiteKey = arguments.SiteKey
            };
        }

        public TurnstileProxylessCaptchaSolver(string clientKey, IWebDriver driver, SolverConfig solverConfig) : base(clientKey, driver, solverConfig)
        {
        }
    }
}
