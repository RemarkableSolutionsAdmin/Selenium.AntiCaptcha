using AntiCaptchaApi.Net.Models;
using OpenQA.Selenium;
using Selenium.AntiCaptcha.Constants;
using Selenium.AntiCaptcha.Enums;
using Selenium.AntiCaptcha.Internal.Extensions;
using Selenium.AntiCaptcha.Models;

namespace Selenium.AntiCaptcha.CaptchaIdentifiers;

public class HCaptchaIdentifier : ProxyCaptchaIdentifier
{
    public HCaptchaIdentifier()
    {
        IdentifiableTypes.AddRange(CaptchaTypeGroups.HCaptchaTypes);
    }

    public override async Task<CaptchaType?> IdentifyInCurrentFrameAsync(IWebDriver driver, SolverArguments arguments,
        CancellationToken cancellationToken)
    {
        return ContainsHCaptchaIFrame(driver) ? await base.SpecifyCaptcha(CaptchaType.HCaptchaProxyless, driver, arguments, cancellationToken) : null;
    }
    
    private static bool ContainsHCaptchaIFrame(IWebDriver driver)
    {
        var element = driver.FindByXPathAllFrames("//iframe[contains(@src, 'hcaptcha')]");
        return element != null;
    }
    
}