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

    internal static async Task<List<CaptchaType>> IdentifyAsync(IWebDriver driver, SolverAdditionalArguments additionalArguments)
    {
        var identifiedTypes = new List<CaptchaType>();
        foreach (var captchaIdentifier in CaptchaIdentifiers)
        {
            var identifiedCaptcha = await captchaIdentifier.IdentifyAsync(driver, additionalArguments);

            if (identifiedCaptcha != null)
            {
                identifiedTypes.Add(identifiedCaptcha.Value);
            }
        }
        
        return identifiedTypes;
    }
    

    internal static bool CanIdentifyCaptcha(CaptchaType captchaType)
    {
        return CaptchaIdentifiers.Any(x => x.CanIdentifyAsync(captchaType));
    }

    private static async Task<CaptchaType?> IdentifyCaptcha(IWebDriver driver, SolverAdditionalArguments additionalArguments, CaptchaType captchaType)
    {
        foreach (var captchaIdentifier in CaptchaIdentifiers)
        {
            if (!captchaIdentifier.CanIdentifyAsync(captchaType))
                continue;

            var result = await captchaIdentifier.SpecifyCaptcha(captchaType, driver, additionalArguments);
            if (result.HasValue)
            {
                return result.Value;
            }
        }
        return captchaType;
    }

    internal static async Task<CaptchaType?> IdentifyCaptcha<TSolution>(IWebDriver driver, SolverAdditionalArguments additionalArguments)
        where TSolution : BaseSolution, new()
    {
        var result = new TSolution().GetCaptchaType();
        return !result.HasValue ? result : await IdentifyCaptcha(driver, additionalArguments, result.Value);
    }
}