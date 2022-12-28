using AntiCaptchaApi.Net.Requests;
using OpenQA.Selenium;
using Selenium.AntiCaptcha.Models;
using Selenium.AntiCaptcha.Solvers.Base;

namespace Selenium.AntiCaptcha.Solvers
{
    internal class GeeTestV4Solver : GeeTestV4SolverBase<GeeTestV4Request>
    {
        protected override GeeTestV4Request BuildRequest(SolverArguments arguments)
        {
            return  new GeeTestV4Request
            {
                WebsiteUrl = arguments.WebsiteUrl,
                Gt = arguments.Gt,
                InitParameters = arguments.InitParameters,
                ProxyConfig = arguments.ProxyConfig,
                UserAgent = arguments.UserAgent,
                Challenge = arguments.Challenge
            };
        }
        
        public GeeTestV4Solver(string clientKey, IWebDriver driver, SolverConfig solverConfig) : base(clientKey, driver, solverConfig)
        {
        }
    }
}
