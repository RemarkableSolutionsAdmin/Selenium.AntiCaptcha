using AntiCaptchaApi.Net.Internal.Validation.Validators;
using AntiCaptchaApi.Net.Models.Solutions;
using AntiCaptchaApi.Net.Requests;
using AntiCaptchaApi.Net.Requests.Abstractions;
using Selenium.AntiCaptcha.Enums;

namespace Selenium.AntiCaptcha.Internal.Extensions;

public static class CaptchaRequestExtensions
{
    public static CaptchaType GetCaptchaType<TSolution>(this CaptchaRequest<TSolution> request)
        where TSolution : BaseSolution, new()
    {
        var @switch = new Dictionary<Type, CaptchaType> {
            { typeof(AntiGateRequest), CaptchaType.AntiGate },
            { typeof(FunCaptchaRequest), CaptchaType.FunCaptcha },
            { typeof(FunCaptchaProxylessRequest), CaptchaType.FunCaptchaProxyless},
            { typeof(GeeTestV3Request), CaptchaType.GeeTestV3},
            { typeof(GeeTestV3ProxylessRequest), CaptchaType.GeeTestV3Proxyless},
            { typeof(GeeTestV4Request), CaptchaType.GeeTestV4},
            { typeof(GeeTestV4ProxylessRequest), CaptchaType.GeeTestV4Proxyless },
            { typeof(HCaptchaProxylessRequest), CaptchaType.HCaptchaProxyless },
            { typeof(HCaptchaRequest), CaptchaType.HCaptcha },
            { typeof(ImageToTextRequest), CaptchaType.ImageToText },
            { typeof(RecaptchaV2EnterpriseProxylessRequest), CaptchaType.ReCaptchaV2EnterpriseProxyless},
            { typeof(RecaptchaV2EnterpriseRequest), CaptchaType.ReCaptchaV2Enterprise },
            { typeof(RecaptchaV2ProxylessRequest), CaptchaType.ReCaptchaV2Proxyless },
            { typeof(RecaptchaV2Request), CaptchaType.ReCaptchaV2 },
            { typeof(RecaptchaV3Request), CaptchaType.ReCaptchaV3 },
            { typeof(RecaptchaV3EnterpriseRequest), CaptchaType.ReCaptchaV3Enterprise},
            { typeof(TurnstileCaptchaProxylessRequest), CaptchaType.TurnstileProxyless },
            { typeof(TurnstileCaptchaRequest), CaptchaType.Turnstile },
        };
        return @switch[request.GetType()];
    }
}