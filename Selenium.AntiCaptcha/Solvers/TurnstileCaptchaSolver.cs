using System.Text.RegularExpressions;
using AntiCaptchaApi.Net.Models.Solutions;
using AntiCaptchaApi.Net.Requests;
using OpenQA.Selenium;
using Selenium.AntiCaptcha.Internal.Extensions;
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
                WebsiteUrl = arguments.Url,
                WebsiteKey = arguments.SiteKey,
                UserAgent = arguments.UserAgent,
                ProxyConfig = arguments.ProxyConfig,
            };
        }

        public TurnstileCaptchaSolver(string clientKey, IWebDriver driver, SolverConfig solverConfig) : base(clientKey, driver, solverConfig)
        {
        }
    }
}
