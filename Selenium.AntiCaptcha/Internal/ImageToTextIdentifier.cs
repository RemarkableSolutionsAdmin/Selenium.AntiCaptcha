using AntiCaptchaApi.Net.Models;
using OpenQA.Selenium;
using Selenium.AntiCaptcha.Enums;
using Selenium.AntiCaptcha.Internal.Extensions;

namespace Selenium.AntiCaptcha.Internal;

public class ImageToTextIdentifier : ProxyCaptchaIdentifier
{
    public ImageToTextIdentifier()
    {
        IdentifableTypes.Add(CaptchaType.ImageToText);
    }

    public override CaptchaType? SpecifyCaptcha(CaptchaType originalType, IWebDriver driver, ProxyConfig? proxyConfig)
    {
        return originalType;
    }

    public override CaptchaType? Identify(IWebDriver driver, ProxyConfig? proxyConfig, IWebElement? imageElement = null)
    {
        var base64 = imageElement?.DownloadSourceAsBase64String();
        
        if (!string.IsNullOrEmpty(base64))
        {
            return CaptchaType.ImageToText;
        }

        return DoesCaptchaImageElementExists(driver) ? CaptchaType.ImageToText : null;
    }

    private static bool DoesCaptchaImageElementExists(IWebDriver driver)
    {
        var imageElements = driver.FindManyByXPathAllFrames("//img");
        return false; // TODO!
    }
    
    
}