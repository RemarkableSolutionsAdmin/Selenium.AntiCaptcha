using AntiCaptchaApi.Net.Models;
using OpenQA.Selenium;
using Selenium.AntiCaptcha.Enums;

namespace Selenium.AntiCaptcha.Internal;

public class AntiGateIdentifier : ProxyCaptchaIdentifier
{    
    public AntiGateIdentifier()
    {
        IdentifableTypes.Add(CaptchaType.AntiGate);
    }
    

    public override CaptchaType? SpecifyCaptcha(CaptchaType originalType, IWebDriver driver, ProxyConfig? proxyConfig)
    {
        return originalType;
    }
}