using AntiCaptchaApi.Net.Models.Solutions;
using OpenQA.Selenium.Chrome;

namespace Selenium.AntiCaptcha.Tests;

public class HCaptchaProxylessAnticaptchaTest : AnticaptchaTestBase
{
    [Fact]
    public void HCaptchaWithCaptchaTypeSpecified()
    {
        Driver.Url = "https://entwickler.ebay.de/signin?tab=register";
        var result = Driver.SolveCaptcha<HCaptchaSolution>(clientKey: ClientKey);
        AssertSolveCaptchaResult(result);
    }
    
    
    [Fact]
    public void HCaptchaWithProxyWithoutCaptchaType()
    {
        Driver.Url = "https://entwickler.ebay.de/signin?tab=register";
        var result = Driver.SolveCaptcha<HCaptchaSolution>(clientKey: ClientKey,
            proxyConfig: GetCurrentTestProxyConfig());
        AssertSolveCaptchaResult(result);
    }

    public HCaptchaProxylessAnticaptchaTest(WebDriverFixture fixture) : base(fixture)
    {
    }
}