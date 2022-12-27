using AntiCaptchaApi.Net.Models.Solutions;
using AntiCaptchaApi.Net.Requests;
using OpenQA.Selenium;
using Selenium.AntiCaptcha.Models;
using Selenium.AntiCaptcha.Solvers.Base;

namespace Selenium.AntiCaptcha.Solvers
{
    public class FunCaptchaProxylessSolver : FunCaptchaSolverBase<FunCaptchaProxylessRequest>
    {
        protected override FunCaptchaProxylessRequest BuildRequest(SolverArguments arguments)
        {
            return new FunCaptchaProxylessRequest
            {
                WebsiteUrl = arguments.Url,
                WebsitePublicKey = arguments.SiteKey,
                Data = arguments.Data,
                FunCaptchaApiJsSubdomain = arguments.FunCaptchaApiJsSubdomain
            };
        }


        public FunCaptchaProxylessSolver(string clientKey, IWebDriver driver, SolverConfig solverConfig) : base(clientKey, driver, solverConfig)
        {
        }
    }
}
