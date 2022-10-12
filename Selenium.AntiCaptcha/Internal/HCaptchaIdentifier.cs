using AntiCaptchaApi.Net.Models;
using OpenQA.Selenium;
using Selenium.AntiCaptcha.enums;

namespace Selenium.AntiCaptcha.Internal;

public class HCaptchaIdentifier : BaseCaptchaIdentifier
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


    public override CaptchaType? SpecifyCaptcha(CaptchaType originalType, IWebDriver driver, ProxyConfig? proxyConfig)
    {

        var x = "";
        return base.SpecifyCaptcha(originalType, driver, proxyConfig);
    }
}