using System.Text.RegularExpressions;
using AntiCaptchaApi.Net.Models;
using AntiCaptchaApi.Net.Models.Solutions;
using AntiCaptchaApi.Net.Requests;
using OpenQA.Selenium;
using Selenium.AntiCaptcha.Internal.Extensions;
using Selenium.AntiCaptcha.Models;
using Selenium.AntiCaptcha.Solvers.Base;

namespace Selenium.AntiCaptcha.Solvers
{
    internal class FunCaptchaSolver : FunCaptchaSolverBase<FunCaptchaRequest, FunCaptchaSolution>
    {

        protected override FunCaptchaRequest BuildRequest(SolverAdditionalArguments additionalArguments)
        {
            return new FunCaptchaRequest
            {
                WebsiteUrl = additionalArguments.Url,
                WebsitePublicKey = additionalArguments.SiteKey,
                FunCaptchaApiJsSubdomain = additionalArguments.FunCaptchaApiJsSubdomain,
                Data = additionalArguments.Data,
                UserAgent = additionalArguments.UserAgent,
                ProxyConfig = additionalArguments.ProxyConfig
            };
        }

        public FunCaptchaSolver(string clientKey, IWebDriver driver) : base(clientKey, driver)
        {
        }
    }
}
