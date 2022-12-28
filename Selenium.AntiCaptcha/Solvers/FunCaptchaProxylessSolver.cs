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
                WebsiteUrl = arguments.WebsiteUrl,
                WebsitePublicKey = arguments.WebsiteKey,
                Data = arguments.Data,
                FunCaptchaApiJsSubdomain = arguments.FunCaptchaApiJsSubdomain
            };
        }


        public FunCaptchaProxylessSolver(string clientKey, IWebDriver driver, SolverConfig solverConfig) : base(clientKey, driver, solverConfig)
        {
        }
    }
}
