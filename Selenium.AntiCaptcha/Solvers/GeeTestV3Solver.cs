﻿using System.Text.RegularExpressions;
using AntiCaptchaApi.Net.Models;
using AntiCaptchaApi.Net.Models.Solutions;
using AntiCaptchaApi.Net.Requests;
using OpenQA.Selenium;
using Selenium.AntiCaptcha.Internal.Extensions;
using Selenium.AntiCaptcha.Solvers.Base;

namespace Selenium.AntiCaptcha.Solvers
{
    internal class GeeTestV3Solver : Solver<GeeTestV3Request, GeeTestV3Solution>
    {
        protected override GeeTestV3Request BuildRequest(IWebDriver driver, string? url, string? siteKey, IWebElement? imageElement, string? userAgent, ProxyConfig proxyConfig)
        {
            var challenge = GetChallenge(driver);
            return  new GeeTestV3Request
            {
                WebsiteUrl = url ?? driver.Url,
                Challenge = challenge,
                GeetestApiServerSubdomain = null,
                GeetestGetLib = null,
                Gt = siteKey,
                ProxyConfig = proxyConfig,
                UserAgent = userAgent ?? Constants.AnticaptchaDefaultValues.UserAgent
            };
        }
        private static string GetChallenge(IWebDriver driver)
        {
            var pageSource = driver.GetAllPageSource();
            var regex = new Regex("challenge=(.*?)&");
            return regex.Match(pageSource).Groups[1].Value;
        }
    }
}
