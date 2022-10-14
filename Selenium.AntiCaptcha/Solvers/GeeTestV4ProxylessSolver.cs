using System.Text.RegularExpressions;
using AntiCaptchaApi.Net.Models;
using AntiCaptchaApi.Net.Models.Solutions;
using AntiCaptchaApi.Net.Requests;
using OpenQA.Selenium;
using Selenium.AntiCaptcha.Solvers.Base;

namespace Selenium.AntiCaptcha.Solvers
{
    internal class GeeTestV4ProxylessSolver : Solver<GeeTestV4ProxylessRequest, GeeTestV4Solution>
    {
        protected override GeeTestV4ProxylessRequest BuildRequest(IWebDriver driver, string? url, string? siteKey, IWebElement? imageElement, string? userAgent, ProxyConfig proxyConfig)
        {
            siteKey ??= GetSiteKey(driver);
            var challenge = GetChallenge(driver);
            return  new GeeTestV4ProxylessRequest
            {
                WebsiteUrl = url ?? driver.Url,
                Challenge = challenge,
                Gt = siteKey,
                //TODO!
                // InitParameters = new Dictionary<string, string>()
                // {
                //     {"riskType", "slide"}
                // }
            };
        }

        private string GetChallenge(IWebDriver driver)
        {
            var regex = new Regex("challenge=(.*?)&");
            return regex.Match(driver.PageSource).Groups[1].Value;
        }
    }
}
