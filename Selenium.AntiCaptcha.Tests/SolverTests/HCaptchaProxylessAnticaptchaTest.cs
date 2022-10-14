using AntiCaptchaApi.Net.Models.Solutions;
using Selenium.AntiCaptcha;
using Selenium.Anticaptcha.Tests.TestCore;

namespace Selenium.Anticaptcha.Tests.SolverTests;

public class HCaptchaProxylessAnticaptchaTest : AnticaptchaTestBase
{
    [Fact]
    public void HCaptchaWithCaptchaTypeSpecified()
    {
        Driver.Url = TestUris.HCaptcha.W1;
        var result = Driver.SolveCaptcha<HCaptchaSolution>(clientKey: ClientKey);
        AssertSolveCaptchaResult(result);
    }
    
    
    [Fact]
    public void HCaptchaWithProxyWithoutCaptchaType()
    {
        Driver.Url = TestUris.HCaptcha.W1;
        var result = Driver.SolveCaptcha<HCaptchaSolution>(clientKey: ClientKey,
            proxyConfig: TestEnvironment.GetCurrentTestProxyConfig());
        AssertSolveCaptchaResult(result);
    }

    public HCaptchaProxylessAnticaptchaTest(WebDriverFixture fixture) : base(fixture)
    {
    }
}