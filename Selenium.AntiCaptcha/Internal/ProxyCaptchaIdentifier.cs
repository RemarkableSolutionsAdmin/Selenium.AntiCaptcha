using AntiCaptchaApi.Net.Models;
using OpenQA.Selenium;
using Selenium.AntiCaptcha.Enums;

namespace Selenium.AntiCaptcha.Internal;

public abstract class ProxyCaptchaIdentifier : ICaptchaIdentifier
{
    protected readonly List<CaptchaType> IdentifableTypes = new();
    
    public bool CanIdentify(CaptchaType type)
    {
        return IdentifableTypes.Contains(type);
    }

    public virtual CaptchaType? Identify(IWebDriver driver, ProxyConfig? proxyConfig)
    {
        return null;
    }

    public virtual CaptchaType? SpecifyCaptcha(CaptchaType originalType, IWebDriver driver, ProxyConfig? proxyConfig)
    {
        if (proxyConfig == null || string.IsNullOrEmpty(proxyConfig.ProxyAddress))
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
}