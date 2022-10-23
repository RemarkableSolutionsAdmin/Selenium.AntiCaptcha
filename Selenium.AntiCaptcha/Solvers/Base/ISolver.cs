using AntiCaptchaApi.Net.Models;
using AntiCaptchaApi.Net.Models.Solutions;
using AntiCaptchaApi.Net.Responses;
using OpenQA.Selenium;

namespace Selenium.AntiCaptcha.Solvers.Base;


public interface ISolver<TSolution> : ISolver
    where TSolution : BaseSolution, new()
{
    public TaskResultResponse<TSolution> Solve(IWebDriver driver, string clientKey, string? url, string? siteKey,
        IWebElement? responseElement,
        IWebElement? submitElement, IWebElement? imageElement, string? userAgent, ProxyConfig? proxyConfig);
}

public interface ISolver { }
