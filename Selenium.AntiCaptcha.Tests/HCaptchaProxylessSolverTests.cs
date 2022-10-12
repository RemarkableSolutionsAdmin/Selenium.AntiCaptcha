using AntiCaptchaApi.Net.Models;
using AntiCaptchaApi.Net.Models.Solutions;
using AntiCaptchaApi.Net.Responses;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using Selenium.AntiCaptcha.enums;

namespace Selenium.AntiCaptcha.Tests;

public class HCaptchaProxylessSolverTests : SolverTestsBase
{
    [Fact]
    public void HCaptchaWithCaptchaTypeSpecified()
    {
        using (var driver = new ChromeDriver())
        {
            driver.Url = "https://entwickler.ebay.de/signin?tab=register";
            var result = driver.SolveCaptcha<HCaptchaSolution>(clientKey: ClientKey);
            AssertSolveCaptchaResult(result);
        }
    }
    
    
    [Fact]
    public void HCaptchaWithProxyWithoutCaptchaType()
    {
        using (var driver = new ChromeDriver())
        {
            driver.Url = "https://entwickler.ebay.de/signin?tab=register";
            var result = driver.SolveCaptcha<HCaptchaSolution>(clientKey: ClientKey,
                proxyConfig: GetCurrentTestProxyConfig());
            AssertSolveCaptchaResult(result);
        }
    }
}