using AntiCaptchaApi.Net.Models.Solutions;
using AntiCaptchaApi.Net.Requests;
using Selenium.AntiCaptcha.Enums;

namespace Selenium.AntiCaptcha.Internal.Helpers;

public static class CaptchaTypeExtensions
{
    public static Type GetSolutionType(this CaptchaType captchaType)
    {
        switch (captchaType)
        {
            case CaptchaType.ReCaptchaV2:
                return typeof(RecaptchaSolution);
            case CaptchaType.ReCaptchaV2Proxyless:
                return typeof(RecaptchaSolution);
            case CaptchaType.ReCaptchaV2Enterprise:
                return typeof(RecaptchaSolution);
            case CaptchaType.ReCaptchaV2EnterpriseProxyless:
                return typeof(RecaptchaSolution);
            case CaptchaType.ReCaptchaV3Proxyless:
                return typeof(RecaptchaSolution);
            case CaptchaType.ReCaptchaV3Enterprise:
                return typeof(RecaptchaSolution);
            case CaptchaType.HCaptcha:
                return typeof(HCaptchaSolution);
            case CaptchaType.HCaptchaProxyless:
                return typeof(HCaptchaSolution);
            case CaptchaType.FunCaptcha:
                return typeof(FunCaptchaSolution);
            case CaptchaType.FunCaptchaProxyless:
                return typeof(FunCaptchaSolution);
            case CaptchaType.ImageToText:
                return typeof(ImageToTextSolution);
            case CaptchaType.GeeTestV3:
                return typeof(GeeTestV3Solution);
            case CaptchaType.GeeTestV3Proxyless:
                return typeof(GeeTestV3Solution);
            case CaptchaType.GeeTestV4:
                return typeof(GeeTestV4Solution);
            case CaptchaType.GeeTestV4Proxyless:
                return typeof(GeeTestV4Solution);
            case CaptchaType.AntiGate:
                return typeof(AntiGateSolution);
            default:
                throw new ArgumentOutOfRangeException(nameof(captchaType), captchaType, null);
        }
    }

}