using AntiCaptchaApi.Net.Models;
using AntiCaptchaApi.Net.Models.Solutions;
using OpenQA.Selenium;
using Selenium.CaptchaIdentifier.Enums;

namespace Selenium.CaptchaIdentifier;

public static class IWebDriverExtensions
{
    public static async Task<CaptchaType?> IdentifyCaptchaAsync<TSolution>(this IWebDriver driver, IWebElement? imageElement = default, ProxyConfig? proxyConfig = default, CancellationToken cancellationToken = default)
        where TSolution : BaseSolution, new()
    {
        return await CaptchaIdentifier.IdentifyCaptchaAsync<TSolution>(driver, imageElement, proxyConfig, cancellationToken);
    }

    public static async Task<List<CaptchaType>> IdentifyCaptchaAsync(this IWebDriver driver, IWebElement? imageElement, ProxyConfig? proxyConfig,
        CancellationToken cancellationToken)
    {
        return await CaptchaIdentifier.IdentifyCaptchaAsync(driver, imageElement, proxyConfig, cancellationToken);
    }
}