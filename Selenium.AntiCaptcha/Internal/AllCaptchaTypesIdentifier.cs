using System.Text.RegularExpressions;
using AntiCaptchaApi.Net.Models;
using AntiCaptchaApi.Net.Models.Solutions;
using OpenQA.Selenium;
using Selenium.AntiCaptcha.Enums;
using Selenium.AntiCaptcha.Internal.Extensions;

namespace Selenium.AntiCaptcha.Internal;

internal static class AllCaptchaTypesIdentifier
{
    private const int IdentifyRetryThreshold = 3;
    private const int IdentifyRetryWaitTimeMs = 2000;

    private static readonly List<ICaptchaIdentifier> CaptchaIdentifiers = new()
    {
        new GeeTestIdentifier(), 
        new RecaptchaIdentifier(),
        new HCaptchaIdentifier(), 
        new ImageToTextIdentifier(), 
        new AntiGateIdentifier(),
        new FunCaptchaIdentifier()
    };

    internal static List<CaptchaType> Identify(IWebDriver driver, ProxyConfig? proxyConfig)
    {
        var identifiedTypes = new List<CaptchaType>();
        for (var i = 0; i < IdentifyRetryThreshold; i++)
        {
            foreach (var captchaIdentifier in CaptchaIdentifiers)
            {
                var identifiedCaptcha = captchaIdentifier.Identify(driver, proxyConfig);

                if (identifiedCaptcha != null)
                {
                    identifiedTypes.Add(identifiedCaptcha.Value);
                }
            }

            if (!identifiedTypes.Any())
            {                
                Thread.Sleep(IdentifyRetryWaitTimeMs);                
            }
            else
            {
                return identifiedTypes;
            }
            
        }
        
        return identifiedTypes;
    }
    
    
    
    
    internal static bool CanIdentifyCaptcha(CaptchaType captchaType)
    {
        return CaptchaIdentifiers.Any(x => x.CanIdentify(captchaType));
    }

    internal static CaptchaType? IdentifyCaptcha<TSolution>(IWebDriver driver, ProxyConfig? proxyConfig)
        where TSolution : BaseSolution, new()
    {
        var result = new TSolution().GetCaptchaType();

        if (!result.HasValue)
            return result;
        
        for (var i = 0; i < IdentifyRetryThreshold; i++)
        {
            foreach (var captchaIdentifier in CaptchaIdentifiers)
            {
                if (!captchaIdentifier.CanIdentify(result.Value)) 
                    continue;
                
                var original = result;
                result = captchaIdentifier.SpecifyCaptcha(result.Value, driver, proxyConfig);
                if (result.HasValue)
                {
                    return result;
                }

                result = original;
            }
            Thread.Sleep(IdentifyRetryWaitTimeMs);
        }
        
        return result;
    } 
}