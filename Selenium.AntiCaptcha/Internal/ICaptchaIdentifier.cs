using AntiCaptchaApi.Net.Models;
using OpenQA.Selenium;
using Selenium.AntiCaptcha.enums;

namespace Selenium.AntiCaptcha.Internal;

public interface ICaptchaIdentifier
{
    public  bool IsIdentifiable(CaptchaType type);
    public CaptchaType? SpecifyCaptcha(CaptchaType originalType, IWebDriver driver, ProxyConfig? proxyConfig);
}