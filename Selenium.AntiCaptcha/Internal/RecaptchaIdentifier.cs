using AntiCaptchaApi.Net.Models;
using OpenQA.Selenium;
using Selenium.AntiCaptcha.enums;
using Selenium.AntiCaptcha.Internal.Extensions;

namespace Selenium.AntiCaptcha.Internal;

internal class RecaptchaIdentifier  : BaseCaptchaIdentifier
{

    private readonly List<CaptchaType> _recaptchaTypes = new()
    {
        CaptchaType.ReCaptchaV2Proxyless,
        CaptchaType.ReCaptchaV2EnterpriseProxyless,
        CaptchaType.ReCaptchaV2Enterprise,
        CaptchaType.ReCaptchaV3Proxyless,
        CaptchaType.ReCaptchaV3Enterprise,
    };
    
    public RecaptchaIdentifier()
    {
        IdentifableTypes.AddRange(_recaptchaTypes);
    }

    public override CaptchaType? SpecifyCaptcha(CaptchaType originalType, IWebDriver driver, ProxyConfig? proxyConfig)
    {
        var pageSource = driver.PageSource;
        var isEnterprise = IsRecaptchaEnterprise(pageSource);
        var recaptchaFrame = GetRecaptchaIFrame(driver);

        if (recaptchaFrame == null)
        {
            return null;
        }

        driver.SwitchTo().Frame(recaptchaFrame);
        
        var isV3Recaptcha = IsV3(driver);
        var isV2Recaptcha = IsV2(driver);

        if (isV2Recaptcha == isV3Recaptcha) //Should we throw an exception?
        {
            return null;
        }

        CaptchaType result; 
        if (isV2Recaptcha)
        { 
            result = isEnterprise ? CaptchaType.ReCaptchaV2Enterprise : CaptchaType.ReCaptchaV2Proxyless;
           return base.SpecifyCaptcha(result, driver, proxyConfig);
        }

        result = isEnterprise ? CaptchaType.ReCaptchaV3Enterprise : CaptchaType.ReCaptchaV3Proxyless;
        return base.SpecifyCaptcha(result, driver, proxyConfig);
    }

    private static IWebElement? GetRecaptchaIFrame(IWebDriver driver)
    {
        return driver.FindByXPath("//iframe[contains(@src, 'recaptcha')]");
    }

    private static bool IsV2(IWebDriver driver)
    {
        var recaptchaV2ElementPaths = new List<string>()
        {
            "//div[@class='rc-anchor-content']"
        };

        return driver.DoesAtLeastOneOfTheElementsExist(recaptchaV2ElementPaths);
    }
    
    private static bool IsV3(IWebDriver driver)
    {
        var recaptchaV3ElementPaths = new List<string>()
        {
            "//div[@class='rc-anchor-invisible-text']"
        };
        return driver.DoesAtLeastOneOfTheElementsExist(recaptchaV3ElementPaths);
    }

    private static bool IsRecaptchaEnterprise(string pageSource)
    {
        return pageSource.DoesContainRegex( 
            @"https:\/\/recaptcha.net\/recaptcha\/enterprise",
            @"https:\/\/www.google.com\/recaptcha\/enterprise");
    }

}