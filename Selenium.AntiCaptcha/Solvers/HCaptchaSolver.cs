using AntiCaptchaApi.Net.Models.Solutions;
using AntiCaptchaApi.Net.Requests;
using OpenQA.Selenium;
using Selenium.AntiCaptcha.Models;
using Selenium.AntiCaptcha.Solvers.Base;

namespace Selenium.AntiCaptcha.Solvers
{
    internal class HCaptchaSolver : HCaptchaSolverBase<HCaptchaRequest>
    {
        protected override HCaptchaRequest BuildRequest(SolverArguments arguments)
        {
            return new HCaptchaRequest
            {
                WebsiteUrl = arguments.Url,
                WebsiteKey = arguments.SiteKey,
                UserAgent = arguments.UserAgent,
                ProxyConfig = arguments.ProxyConfig,
                IsInvisible = arguments.IsInvisible,
                EnterprisePayload = arguments.EnterprisePayload
            };
        }

        public HCaptchaSolver(string clientKey, IWebDriver driver, SolverConfig solverConfig) : base(clientKey, driver, solverConfig)
        {
        }
    }
}
