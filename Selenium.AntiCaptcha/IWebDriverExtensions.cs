﻿using AntiCaptchaApi.Net.Models;
using AntiCaptchaApi.Net.Models.Solutions;
using AntiCaptchaApi.Net.Responses;
using AntiCaptchaApi.Net.Responses.Abstractions;
using OpenQA.Selenium;
using Selenium.AntiCaptcha.Enums;
using Selenium.AntiCaptcha.Internal;
using Selenium.AntiCaptcha.Internal.Extensions;

namespace Selenium.AntiCaptcha
{
    public static class IWebDriverExtensions
    {
        private static int IdentifyRetryThreshold = 3;
        private static int IdentifyRetryWaitTimeMs = 2000;

        public static BaseResponse SolveCaptcha(
            this IWebDriver driver,
            string clientKey,
            CaptchaType? captchaType = null,
            string? url = null,
            string? siteKey = null,
            IWebElement? responseElement = null,
            IWebElement? submitElement = null,
            IWebElement? imageElement = null,
            string? userAgent = null,
            ProxyConfig? proxyConfig = null)
        {
            if (captchaType == null)
            {
                var identifiedCaptchaTypes = AllCaptchaTypesIdentifier.Identify(driver, proxyConfig);
                if (identifiedCaptchaTypes.Count != 1)
                {
                    throw new Exception("Unable to idenfity captcha");
                }
                captchaType = identifiedCaptchaTypes.First();
            }

            dynamic solver = SolverFactory.GetSolver(captchaType.Value);
            return solver.Solve(driver, clientKey, url, siteKey, responseElement, submitElement, imageElement, userAgent, proxyConfig);

        }


        public static TaskResultResponse<TSolution>? SolveCaptcha<TSolution>(
            this IWebDriver driver,
            string clientKey,
            CaptchaType? captchaType = null,
            string? url = null,
            string? siteKey = null,
            IWebElement? responseElement = null,
            IWebElement? submitElement = null,
            IWebElement? imageElement = null,
            string? userAgent = null,
            ProxyConfig? proxyConfig = null)
                where TSolution : BaseSolution, new()
        {
            captchaType ??= AllCaptchaTypesIdentifier.IdentifyCaptcha<TSolution>(driver, proxyConfig);

            if (!captchaType.HasValue)
            {
                throw new ArgumentNullException(nameof(captchaType), "Could not identify the captcha type from arguments. Please provide captchaType.");
            }

            ValidateSolutionOutputToCaptchaType<TSolution>(captchaType.Value);
            var solver = SolverFactory.GetSolver<TSolution>(captchaType.Value);

            return solver.Solve(driver, clientKey, url, siteKey, responseElement, submitElement, imageElement, userAgent, proxyConfig);

        }

        private static void ValidateSolutionOutputToCaptchaType<TSolution>(CaptchaType captchaType)
            where TSolution : BaseSolution, new()
        {
            var wrongSolutionTypeMessage = $"Wrong solution type. Was trying to create solution of type: {typeof(TSolution)} with captcha type: {captchaType}";
            var captchaSolutionType = captchaType.GetSolutionType();

            if (typeof(TSolution) != captchaSolutionType)
            {
                throw new ArgumentException(wrongSolutionTypeMessage);
            }
        }
    }
}