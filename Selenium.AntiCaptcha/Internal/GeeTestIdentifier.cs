using AntiCaptchaApi.Net.Models;
using OpenQA.Selenium;
using Selenium.AntiCaptcha.enums;
using Selenium.AntiCaptcha.Internal.Extensions;

namespace Selenium.AntiCaptcha.Internal;

public class GeeTestIdentifier : BaseCaptchaIdentifier
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
    
    public override CaptchaType? SpecifyCaptcha(CaptchaType originalType, IWebDriver driver, ProxyConfig? proxyConfig)
    {
        if (!IsV4(driver)) //TODO! Need to find proper GeeTestV3 to check if this works.
        {
            return IsV3(driver) ? base.SpecifyCaptcha(CaptchaType.GeeTestV3Proxyless, driver, proxyConfig) : null;
        }
        
        return base.SpecifyCaptcha( CaptchaType.GeeTestV4Proxyless, driver, proxyConfig);
    }
    
    private static bool IsV3(IWebDriver driver)
    {
        var v3Paths = new List<string>()
        {
            "//script[contains(@scr, 'https://static.geetest.com/v4/gt4.js')]"
        };

        return driver.DoesAtLeastOneOfTheElementsExist(v3Paths.ToArray());
    }
    
    private static bool IsV4(IWebDriver driver)
    {
        var v4Paths = new List<string>()
        {
            "//script[@src='https://static.geetest.com/v4/gt4.js']"
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