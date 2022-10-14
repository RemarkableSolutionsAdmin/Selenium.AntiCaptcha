using AntiCaptchaApi.Net.Models;
using OpenQA.Selenium;
using Selenium.AntiCaptcha.Enums;

namespace Selenium.AntiCaptcha.Internal;

public interface ICaptchaIdentifier
{
    public  bool CanIdentify(CaptchaType type);
    public CaptchaType? Identify(IWebDriver driver, ProxyConfig? proxyConfig);
    public CaptchaType? SpecifyCaptcha(CaptchaType originalType, IWebDriver driver, ProxyConfig? proxyConfig);
}