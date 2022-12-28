﻿using AntiCaptchaApi.Net.Models;
using OpenQA.Selenium;
using Selenium.AntiCaptcha.Constants;
using Selenium.AntiCaptcha.Enums;
using Selenium.AntiCaptcha.Internal.Extensions;
using Selenium.AntiCaptcha.Internal.Helpers;
using Selenium.AntiCaptcha.Models;

namespace Selenium.AntiCaptcha.CaptchaIdentifiers;

public class FunCaptchaIdentifier : ProxyCaptchaIdentifier
{
    public FunCaptchaIdentifier()
    {
        IdentifiableTypes.AddRange(CaptchaTypeGroups.FunCaptchaTypes);
    }
    
    public override async Task<CaptchaType?> IdentifyInCurrentFrameAsync(IWebDriver driver, SolverArguments arguments,
        CancellationToken cancellationToken)
    {
        if(IsFunCaptcha(driver))
            return await base.SpecifyCaptcha(CaptchaType.FunCaptchaProxyless, driver, arguments, cancellationToken);
        return null;
    }

    private bool IsFunCaptcha(IWebDriver driver)
        => IsThereFunCaptchaFunCaptchaScriptInAnyIFrames(driver) || IsThereAnElementWithPkey(driver);


    private bool IsThereAnElementWithPkey(IWebDriver driver)
    {
        driver.SwitchTo().DefaultContent();
        return !string.IsNullOrEmpty(PageSourceSearcher.FindFunCaptchaSiteKey(driver));
    }

    private static bool IsThereFunCaptchaFunCaptchaScriptInAnyIFrames(IWebDriver driver)
    {
        driver.SwitchTo().DefaultContent();
        return driver.FindByXPathAllFrames("//script[contains(@src, 'funcaptcha'") != null;
    }
}