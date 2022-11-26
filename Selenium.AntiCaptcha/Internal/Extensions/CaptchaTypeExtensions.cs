using AntiCaptchaApi.Net.Models.Solutions;
using Selenium.AntiCaptcha.Enums;

namespace Selenium.AntiCaptcha.Internal.Extensions;

public static class CaptchaTypeExtensions
{
    private static readonly List<CaptchaType> ProxyCaptchaTypes = new()
    {
        CaptchaType.FunCaptcha,
        CaptchaType.HCaptcha,
        CaptchaType.GeeTestV3,
        CaptchaType.GeeTestV4,
        CaptchaType.ReCaptchaV2,
        CaptchaType.ReCaptchaV2Enterprise
    };
    
    
    private static readonly List<CaptchaType> ProxylessCaptchaTypes = new()
    {
        CaptchaType.ReCaptchaV2Proxyless,
        CaptchaType.ReCaptchaV2EnterpriseProxyless,
        CaptchaType.ReCaptchaV3Proxyless,
        CaptchaType.ReCaptchaV3Enterprise,
        CaptchaType.HCaptchaProxyless,
        CaptchaType.FunCaptchaProxyless,
        CaptchaType.ImageToText,
        CaptchaType.GeeTestV3Proxyless,
        CaptchaType.GeeTestV4Proxyless,
        CaptchaType.AntiGate
    };

    internal static bool IsProperlyDefined(this CaptchaType type)
    {
        return ProxyCaptchaTypes.Contains(type) || ProxylessCaptchaTypes.Contains(type);
    }
    
    
    public static CaptchaType ToProxyType(this CaptchaType type)
    {
        return type switch
        {
            CaptchaType.ReCaptchaV2Proxyless => CaptchaType.ReCaptchaV2,
            CaptchaType.ReCaptchaV2EnterpriseProxyless => CaptchaType.ReCaptchaV2Enterprise,
            CaptchaType.ReCaptchaV2Enterprise => CaptchaType.ReCaptchaV2Enterprise,
            CaptchaType.ReCaptchaV3Proxyless => CaptchaType.ReCaptchaV3Proxyless,
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
    
    public static bool IsProxyType(this CaptchaType captchaType)
    {
        return ProxyCaptchaTypes.Contains(captchaType);
    }
    
    public static bool IsProxylessType(this CaptchaType captchaType)
    {
        return ProxylessCaptchaTypes.Contains(captchaType);
    }
    
    public static Type GetSolutionType(this CaptchaType captchaType)
    {
        return captchaType switch
        {
            CaptchaType.ReCaptchaV2 => typeof(RecaptchaSolution),
            CaptchaType.ReCaptchaV2Proxyless => typeof(RecaptchaSolution),
            CaptchaType.ReCaptchaV2Enterprise => typeof(RecaptchaSolution),
            CaptchaType.ReCaptchaV2EnterpriseProxyless => typeof(RecaptchaSolution),
            CaptchaType.ReCaptchaV3Proxyless => typeof(RecaptchaSolution),
            CaptchaType.ReCaptchaV3Enterprise => typeof(RecaptchaSolution),
            CaptchaType.HCaptcha => typeof(HCaptchaSolution),
            CaptchaType.HCaptchaProxyless => typeof(HCaptchaSolution),
            CaptchaType.FunCaptcha => typeof(FunCaptchaSolution),
            CaptchaType.FunCaptchaProxyless => typeof(FunCaptchaSolution),
            CaptchaType.ImageToText => typeof(ImageToTextSolution),
            CaptchaType.GeeTestV3 => typeof(GeeTestV3Solution),
            CaptchaType.GeeTestV3Proxyless => typeof(GeeTestV3Solution),
            CaptchaType.GeeTestV4 => typeof(GeeTestV4Solution),
            CaptchaType.GeeTestV4Proxyless => typeof(GeeTestV4Solution),
            CaptchaType.AntiGate => typeof(AntiGateSolution),
            _ => throw new ArgumentOutOfRangeException(nameof(captchaType), captchaType, null)
        };
    }

}