using System.Text.RegularExpressions;
using AntiCaptchaApi.Net.Models;
using AntiCaptchaApi.Net.Models.Solutions;
using OpenQA.Selenium;
using Selenium.AntiCaptcha.Enums;
using Selenium.AntiCaptcha.Internal.Extensions;
using Selenium.AntiCaptcha.Models;

namespace Selenium.AntiCaptcha.Internal;

internal static class AllCaptchaTypesIdentifier
{
    private static readonly List<ICaptchaIdentifier> CaptchaIdentifiers = new()
    {
        new GeeTestIdentifier(),
        new RecaptchaIdentifier(),
        new HCaptchaIdentifier(),
        new ImageToTextIdentifier(),
        new AntiGateIdentifier(),
        new FunCaptchaIdentifier()
    };

    internal static async Task<List<CaptchaType>> IdentifyAsync(IWebDriver driver, SolverAdditionalArguments additionalArguments, CancellationToken cancellationToken)
    {
        var identifiedTypes = new List<CaptchaType>();
        foreach (var captchaIdentifier in CaptchaIdentifiers)
        {
            var currentFrameElement = driver.GetCurrentFrame();
            
            var identifiedCaptcha = await captchaIdentifier.IdentifyInAllFramesAsync(driver, additionalArguments, cancellationToken);
            var currentFrameElement2 = driver.GetCurrentFrame();


            if (identifiedCaptcha != null)
            {
                identifiedTypes.Add(identifiedCaptcha.Value);
            }
        }
        
        return identifiedTypes;
    }
    

    internal static bool CanIdentifyCaptcha(CaptchaType captchaType)
    {
        return CaptchaIdentifiers.Any(x => x.CanIdentify(captchaType));
    }

    private static async Task<CaptchaType?> IdentifyCaptchaAsync(IWebDriver driver, SolverAdditionalArguments additionalArguments, CaptchaType captchaType, CancellationToken cancellationToken)
    {
        foreach (var captchaIdentifier in CaptchaIdentifiers.Where(x => x.CanIdentify(captchaType)))
        {
            var result = await captchaIdentifier.SpecifyCaptcha(captchaType, driver, additionalArguments, cancellationToken);
            if (result.HasValue)
            {
                return result.Value;
            }
        }
        return captchaType;
    }

    internal static async Task<CaptchaType?> IdentifyCaptchaAsync<TSolution>(
        IWebDriver driver, 
        SolverAdditionalArguments additionalArguments,
        CancellationToken cancellationToken)
        where TSolution : BaseSolution, new()
    {
        var result = new TSolution().GetCaptchaType();
        return !result.HasValue ? result : await IdentifyCaptchaAsync(driver, additionalArguments, result.Value, cancellationToken);
    }
}