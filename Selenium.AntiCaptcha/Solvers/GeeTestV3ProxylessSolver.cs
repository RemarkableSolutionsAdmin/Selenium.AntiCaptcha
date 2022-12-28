using AntiCaptchaApi.Net.Requests;
using AntiCaptchaApi.Net.Requests.Abstractions.Interfaces;
using OpenQA.Selenium;
using Selenium.AntiCaptcha.Models;
using Selenium.AntiCaptcha.Solvers.Base;

namespace Selenium.AntiCaptcha.Solvers
{
    internal class GeeTestV3ProxylessSolver : GeeTestV3SolverBase<IGeeTestV3ProxylessRequest>
    {
        protected override IGeeTestV3ProxylessRequest BuildRequest(SolverArguments arguments)
        {
            return new GeeTestV3ProxylessRequest(arguments);
        }

        public GeeTestV3ProxylessSolver(string clientKey, IWebDriver driver, SolverConfig solverConfig) : base(clientKey, driver, solverConfig)
        {
        }
    }
}
