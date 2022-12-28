using AntiCaptchaApi.Net.Requests;
using AntiCaptchaApi.Net.Requests.Abstractions.Interfaces;
using OpenQA.Selenium;
using Selenium.AntiCaptcha.Models;
using Selenium.AntiCaptcha.Solvers.Base;

namespace Selenium.AntiCaptcha.Solvers
{
    internal class HCaptchaSolver : HCaptchaSolverBase<IHCaptchaRequest>
    {
        protected override IHCaptchaRequest BuildRequest(SolverArguments arguments)
        {
            return new HCaptchaRequest(arguments);
        }

        public HCaptchaSolver(string clientKey, IWebDriver driver, SolverConfig solverConfig) : base(clientKey, driver, solverConfig)
        {
        }
    }
}
