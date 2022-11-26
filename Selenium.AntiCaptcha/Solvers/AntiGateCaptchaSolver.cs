using System.Text.RegularExpressions;
using AntiCaptchaApi.Net.Models;
using AntiCaptchaApi.Net.Models.Solutions;
using AntiCaptchaApi.Net.Requests;
using Newtonsoft.Json.Linq;
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
    }
}
