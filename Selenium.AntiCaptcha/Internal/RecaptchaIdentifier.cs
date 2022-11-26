﻿using AntiCaptchaApi.Net.Models;
using AntiCaptchaApi.Net.Models.Solutions;
using OpenQA.Selenium;
using Selenium.AntiCaptcha.Enums;
using Selenium.AntiCaptcha.Internal.Extensions;
using Selenium.AntiCaptcha.Models;

namespace Selenium.AntiCaptcha.Internal;

internal class RecaptchaIdentifier  : ProxyCaptchaIdentifier
{

    private readonly List<CaptchaType> _recaptchaTypes = new()
    {
        CaptchaType.ReCaptchaV2,
        CaptchaType.ReCaptchaV2Proxyless,
        CaptchaType.ReCaptchaV2EnterpriseProxyless,
        CaptchaType.ReCaptchaV2Enterprise,
        CaptchaType.ReCaptchaV3Proxyless,
        CaptchaType.ReCaptchaV3Enterprise,
    };
    
    public RecaptchaIdentifier()
    {
        IdentifiableTypes.AddRange(_recaptchaTypes);
    }

    public override async Task<CaptchaType?> IdentifyAsync(IWebDriver driver, SolverAdditionalArguments additionalArguments)
    {       
        try
        {
            var pageSource = driver.GetAllPageSource();
            var isEnterprise = IsRecaptchaEnterprise(pageSource);
            var recaptchaFrame = GetRecaptchaIFrame(driver);

            if (recaptchaFrame == null)
            {
                return null;
            }

            driver.SwitchTo().Frame(recaptchaFrame);
            var isInvisibleRecaptcha = IsInvisibleRecaptcha(driver);
            var isV3Recaptcha = false;
            var isV2Recaptcha = false;

            if (isInvisibleRecaptcha) //Might be invisible V2 or V3.
            {
                var containsInteractableButtonWithSiteKey = HasInteractableButtonWithSiteKey(driver);
                isV2Recaptcha = containsInteractableButtonWithSiteKey;
            ;    isV3Recaptcha = !containsInteractableButtonWithSiteKey;
            }
            else
            {
                driver.SwitchTo().Frame(recaptchaFrame);
                isV2Recaptcha = IsV2(driver);
            }

            if (isV2Recaptcha == isV3Recaptcha)
            {
                return null;
            }

            CaptchaType result;
            if (isV2Recaptcha)
            {
                result = isEnterprise ? CaptchaType.ReCaptchaV2EnterpriseProxyless : CaptchaType.ReCaptchaV2Proxyless;
                return await base.SpecifyCaptcha(result, driver, additionalArguments);
            }

            result = isEnterprise ? CaptchaType.ReCaptchaV3Enterprise : CaptchaType.ReCaptchaV3Proxyless;
            return await base.SpecifyCaptcha(result, driver, additionalArguments);
        }
        catch (Exception)
        {
            return null;
        }
        finally
        {
            driver.SwitchTo().DefaultContent();
        }
    }

    private static bool HasInteractableButtonWithSiteKey(IWebDriver driver)
    {
        var button = driver.FindByXPathAllFrames(
            "//button[string-length(@data-sitekey) > 0]",
            "//button[string-length(@sitekey) > 0]");

        return button != null;
    }

    private bool IsInvisibleRecaptcha(IWebDriver driver)
    {
        var recaptchaV3ElementPaths = new List<string>()
        {
            "//div[@class='rc-anchor-invisible-text']"
        };
        return driver.FindByXPathAllFrames(recaptchaV3ElementPaths.ToArray()) != null;
    }
    

    public override Task<CaptchaType?> SpecifyCaptcha(CaptchaType originalType, IWebDriver driver,
        SolverAdditionalArguments additionalArguments)
    {
        return IdentifyAsync(driver, additionalArguments);
    }

    private static IWebElement? GetRecaptchaIFrame(IWebDriver driver)
    {
        return driver.FindByXPath("//iframe[contains(@src, 'recaptcha')]");
    }

    private static bool IsV2(IWebDriver driver)
    {
        var recaptchaV2ElementPaths = new List<string>()
        {
            "//div[@class='rc-anchor-content']",
            
            //"//div[contains(@class, 'rc-anchor-invisible-text')]"
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