using OpenQA.Selenium;
using Selenium.AntiCaptcha.Enums;
using Selenium.AntiCaptcha.Internal.Extensions;
using Selenium.AntiCaptcha.Models;

namespace Selenium.AntiCaptcha.Internal;

public class GeeTestIdentifier : ProxyCaptchaIdentifier
{
    public GeeTestIdentifier()
    {
        IdentifiableTypes.AddRange(GeeTypes);
    }
    
    private static List<CaptchaType> GeeTypes = new()
    {
        CaptchaType.GeeTestV3,
        CaptchaType.GeeTestV3Proxyless,
        CaptchaType.GeeTestV4,
        CaptchaType.GeeTestV4Proxyless,
    };

    public override async Task<CaptchaType?> IdentifyInCurrentFrameAsync(IWebDriver driver, SolverArguments arguments,
        CancellationToken cancellationToken)
    {
        try
        {
            var geeScriptElement = GetGeeScriptElement(driver);
            var scriptSrcText = geeScriptElement?.GetAttribute("src");

            var areChallengeAndGtInScriptSource = scriptSrcText
                ?.DoesContainRegex("challenge=", "gt=");

            if (!areChallengeAndGtInScriptSource.GetValueOrDefault())
                return null;

            var hasV4OnlyAttribute = scriptSrcText?.DoesContainRegex("captcha_id=\\w{32}");

            return await base.SpecifyCaptcha(hasV4OnlyAttribute.GetValueOrDefault() ? CaptchaType.GeeTestV4Proxyless : CaptchaType.GeeTestV3Proxyless, driver, arguments, cancellationToken);
        }
        catch
        {
            return null;
        }
    }

    public override Task<CaptchaType?> SpecifyCaptcha(CaptchaType originalType, IWebDriver driver,
        SolverArguments arguments, CancellationToken cancellationToken)
    {
        return IdentifyInCurrentFrameAsync(driver, arguments, cancellationToken);
    }

    private static IWebElement? GetGeeScriptElement(IWebDriver driver)
    {
        return driver.FindByXPathAllFrames("//script[contains(@src, 'geetest.com') and contains(@src, 'challenge')]");
    }

}