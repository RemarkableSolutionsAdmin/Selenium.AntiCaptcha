using AntiCaptchaApi.Net.Models.Solutions;
using AntiCaptchaApi.Net.Requests;
using AntiCaptchaApi.Net.Requests.Abstractions.Interfaces;
using OpenQA.Selenium;
using Selenium.AntiCaptcha.Models;
using Selenium.AntiCaptcha.Solvers.Base;

namespace Selenium.AntiCaptcha.Solvers
{
    internal class AntiGateSolver : Solver<IAntiGateRequest, AntiGateSolution>
    {
        protected override IAntiGateRequest BuildRequest(SolverArguments arguments)
        {
            return new AntiGateRequest(arguments);
        }

        protected override async Task<SolverArguments> FillMissingSolverArguments(
            SolverArguments solverArguments)
        {
            return await base.FillMissingSolverArguments(solverArguments) with
            {
                Variables = solverArguments.Variables,
                DomainsOfInterest = solverArguments.DomainsOfInterest
            };
        }

        public AntiGateSolver(string clientKey, IWebDriver driver, SolverConfig solverConfig) : base(clientKey, driver, solverConfig)
        {
        }
    }
}
