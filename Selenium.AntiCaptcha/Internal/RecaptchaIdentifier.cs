using OpenQA.Selenium;
using Selenium.AntiCaptcha.enums;
using Selenium.AntiCaptcha.Internal.Extensions;

namespace Selenium.AntiCaptcha.Internal;

internal static class RecaptchaIdentifier
{
    private static List<CaptchaType> RecaptchaTypes = new()
    {
        CaptchaType.ReCaptchaV2Proxyless,
        CaptchaType.ReCaptchaV2EnterpriseProxyless,
        CaptchaType.ReCaptchaV2Enterprise,
        CaptchaType.ReCaptchaV3Proxyless,
    }; 

    public static bool IsRecaptcha(CaptchaType type)
    {
        return RecaptchaTypes.Contains(type);
    }

    public static CaptchaType? SpecifyRecaptchaType(IWebDriver driver)
    {
        var pageSource = driver.PageSource;
        var isEnterprise = IsRecaptchaEnterprise(pageSource);
        var recaptchaFrame = GetRecaptchaIFrame(driver);

        if (recaptchaFrame == null)
        {
            return null;
        }

        driver.SwitchTo().Frame(recaptchaFrame);
        
        var isV3Recaptcha = IsV3Recaptcha(driver);
        var isV2Recaptcha = IsV2Recaptcha(driver);

        if (isV2Recaptcha == isV3Recaptcha) //Should we throw an exception?
        {
            return null;
        }

        if (isV2Recaptcha)
        {
            return isEnterprise ? CaptchaType.ReCaptchaV2Enterprise : CaptchaType.ReCaptchaV2Proxyless;
        }

        return isEnterprise ? CaptchaType.ReCaptchaV3Enterprise : CaptchaType.ReCaptchaV3Proxyless;
    }

    private static IWebElement? GetRecaptchaIFrame(IWebDriver driver)
    {
        return driver.FindByXPath("//iframe[contains(@src, 'recaptcha')]");
    }

    private static bool IsV2Recaptcha(IWebDriver driver)
    {
        var recaptchaV2ElementPaths = new List<string>()
        {
            "//div[@class='rc-anchor-content']"
        };

        foreach (var path in recaptchaV2ElementPaths)
        {
            if (driver.FindByXPath(path) != null)
            {
                return true;
            }
        }
        
        return false;
    }
    
    private static bool IsV3Recaptcha(IWebDriver driver)
    {
        var recaptchaV3ElementPaths = new List<string>()
        {
            "//div[@class='rc-anchor-invisible-text']"
        };

        foreach (var path in recaptchaV3ElementPaths)
        {
            if (driver.FindByXPath(path) != null)
            {
                return true;
            }
        }
        
        return false;
    }

    private static bool IsRecaptchaEnterprise(string pageSource)
    {
        return pageSource.DoesContainRegex( 
            @"https:\/\/recaptcha.net\/recaptcha\/enterprise",
            @"https:\/\/www.google.com\/recaptcha\/enterprise");
    }

}