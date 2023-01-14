using AntiCaptchaApi.Net.Models;
using OpenQA.Selenium;
using Selenium.CaptchaIdentifier.Constants;
using Selenium.CaptchaIdentifier.Enums;
using Selenium.FramesSearcher.Extensions;

namespace Selenium.CaptchaIdentifier.CaptchaIdentifiers;

public class HCaptchaIdentifier : ProxyCaptchaIdentifier
{
    public HCaptchaIdentifier()
    {
        IdentifiableTypes.AddRange(CaptchaTypeGroups.HCaptchaTypes);
    }

    public override async Task<CaptchaType?> IdentifyInCurrentFrameAsync(IWebDriver driver, IWebElement? imageElement, ProxyConfig? proxyConfig,
        CancellationToken cancellationToken)
    {
        return ContainsHCaptchaIFrame(driver) ? await base.SpecifyCaptcha(CaptchaType.HCaptchaProxyless, driver, imageElement, proxyConfig, cancellationToken) : null;
    }
    
    private static bool ContainsHCaptchaIFrame(IWebDriver driver)
    {
        var element = driver.FindByXPathAllFrames("//iframe[contains(@src, 'hcaptcha')]");
        return element != null;
    }
    
}