using System.Text.RegularExpressions;
using AntiCaptchaApi.Net.Models.Solutions;
using AntiCaptchaApi.Net.Requests;
using OpenQA.Selenium;
using Selenium.AntiCaptcha.Internal.Extensions;
using Selenium.AntiCaptcha.Models;
using Selenium.AntiCaptcha.Solvers.Base;

namespace Selenium.AntiCaptcha.Solvers
{
    internal class GeeTestV4Solver : GeeTestV4SolverBase<GeeTestV4Request>
    {
        protected override GeeTestV4Request BuildRequest(SolverAdditionalArguments additionalArguments)
        {
            return  new GeeTestV4Request
            {
                WebsiteUrl = additionalArguments.Url,
                Gt = additionalArguments.Gt,
                InitParameters = additionalArguments.InitParameters,
                ProxyConfig = additionalArguments.ProxyConfig,
                UserAgent = additionalArguments.UserAgent,
                Challenge = additionalArguments.Challenge
            };
        }
        
        public GeeTestV4Solver(string clientKey, IWebDriver driver) : base(clientKey, driver)
        {
        }
    }
}
