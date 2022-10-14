using System.Text.RegularExpressions;
using AntiCaptchaApi.Net.Models;
using AntiCaptchaApi.Net.Models.Solutions;
using AntiCaptchaApi.Net.Requests;
using Newtonsoft.Json.Linq;
using OpenQA.Selenium;
using Selenium.AntiCaptcha.Internal.Extensions;
using Selenium.AntiCaptcha.Solvers.Base;

namespace Selenium.AntiCaptcha.Solvers
{
    internal class AntiGateSolver : Solver<AntiGateRequest, AntiGateSolution>
    {
        protected override AntiGateRequest BuildRequest(IWebDriver driver, string? url, string? siteKey, IWebElement? imageElement, string? userAgent, ProxyConfig proxyConfig)
        {
            return new AntiGateRequest
            {
                WebsiteUrl = url ?? driver.Url,
                TemplateName = "CloudFlare cookies for a proxy", //TODO.
                Variables = new JObject(), //TODO.
                DomainsOfInterest = new List<string>
                {
                    "anything"  
                }, //TODO.
                ProxyConfig = proxyConfig
            };
        }
    }
}
