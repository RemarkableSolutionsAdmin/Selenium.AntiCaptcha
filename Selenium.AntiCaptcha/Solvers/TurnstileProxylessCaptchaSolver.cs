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
        protected override TurnstileCaptchaProxylessRequest BuildRequest(SolverAdditionalArguments additionalArguments)
        {
            return new TurnstileCaptchaProxylessRequest
            {
                WebsiteUrl = additionalArguments.Url,
                WebsiteKey = additionalArguments.SiteKey
            };
        }

        public TurnstileProxylessCaptchaSolver(string clientKey, IWebDriver driver) : base(clientKey, driver)
        {
        }
    }
}
