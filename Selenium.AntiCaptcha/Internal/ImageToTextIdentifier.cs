using AntiCaptchaApi.Net.Models;
using OpenQA.Selenium;
using Selenium.AntiCaptcha.Enums;

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
}