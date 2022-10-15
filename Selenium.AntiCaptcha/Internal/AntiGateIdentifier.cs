using AntiCaptchaApi.Net.Models;
using AntiCaptchaApi.Net.Models.Solutions;
using OpenQA.Selenium;
using Selenium.AntiCaptcha.Enums;

namespace Selenium.AntiCaptcha.Internal;

public class AntiGateIdentifier : ProxyCaptchaIdentifier
{    
    public AntiGateIdentifier()
    {
        IdentifableTypes.Add(CaptchaType.AntiGate);
    }


    public override CaptchaType? Identify(IWebDriver driver, ProxyConfig? proxyConfig, IWebElement? imageElement = null)
    {
        return null; //todo!
    }

    public override CaptchaType? SpecifyCaptcha(CaptchaType originalType, IWebDriver driver, ProxyConfig? proxyConfig)
    {
        return originalType;
    }
}