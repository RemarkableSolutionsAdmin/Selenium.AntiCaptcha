using AntiCaptchaApi.Net.Models;
using OpenQA.Selenium;
using Selenium.CaptchaIdentifier.Enums;

namespace Selenium.CaptchaIdentifier.CaptchaIdentifiers;

public class AntiGateIdentifier : ProxyCaptchaIdentifier
{    
    public AntiGateIdentifier()
    {
        IdentifiableTypes.Add(CaptchaType.AntiGate);
    }


    public override async Task<CaptchaType?> IdentifyInCurrentFrameAsync(IWebDriver driver, IWebElement? imageElement, ProxyConfig? proxyConfig,
        CancellationToken cancellationToken)
    {
        return null; //TODO!
    }
}