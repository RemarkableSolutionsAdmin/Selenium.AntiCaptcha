using AntiCaptchaApi.Net.Models.Solutions;
using Selenium.AntiCaptcha;
using Selenium.Anticaptcha.Tests.TestCore;

namespace Selenium.Anticaptcha.Tests.SolverTests;

public class HCaptchaProxylessAnticaptchaTest : SequentialAnticaptchaTestBase
{
    [Fact]
    public void HCaptchaWithCaptchaTypeSpecified()
    {
        SetDriverUrl(TestUris.HCaptcha.W1);
        var result = Driver.SolveCaptcha<HCaptchaSolution>(clientKey: ClientKey);
        AssertSolveCaptchaResult(result);
    }
    
    
    [Fact]
    public void HCaptchaWithProxyWithoutCaptchaType()
    {
        SetDriverUrl(TestUris.HCaptcha.W1);
        var result = Driver.SolveCaptcha<HCaptchaSolution>(clientKey: ClientKey,
            proxyConfig: TestEnvironment.GetCurrentTestProxyConfig());
        AssertSolveCaptchaResult(result);
    }

    [Fact]
    public void SolveNonGeneric_HCaptchaWithCaptchaTypeSpecified()
    {
        SetDriverUrl(TestUris.HCaptcha.W1);
        var result = Driver.SolveCaptcha(clientKey: ClientKey);
        AssertSolveCaptchaResult(result);
    }
    
    
    [Fact]
    public void SolveNonGeneric_HCaptchaWithProxyWithoutCaptchaType()
    {
        SetDriverUrl(TestUris.HCaptcha.W1);
        var result = Driver.SolveCaptcha(clientKey: ClientKey,
            proxyConfig: TestEnvironment.GetCurrentTestProxyConfig());
        AssertSolveCaptchaResult(result);
    }

    public HCaptchaProxylessAnticaptchaTest(WebDriverFixture fixture) : base(fixture)
    {
    }
}