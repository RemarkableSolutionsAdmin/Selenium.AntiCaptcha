using AntiCaptchaApi.Net.Models;
using AntiCaptchaApi.Net.Models.Solutions;
using AntiCaptchaApi.Net.Requests.Abstractions;
using AntiCaptchaApi.Net.Responses;
using OpenQA.Selenium;
using Selenium.AntiCaptcha.enums;
using Selenium.AntiCaptcha.Internal;
using Selenium.AntiCaptcha.Internal.Helpers;
using Selenium.AntiCaptcha.solvers;

namespace Selenium.AntiCaptcha
{
    public static class IWebDriverExtensions
    {
        public static void SolveCaptcha(
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
            captchaType ??= AllCaptchaTypesIdentifier.IdentifyCaptcha<TSolution>(driver, imageElement, proxyConfig);
            
            if(!captchaType.HasValue)
            {
                throw new ArgumentNullException(nameof(captchaType), "Could not identify the captcha type from arguments. Please provide captchaType.");
            }
            
            ValidateSolutionOutputToCaptchaType<TSolution>(captchaType.Value);

            switch (captchaType.Value)
            {
                case CaptchaType.ReCaptchaV2:
                    return new ReCaptchaV2Solver().Solve(driver, clientKey, url, siteKey, responseElement, submitElement, imageElement,
                            userAgent, proxyConfig)
                        as TaskResultResponse<TSolution>;
                case CaptchaType.HCaptchaProxyless:
                    return new HCaptchaProxylessSolver().Solve(driver, clientKey, url, siteKey, responseElement, submitElement, imageElement,
                            userAgent, proxyConfig)
                        as TaskResultResponse<TSolution>;;
                case CaptchaType.HCaptcha:
                    return new HCaptchaSolver().Solve(driver, clientKey, url, siteKey, responseElement, submitElement, imageElement,
                            userAgent, proxyConfig)
                        as TaskResultResponse<TSolution>;
                case CaptchaType.FunCaptcha:
                    return new FunCaptchaSolver().Solve(driver, clientKey, url, siteKey, responseElement, submitElement, imageElement,
                            userAgent, proxyConfig)
                        as TaskResultResponse<TSolution>;
                case CaptchaType.GeeTestV3:
                    return new GeeTestV3Solver().Solve(driver, clientKey, url, siteKey, responseElement, submitElement, imageElement, userAgent, proxyConfig) 
                        as TaskResultResponse<TSolution>;
                case CaptchaType.GeeTestV4:
                    return new GeeTestV4Solver().Solve(driver, clientKey, url, siteKey, responseElement, submitElement, imageElement, userAgent, proxyConfig) 
                        as TaskResultResponse<TSolution>;
                case CaptchaType.ImageToText:
                    return new ImageToTextSolver().Solve(driver, clientKey, url, siteKey, responseElement, submitElement, imageElement, userAgent, proxyConfig)
                        as TaskResultResponse<TSolution>;
                default:
                    throw new ArgumentOutOfRangeException(nameof(captchaType), captchaType, null);
            }
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