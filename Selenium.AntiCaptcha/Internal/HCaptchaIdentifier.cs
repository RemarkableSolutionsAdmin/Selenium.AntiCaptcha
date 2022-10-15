using AntiCaptchaApi.Net.Models;
using OpenQA.Selenium;
using Selenium.AntiCaptcha.Enums;
using Selenium.AntiCaptcha.Internal.Extensions;

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
        IdentifableTypes.AddRange(HCaptchaTypes);
    }

    public override CaptchaType? Identify(IWebDriver driver, ProxyConfig? proxyConfig, IWebElement? imageElement = null)
    {
        return ContainsHCaptchaIFrame(driver) ? base.SpecifyCaptcha(CaptchaType.HCaptchaProxyless, driver, proxyConfig) : null;
    }
    
    private static bool ContainsHCaptchaIFrame(IWebDriver driver)
    {
        var element = driver.FindByXPathAllFrames("//iframe[contains(@src, 'hcaptcha')]");
        return element != null;
    }
    
}