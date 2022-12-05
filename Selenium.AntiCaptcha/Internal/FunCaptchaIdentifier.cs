using System.Text.RegularExpressions;
using AntiCaptchaApi.Net.Models;
using AntiCaptchaApi.Net.Models.Solutions;
using OpenQA.Selenium;
using Selenium.AntiCaptcha.Enums;
using Selenium.AntiCaptcha.Internal.Extensions;
using Selenium.AntiCaptcha.Internal.Helpers;
using Selenium.AntiCaptcha.Models;

namespace Selenium.AntiCaptcha.Internal;

internal class FunCaptchaIdentifier : ProxyCaptchaIdentifier
{

    private readonly List<CaptchaType> _funcaptchaTypes = new()
    {
        CaptchaType.FunCaptcha, CaptchaType.FunCaptchaProxyless
    };

    public override async Task<CaptchaType?> IdentifyInCurrentFrameAsync(IWebDriver driver, SolverAdditionalArguments additionalArguments,
        CancellationToken cancellationToken)
    {
        if(IsFunCaptcha(driver))
            return await base.SpecifyCaptcha(CaptchaType.FunCaptchaProxyless, driver, additionalArguments, cancellationToken);
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
    

    public FunCaptchaIdentifier()
    {
        IdentifiableTypes.AddRange(_funcaptchaTypes);
    }
}