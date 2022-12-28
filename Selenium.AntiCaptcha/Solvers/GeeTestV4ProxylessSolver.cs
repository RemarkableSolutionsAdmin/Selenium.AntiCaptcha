using AntiCaptchaApi.Net.Requests;
using AntiCaptchaApi.Net.Requests.Abstractions.Interfaces;
using OpenQA.Selenium;
using Selenium.AntiCaptcha.Models;
using Selenium.AntiCaptcha.Solvers.Base;

namespace Selenium.AntiCaptcha.Solvers
{
    internal class GeeTestV4ProxylessSolver : GeeTestV4SolverBase<IGeeTestV4ProxylessRequest>
    {
        protected override IGeeTestV4ProxylessRequest BuildRequest(SolverArguments arguments)
        {
            return new GeeTestV4ProxylessRequest(arguments);
        }

        public GeeTestV4ProxylessSolver(string clientKey, IWebDriver driver, SolverConfig solverConfig) : base(clientKey, driver, solverConfig)
        {
        }
    }
}
