using AntiCaptchaApi.Net.Models;
using OpenQA.Selenium;
using Selenium.AntiCaptcha.Enums;
using Selenium.AntiCaptcha.Internal.Extensions;
using Selenium.AntiCaptcha.Models;

namespace Selenium.AntiCaptcha.Internal;

public class HCaptchaIdentifier : ProxyCaptchaIdentifier
{
    private static List<CaptchaType> HCaptchaTypes = new()
    {
        CaptchaType.HCaptchaProxyless,
        CaptchaType.HCaptcha,
    };

    public HCaptchaIdentifier()
    {
        IdentifiableTypes.AddRange(HCaptchaTypes);
    }

    public override CaptchaType? Identify(IWebDriver driver, SolverAdditionalArguments additionalArguments)
    {
        return ContainsHCaptchaIFrame(driver) ? base.SpecifyCaptcha(CaptchaType.HCaptchaProxyless, driver, additionalArguments) : null;
    }
    
    private static bool ContainsHCaptchaIFrame(IWebDriver driver)
    {
        var element = driver.FindByXPathAllFrames("//iframe[contains(@src, 'hcaptcha')]");
        return element != null;
    }
    
}