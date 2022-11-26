using AntiCaptchaApi.Net.Models;
using AntiCaptchaApi.Net.Models.Solutions;
using OpenQA.Selenium;
using Selenium.AntiCaptcha.Enums;
using Selenium.AntiCaptcha.Internal.Extensions;
using Selenium.AntiCaptcha.Internal.Helpers;
using Selenium.AntiCaptcha.Models;

namespace Selenium.AntiCaptcha.Internal;

public abstract class ProxyCaptchaIdentifier : ICaptchaIdentifier
{
    protected readonly List<CaptchaType> IdentifiableTypes = new();
    
    public bool CanIdentify(CaptchaType type)
    {
        return IdentifiableTypes.Contains(type);
    }

    public abstract CaptchaType? Identify(IWebDriver driver, SolverAdditionalArguments additionalArguments);
    public virtual CaptchaType? SpecifyCaptcha(CaptchaType originalType, IWebDriver driver, SolverAdditionalArguments additionalArguments)
    {
        if (additionalArguments.ProxyConfig == null || string.IsNullOrEmpty(additionalArguments.ProxyConfig.ProxyAddress))
        {
            return originalType;
        }

        return originalType.ToProxyType();
    }

}