﻿using OpenQA.Selenium;
using System.Text.RegularExpressions;
using AntiCaptchaApi.Net;
using AntiCaptchaApi.Net.Enums;
using AntiCaptchaApi.Net.Models;
using AntiCaptchaApi.Net.Models.Solutions;
using AntiCaptchaApi.Net.Requests;
using AntiCaptchaApi.Net.Responses;

namespace Selenium.AntiCaptcha.solvers
{
    internal class GeeTestV3Solver : Solver<GeeTestV3ProxylessRequest, GeeTestV3Solution>
    {
        protected override string GetSiteKey(IWebDriver driver, int waitingTime = 1000)
        {
            var regex = new Regex("gt=(.*?)&");
            var gt = regex.Match(driver.PageSource).Groups[1].Value;

            if (!string.IsNullOrEmpty(gt))
                return gt;
            
            
            regex = new Regex("captcha_id=(.*?)&");
            var captchaRegexGroups = regex.Match(driver.PageSource).Groups;

            return captchaRegexGroups[1].Value;
        }

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

        protected override void FillResponseElement(IWebDriver driver, GeeTestV3Solution solution, IWebElement? responseElement)
        {
            throw new NotImplementedException();
        }

        private string GetChallenge(IWebDriver driver)
        {
            var regex = new Regex("challenge=(.*?)&");
            return regex.Match(driver.PageSource).Groups[1].Value;
        }
    }
}
