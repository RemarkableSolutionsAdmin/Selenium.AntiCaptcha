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
        protected override TurnstileCaptchaRequest BuildRequest(SolverAdditionalArguments additionalArguments)
        {
            return new TurnstileCaptchaRequest
            {
                WebsiteUrl = additionalArguments.Url,
                WebsiteKey = additionalArguments.SiteKey,
                UserAgent = additionalArguments.UserAgent,
                ProxyConfig = additionalArguments.ProxyConfig,
            };
        }

        public TurnstileCaptchaSolver(string clientKey, IWebDriver driver) : base(clientKey, driver)
        {
        }
    }
}
