using System.Text.RegularExpressions;
using AntiCaptchaApi.Net.Models;
using AntiCaptchaApi.Net.Models.Solutions;
using AntiCaptchaApi.Net.Requests;
using Newtonsoft.Json.Linq;
using OpenQA.Selenium;
using Selenium.AntiCaptcha.Internal.Extensions;
using Selenium.AntiCaptcha.Models;
using Selenium.AntiCaptcha.Solvers.Base;

namespace Selenium.AntiCaptcha.Solvers
{
    internal class AntiGateSolver : Solver<AntiGateRequest, AntiGateSolution>
    {
        protected override AntiGateRequest BuildRequest(SolverAdditionalArguments additionalArguments)
        {
            return new AntiGateRequest
            {
                WebsiteUrl = additionalArguments.Url,
                TemplateName = additionalArguments.TemplateName,
                Variables = additionalArguments.Variables,
                DomainsOfInterest = additionalArguments.DomainsOfInterest,
                ProxyConfig = additionalArguments.ProxyConfig
            };
        }

        protected override async Task<SolverAdditionalArguments> FillMissingAdditionalArguments(
            SolverAdditionalArguments solverAdditionalArguments)
        {
            return await base.FillMissingAdditionalArguments(solverAdditionalArguments) with
            {
                Variables = solverAdditionalArguments.Variables ?? new JObject(),
                DomainsOfInterest = solverAdditionalArguments.DomainsOfInterest ?? new List<string>()
            };
        }

        public AntiGateSolver(string clientKey, IWebDriver driver) : base(clientKey, driver)
        {
        }
    }
}
