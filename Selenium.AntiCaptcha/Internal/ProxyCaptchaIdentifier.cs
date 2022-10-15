using AntiCaptchaApi.Net.Models;
using AntiCaptchaApi.Net.Models.Solutions;
using OpenQA.Selenium;
using Selenium.AntiCaptcha.Enums;
using Selenium.AntiCaptcha.Internal.Helpers;

namespace Selenium.AntiCaptcha.Internal;

public abstract class ProxyCaptchaIdentifier : ICaptchaIdentifier
{
    protected readonly List<CaptchaType> IdentifableTypes = new();
    
    public bool CanIdentify(CaptchaType type)
    {
        return IdentifableTypes.Contains(type);
    }

    public abstract CaptchaType? Identify(IWebDriver driver, ProxyConfig? proxyConfig, IWebElement? imageElement = null);
    public virtual CaptchaType? SpecifyCaptcha(CaptchaType originalType, IWebDriver driver, ProxyConfig? proxyConfig)
    {
        if (proxyConfig == null || string.IsNullOrEmpty(proxyConfig.ProxyAddress))
        {
            return originalType;
        }

        return originalType.ToProxyType();
    }

}