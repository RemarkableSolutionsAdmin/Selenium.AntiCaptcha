using AntiCaptchaApi.Net.Models;
using AntiCaptchaApi.Net.Models.Solutions;
using AntiCaptchaApi.Net.Requests.Abstractions;
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
            ProxyConfig? proxyConfig = null)
        where TSolution : BaseSolution, new()
        {
            if (captchaType == null)
            {
                captchaType = IdentifyCaptcha<TSolution>(driver, imageElement, proxyConfig);
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
                case CaptchaType.GeeTestV3:
                    return new GeeTestV3Solver()
                        .Solve(driver, clientKey, url, siteKey, responseElement, submitElement, imageElement, userAgent, proxyConfig) as TaskResultResponse<TSolution>;
                case CaptchaType.ImageToText:
                    return new ImageToTextSolver()
                        .Solve(driver, clientKey, url, siteKey, responseElement, submitElement, imageElement, userAgent, proxyConfig) as TaskResultResponse<TSolution>;
                default:
                    throw new Exception("Not supported captchaType");
            }
        }

        private static CaptchaType? IdentifyCaptcha<TSolution>(IWebDriver driver, IWebElement? imageElement, ProxyConfig? proxyConfig)
        {
            var result = IdentifyCaptchaOnSolutionType<TSolution>();
            //TODO! RecaptchaSoltuion has 4 CaptchaTypes.

            if (result.HasValue && result.Value == CaptchaType.ReCaptchaV2Proxyless)
            {
                
            }
            
            var proxyResult = IdentifyCaptchaOnProxy(result, proxyConfig);
            
            if (imageElement != null)
                return CaptchaType.ImageToText;

            return proxyResult;
        }

        private static CaptchaType SpecifyWhichRecaptchaItIs(IWebDriver driver)
        {
            return CaptchaType.ReCaptchaV2Proxyless;
        }

        private static CaptchaType? IdentifyCaptchaOnProxy(CaptchaType? type, ProxyConfig? proxyConfig)
        {
            if (type != null && proxyConfig != null)
            {
                switch (type.Value)
                {
                    case CaptchaType.ReCaptchaV2Proxyless:
                        return CaptchaType.ReCaptchaV2;
                    case CaptchaType.ReCaptchaV2EnterpriseProxyless:
                        return CaptchaType.ReCaptchaV2Enterprise;
                    case CaptchaType.FunCaptchaProxyless:
                        return CaptchaType.ReCaptchaV2;
                    case CaptchaType.GeeTestV3Proxyless:
                        return CaptchaType.GeeTestV3;
                    case CaptchaType.GeeTestV4Proxyless:
                        return CaptchaType.GeeTestV4;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }

            return type;
        }

        private static CaptchaType? IdentifyCaptchaOnSolutionType<TSolution>()
        {
            switch (typeof(TSolution).Name)
            {
                case nameof(GeeTestV4Solution):
                    return CaptchaType.GeeTestV4Proxyless;
                case nameof(GeeTestV3Solution):
                    return CaptchaType.GeeTestV3Proxyless;
                case nameof(RecaptchaSolution):
                    return CaptchaType.ReCaptchaV2Proxyless;
                case nameof(HCaptchaSolution):
                    return CaptchaType.HCaptchaProxyless;
                case nameof(ImageToTextSolution):
                    return CaptchaType.ImageToText;
                case nameof(AntiGateSolution):
                    return CaptchaType.AntiGate;
                case nameof(FunCaptchaSolution):
                    return CaptchaType.FunCaptchaProxyless;
                default:
                    return null;
            }
        }
    }
}