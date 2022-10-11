using AntiCaptchaApi.Net.Models;
using AntiCaptchaApi.Net.Models.Solutions;
using AntiCaptchaApi.Net.Responses;
using OpenQA.Selenium;
using Selenium.AntiCaptcha.enums;
using Selenium.AntiCaptcha.solvers;

namespace Selenium.AntiCaptcha
{
    public static class IWebDriverExtensions
    {
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
            ProxyConfig proxyConfig = null)
        where TSolution : BaseSolution, new()
        {
            if (captchaType == null)
            {
                captchaType = IdentifyCaptcha(driver);
            }
            
            if(!captchaType.HasValue)
            {
                throw new ArgumentNullException(nameof(captchaType));
            }
            //TODO: TSolution must be of right Type
            
            switch (captchaType.Value)
            {
                case CaptchaType.ReCaptchaV2:
                    return new ReCaptchaV2Solver()
                        .Solve(driver, clientKey, url, siteKey, responseElement, submitElement, imageElement, userAgent, proxyConfig) as TaskResultResponse<TSolution>;
                case CaptchaType.HCaptchaProxyless:
                    return new HCaptchaProxylessSolver()
                        .Solve(driver, clientKey, url, siteKey, responseElement, submitElement, imageElement, userAgent, proxyConfig) as TaskResultResponse<TSolution>;
                case CaptchaType.HCaptcha:
                    return new HCaptchaSolver()
                        .Solve(driver, clientKey, url, siteKey, responseElement, submitElement, imageElement, userAgent, proxyConfig) as TaskResultResponse<TSolution>;
                    break;
                case CaptchaType.FunCaptcha:
                    return new FunCaptchaSolver()
                        .Solve(driver, clientKey, url, siteKey, responseElement, submitElement, imageElement, userAgent, proxyConfig) as TaskResultResponse<TSolution>;
                    break;
                case CaptchaType.GeeTest:
                    return new GeeTestV3Solver()
                        .Solve(driver, clientKey, url, siteKey, responseElement, submitElement, imageElement, userAgent, proxyConfig) as TaskResultResponse<TSolution>;
                case CaptchaType.ImageToText:
                    return new ImageToTextSolver()
                        .Solve(driver, clientKey, url, siteKey, responseElement, submitElement, imageElement, userAgent, proxyConfig) as TaskResultResponse<TSolution>;
                default:
                    throw new Exception("Not supported captchaType");
            }
        }

        private static CaptchaType? IdentifyCaptcha(IWebDriver driver)
        {
            //TODO: identify captcha type
            throw new NotImplementedException();
        }
    }
}