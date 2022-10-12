using AntiCaptchaApi.Net.Models.Solutions;
using Selenium.AntiCaptcha.enums;

namespace Selenium.AntiCaptcha.Internal.Extensions;

internal static class SolutionExtensions
{
    public static CaptchaType? GetCaptchaType<TSolution>(this TSolution solution)
        where TSolution : BaseSolution
    {            
        return solution.GetType().Name switch
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