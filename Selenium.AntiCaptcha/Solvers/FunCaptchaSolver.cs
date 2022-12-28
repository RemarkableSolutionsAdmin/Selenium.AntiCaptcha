using AntiCaptchaApi.Net.Requests;
using OpenQA.Selenium;
using Selenium.AntiCaptcha.Models;
using Selenium.AntiCaptcha.Solvers.Base;

namespace Selenium.AntiCaptcha.Solvers
{
    internal class FunCaptchaSolver : FunCaptchaSolverBase<FunCaptchaRequest>
    {
        protected override FunCaptchaRequest BuildRequest(SolverArguments arguments)
        {
            return new FunCaptchaRequest
            {
                WebsiteUrl = arguments.WebsiteUrl,
                WebsitePublicKey = arguments.WebsitePublicKey,
                FunCaptchaApiJsSubdomain = arguments.FunCaptchaApiJsSubdomain,
                Data = arguments.Data,
                UserAgent = arguments.UserAgent,
                ProxyConfig = arguments.ProxyConfig
            };
        }

        public FunCaptchaSolver(string clientKey, IWebDriver driver, SolverConfig solverConfig) : base(clientKey, driver, solverConfig)
        {
        }
    }
}
