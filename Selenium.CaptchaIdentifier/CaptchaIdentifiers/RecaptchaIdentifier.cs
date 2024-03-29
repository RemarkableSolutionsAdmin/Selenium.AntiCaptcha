﻿using AntiCaptchaApi.Net.Models;
using OpenQA.Selenium;
using Selenium.CaptchaIdentifier.Constants;
using Selenium.CaptchaIdentifier.Enums;
using Selenium.FramesSearcher.Extensions;

namespace Selenium.CaptchaIdentifier.CaptchaIdentifiers;

public class RecaptchaIdentifier  : ProxyCaptchaIdentifier
{
    public RecaptchaIdentifier()
    {
        IdentifiableTypes.AddRange(CaptchaTypeGroups.ReCaptchaTypes);
    }

    public override async Task<CaptchaType?> IdentifyInCurrentFrameAsync(IWebDriver driver,
        IWebElement? imageElement, ProxyConfig? proxyConfig,
        CancellationToken cancellationToken)
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
                driver.SwitchTo().DefaultContent();
                var containsInteractableButtonWithSiteKey = HasInteractableButtonWithSiteKey(driver);
                isV2Recaptcha = containsInteractableButtonWithSiteKey;
            ;    isV3Recaptcha = !containsInteractableButtonWithSiteKey;
            }
            else
            {
                driver.SwitchTo().Frame(recaptchaFrame);
                isV2Recaptcha = true;
            }

            if (isV2Recaptcha == isV3Recaptcha)
            {
                return null;
            }

            CaptchaType result;
            if (isV2Recaptcha)
            {
                result = isEnterprise ? CaptchaType.ReCaptchaV2EnterpriseProxyless : CaptchaType.ReCaptchaV2Proxyless;
                return await base.SpecifyCaptcha(result, driver, imageElement, proxyConfig, cancellationToken);
            }

            result = isEnterprise ? CaptchaType.ReCaptchaV3Enterprise : CaptchaType.ReCaptchaV3;
            return await base.SpecifyCaptcha(result, driver, imageElement, proxyConfig, cancellationToken);
        }
        catch (Exception)
        {
            return null;
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
        IWebElement? imageElement, ProxyConfig? proxyConfig, CancellationToken cancellationToken)
    {
        return IdentifyInCurrentFrameAsync(driver, imageElement, proxyConfig, cancellationToken);
    }

    private static IWebElement? GetRecaptchaIFrame(IWebDriver driver)
    {
        return driver.FindByXPathInCurrentFrame("//iframe[contains(@src, 'recaptcha') and not(contains(@src, 'hcaptcha'))]");
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