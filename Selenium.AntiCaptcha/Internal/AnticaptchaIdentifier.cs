using System.Text.RegularExpressions;
using AntiCaptchaApi.Net.Models;
using AntiCaptchaApi.Net.Models.Solutions;
using OpenQA.Selenium;
using Selenium.AntiCaptcha.enums;
using Selenium.AntiCaptcha.Internal.Extensions;

namespace Selenium.AntiCaptcha.Internal;

internal static class AnticaptchaIdentifier
{
    internal static CaptchaType? IdentifyCaptcha<TSolution>(IWebDriver driver, IWebElement? imageElement, ProxyConfig? proxyConfig)
    {
        var result = IdentifyCaptchaOnSolutionType<TSolution>();

        if (!result.HasValue)
            return result;

        if (RecaptchaIdentifier.IsRecaptcha(result.Value))
        {
            result = RecaptchaIdentifier.SpecifyRecaptchaType(driver);
            return TransformToProxyCaptcha(result.Value, proxyConfig);
        }

        var proxyResult = TransformToProxyCaptcha(result.Value, proxyConfig);
            
        if (imageElement != null)
            return CaptchaType.ImageToText;

        return proxyResult;
    } 
        private static CaptchaType TransformToProxyCaptcha(CaptchaType originalType, ProxyConfig? proxyConfig)
        {
            if (proxyConfig == null)
            {
                return originalType;
            }

            return originalType switch
            {
                CaptchaType.ReCaptchaV2Proxyless => CaptchaType.ReCaptchaV2,
                CaptchaType.ReCaptchaV2EnterpriseProxyless => CaptchaType.ReCaptchaV2Enterprise,
                CaptchaType.ReCaptchaV2Enterprise => CaptchaType.ReCaptchaV2Enterprise,
                CaptchaType.ReCaptchaV3Proxyless => CaptchaType.ReCaptchaV3Enterprise,
                CaptchaType.ReCaptchaV2 => CaptchaType.ReCaptchaV2,
                CaptchaType.HCaptcha => CaptchaType.HCaptcha,
                CaptchaType.HCaptchaProxyless => CaptchaType.HCaptcha,
                CaptchaType.FunCaptcha => CaptchaType.FunCaptcha,
                CaptchaType.FunCaptchaProxyless => CaptchaType.FunCaptcha,
                CaptchaType.ImageToText => CaptchaType.ImageToText,
                CaptchaType.GeeTestV3 => CaptchaType.GeeTestV3,
                CaptchaType.GeeTestV4 => CaptchaType.GeeTestV4,
                CaptchaType.GeeTestV3Proxyless => CaptchaType.GeeTestV3,
                CaptchaType.GeeTestV4Proxyless => CaptchaType.GeeTestV4,
                CaptchaType.AntiGate => CaptchaType.AntiGate,
                CaptchaType.ReCaptchaV3Enterprise => CaptchaType.ReCaptchaV3Enterprise,
                _ => throw new ArgumentOutOfRangeException()
            };
        
        }

        private static CaptchaType? IdentifyCaptchaOnSolutionType<TSolution>()
        {
            return typeof(TSolution).Name switch
            {
                nameof(GeeTestV4Solution) => CaptchaType.GeeTestV4Proxyless,
                nameof(GeeTestV3Solution) => CaptchaType.GeeTestV3Proxyless,
                nameof(RecaptchaSolution) => CaptchaType.ReCaptchaV2Proxyless,
                nameof(HCaptchaSolution) => CaptchaType.HCaptchaProxyless,
                nameof(ImageToTextSolution) => CaptchaType.ImageToText,
                nameof(AntiGateSolution) => CaptchaType.AntiGate,
                nameof(FunCaptchaSolution) => CaptchaType.FunCaptchaProxyless,
                _ => null,
            };
        }
}