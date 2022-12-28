using AntiCaptchaApi.Net.Models.Solutions;
using AntiCaptchaApi.Net.Requests;
using OpenQA.Selenium;
using Selenium.AntiCaptcha.Models;
using Selenium.AntiCaptcha.Solvers.Base;

namespace Selenium.AntiCaptcha.Solvers
{
    internal class AntiGateSolver : Solver<AntiGateRequest, AntiGateSolution>
    {
        protected override AntiGateRequest BuildRequest(SolverArguments arguments)
        { 
            return new AntiGateRequest
            {
                WebsiteUrl = arguments.WebsiteUrl,
                TemplateName = arguments.TemplateName,
                Variables = arguments.Variables,
                DomainsOfInterest = arguments.DomainsOfInterest,
                ProxyConfig = arguments.ProxyConfig
            };
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
