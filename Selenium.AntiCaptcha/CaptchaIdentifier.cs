using AntiCaptchaApi.Net.Models.Solutions;
using OpenQA.Selenium;
using Selenium.AntiCaptcha.Enums;
using Selenium.AntiCaptcha.Internal;
using Selenium.AntiCaptcha.Internal.Extensions;
using Selenium.AntiCaptcha.Models;

namespace Selenium.AntiCaptcha;

public static class CaptchaIdentifier
{
    private static readonly List<ICaptchaIdentifier> CaptchaIdentifiers = new()
    {
        new GeeTestIdentifier(),
        new RecaptchaIdentifier(),
        new HCaptchaIdentifier(),
        new ImageToTextIdentifier(),
        new AntiGateIdentifier(),
        new FunCaptchaIdentifier(),
        new TurnstileCaptchaIdentifier()
    };

    public static async Task<List<CaptchaType>> IdentifyAsync(IWebDriver driver, SolverArguments arguments, CancellationToken cancellationToken)
    {
        var identifiedTypes = new List<CaptchaType>();
        foreach (var captchaIdentifier in CaptchaIdentifiers)
        {
            var identifiedCaptcha = await captchaIdentifier.IdentifyInAllFramesAsync(driver, arguments, cancellationToken);
            
            if (identifiedCaptcha != null)
            {
                identifiedTypes.Add(identifiedCaptcha.Value);
            }
        }
        
        return identifiedTypes;
    }
    

    public static bool CanIdentifyCaptcha(CaptchaType captchaType)
    {
        return CaptchaIdentifiers.Any(x => x.CanIdentify(captchaType));
    }

    private static async Task<CaptchaType?> IdentifyCaptchaAsync(IWebDriver driver, SolverArguments arguments, CaptchaType captchaType, CancellationToken cancellationToken)
    {
        foreach (var captchaIdentifier in CaptchaIdentifiers.Where(x => x.CanIdentify(captchaType)))
        {
            var result = await captchaIdentifier.SpecifyCaptcha(captchaType, driver, arguments, cancellationToken);
            if (result.HasValue)
            {
                return result.Value;
            }
        }
        return captchaType;
    }

    public static async Task<CaptchaType?> IdentifyCaptchaAsync<TSolution>(IWebDriver driver, SolverArguments arguments, CancellationToken cancellationToken)
        where TSolution : BaseSolution, new()
    {
        var result = new TSolution().GetCaptchaType();
        return !result.HasValue ? result : await IdentifyCaptchaAsync(driver, arguments, result.Value, cancellationToken);
    }
}