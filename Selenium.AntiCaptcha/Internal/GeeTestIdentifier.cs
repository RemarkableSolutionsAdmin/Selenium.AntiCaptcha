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
        var isV3 = IsV3(driver);
        var isV4 = IsV4(driver);


        if (isV3 == isV4)
        {
            return null;
        }
        
        return base.SpecifyCaptcha(isV4 ? CaptchaType.GeeTestV4Proxyless : CaptchaType.GeeTestV3Proxyless, driver, proxyConfig);
    }

    public override CaptchaType? SpecifyCaptcha(CaptchaType originalType, IWebDriver driver, ProxyConfig? proxyConfig)
    {
        return Identify(driver, proxyConfig);
    }
    
    private static bool IsV3(IWebDriver driver)
    {
        var v3Paths = new List<string>()
        {
            "//script[contains(@src, 'https://api.geetest.com')]"
        };

        return driver.DoesAtLeastOneOfTheElementsExist(v3Paths.ToArray());
    }
    
    private static bool IsV4(IWebDriver driver)
    {
        var v4Paths = new List<string>()
        {
            "//script[contains(@src, 'https://gcaptcha4.geetest.com')]"
        };

        return driver.DoesAtLeastOneOfTheElementsExist(v4Paths.ToArray());
    }

    private static bool IsRecaptchaEnterprise(string pageSource)
    {
        return pageSource.DoesContainRegex( 
            @"https:\/\/recaptcha.net\/recaptcha\/enterprise",
            @"https:\/\/www.google.com\/recaptcha\/enterprise");
    }

}