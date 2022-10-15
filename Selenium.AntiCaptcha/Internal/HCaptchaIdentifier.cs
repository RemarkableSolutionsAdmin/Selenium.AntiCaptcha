using AntiCaptchaApi.Net.Models;
using OpenQA.Selenium;
using Selenium.AntiCaptcha.Enums;

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
        return null; //todo!
    }
}