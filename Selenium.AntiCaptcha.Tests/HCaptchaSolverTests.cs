using AntiCaptchaApi.Net.Models;
using AntiCaptchaApi.Net.Models.Solutions;
using AntiCaptchaApi.Net.Responses;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using Selenium.AntiCaptcha.enums;

namespace Selenium.AntiCaptcha.Tests;

public class HCaptchaSolverTests : SolverTestsBase
{
    [Fact]
    public void HCaptchaProxyLess()
    {
        using (var driver = new ChromeDriver())
        {
            driver.Url = "https://entwickler.ebay.de/signin?tab=register";
            var result = driver.SolveCaptcha<HCaptchaSolution>(clientKey: ClientKey,
                captchaType: CaptchaType.HCaptchaProxyless,
                proxyConfig: GetCurrentTestProxyConfig());
            Assert.False(result.IsErrorResponse);
            Assert.NotNull(result.Solution);
            Assert.True(result.Solution.IsValid());
        }
    }
}