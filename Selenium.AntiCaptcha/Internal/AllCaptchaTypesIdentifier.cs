using System.Text.RegularExpressions;
using AntiCaptchaApi.Net.Models;
using AntiCaptchaApi.Net.Models.Solutions;
using OpenQA.Selenium;
using Selenium.AntiCaptcha.enums;
using Selenium.AntiCaptcha.Internal.Extensions;

namespace Selenium.AntiCaptcha.Internal;

internal static class AllCaptchaTypesIdentifier
{
    private static readonly List<ICaptchaIdentifier> CaptchaIdentifiers = new()
    {
        new GeeTestIdentifier(), new RecaptchaIdentifier(), new HCaptchaIdentifier()
    };

    internal static CaptchaType? IdentifyCaptcha<TSolution>(IWebDriver driver, IWebElement? imageElement, ProxyConfig? proxyConfig)
        where TSolution : BaseSolution, new()
    {
        var result = new TSolution().GetCaptchaType();

        if (!result.HasValue)
            return result;

        foreach (var captchaIdentifier in CaptchaIdentifiers)
        {
            if (captchaIdentifier.IsIdentifiable(result.Value))
            {
                return captchaIdentifier.SpecifyCaptcha(result.Value, driver, proxyConfig);
            }
        }
        
        if (imageElement != null)
            return CaptchaType.ImageToText;
        return result;
    } 
}