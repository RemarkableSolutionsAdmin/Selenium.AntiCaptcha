using AntiCaptchaApi.Net.Models;
using OpenQA.Selenium;
using Selenium.CaptchaIdentifier.Constants;
using Selenium.CaptchaIdentifier.Enums;
using Selenium.FramesSearcher.Extensions;

namespace Selenium.CaptchaIdentifier.CaptchaIdentifiers;

public class GeeTestIdentifier : ProxyCaptchaIdentifier
{
    public GeeTestIdentifier()
    {
        IdentifiableTypes.AddRange(CaptchaTypeGroups.GeeTestTypes);
    }

    public override async Task<CaptchaType?> IdentifyInCurrentFrameAsync(IWebDriver driver, IWebElement? imageElement, ProxyConfig? proxyConfig,
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

            return await base.SpecifyCaptcha(hasV4OnlyAttribute.GetValueOrDefault() ? CaptchaType.GeeTestV4Proxyless : CaptchaType.GeeTestV3Proxyless, driver, imageElement, proxyConfig, cancellationToken);
        }
        catch
        {
            return null;
        }
    }

    public override Task<CaptchaType?> SpecifyCaptcha(CaptchaType originalType, IWebDriver driver,
        IWebElement? imageElement, ProxyConfig? proxyConfig, CancellationToken cancellationToken)
    {
        return IdentifyInCurrentFrameAsync(driver, imageElement, proxyConfig, cancellationToken);
    }

    private static IWebElement? GetGeeScriptElement(IWebDriver driver)
    {
        return driver.FindByXPathAllFrames("//script[contains(@src, 'geetest.com') and contains(@src, 'challenge')]");
    }

}