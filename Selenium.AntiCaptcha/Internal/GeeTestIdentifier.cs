using System.Text.RegularExpressions;
using AntiCaptchaApi.Net.Models;
using OpenQA.Selenium;
using Selenium.AntiCaptcha.Enums;
using Selenium.AntiCaptcha.Internal.Extensions;

namespace Selenium.AntiCaptcha.Internal;

public class GeeTestIdentifier : ProxyCaptchaIdentifier
{
    public GeeTestIdentifier()
    {
        IdentifableTypes.AddRange(GeeTypes);
    }
    
    private static List<CaptchaType> GeeTypes = new()
    {
        CaptchaType.GeeTestV3,
        CaptchaType.GeeTestV3Proxyless,
        CaptchaType.GeeTestV4,
        CaptchaType.GeeTestV4Proxyless,
    };

    public override CaptchaType? Identify(IWebDriver driver, ProxyConfig? proxyConfig, IWebElement? imageElement = null)
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

            return base.SpecifyCaptcha(hasV4OnlyAttribute.GetValueOrDefault() ? CaptchaType.GeeTestV4Proxyless : CaptchaType.GeeTestV3Proxyless, driver, proxyConfig);
        }
        catch
        {
            return null;
        }
    }

    public override CaptchaType? SpecifyCaptcha(CaptchaType originalType, IWebDriver driver, ProxyConfig? proxyConfig)
    {
        return Identify(driver, proxyConfig);
    }

    private static IWebElement? GetGeeScriptElement(IWebDriver driver)
    {
        return driver.FindByXPathAllFrames("//script[contains(@src, 'geetest.com') and contains(@src, 'challenge')]");
    }

}