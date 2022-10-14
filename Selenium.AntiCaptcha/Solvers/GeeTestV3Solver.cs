using OpenQA.Selenium;
using System.Text.RegularExpressions;
using AntiCaptchaApi.Net.Models;
using AntiCaptchaApi.Net.Models.Solutions;
using AntiCaptchaApi.Net.Requests;

namespace Selenium.AntiCaptcha.solvers
{
    internal class GeeTestV3Solver : Solver<GeeTestV3ProxylessRequest, GeeTestV3Solution>
    {
        protected override GeeTestV3ProxylessRequest BuildRequest(IWebDriver driver, string? url, string? siteKey, IWebElement? imageElement, string? userAgent, ProxyConfig proxyConfig)
        {
            siteKey ??= GetSiteKey(driver);
            var challenge = GetChallenge(driver);
            return  new GeeTestV3ProxylessRequest
            {
                WebsiteUrl = url ?? driver.Url,
                Challenge = challenge,
                Gt = siteKey
            };
        }
        private static string GetChallenge(IWebDriver driver)
        {
            var regex = new Regex("challenge=(.*?)&");
            return regex.Match(driver.PageSource).Groups[1].Value;
        }
    }
}
