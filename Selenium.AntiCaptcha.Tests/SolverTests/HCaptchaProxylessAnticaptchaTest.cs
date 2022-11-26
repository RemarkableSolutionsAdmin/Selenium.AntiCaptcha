using AntiCaptchaApi.Net.Models.Solutions;
using Selenium.AntiCaptcha;
using Selenium.AntiCaptcha.Enums;
using Selenium.AntiCaptcha.Models;
using Selenium.Anticaptcha.Tests.TestCore;

namespace Selenium.Anticaptcha.Tests.SolverTests;

public class HCaptchaProxylessAnticaptchaTest : SequentialAnticaptchaTestBase
{
    [Fact]
    public void HCaptchaWithCaptchaTypeSpecified()
    {
        SetDriverUrl(TestUris.HCaptcha.W1);
        var result = Driver.SolveCaptcha<HCaptchaSolution>(clientKey: ClientKey, new SolverAdditionalArguments {CaptchaType = CaptchaType.HCaptcha});
        AssertSolveCaptchaResult(result);
    }
    
    
    [Fact]
    public void HCaptchaWithProxyWithoutCaptchaType()
    {
        SetDriverUrl(TestUris.HCaptcha.W1);
        var result = Driver.SolveCaptcha<HCaptchaSolution>(clientKey: ClientKey);
        AssertSolveCaptchaResult(result);
    }

    [Fact]
    public void SolveNonGeneric_HCaptchaWithCaptchaTypeSpecified()
    {
        SetDriverUrl(TestUris.HCaptcha.W1);
        var result = Driver.SolveCaptcha(clientKey: ClientKey, new SolverAdditionalArguments {CaptchaType = CaptchaType.HCaptchaProxyless});
        AssertSolveCaptchaResult(result);
    }
    
    
    [Fact]
    public void SolveNonGeneric_HCaptchaWithProxyWithoutCaptchaType()
    {
        SetDriverUrl(TestUris.HCaptcha.W1);
        var result = Driver.SolveCaptcha(clientKey: ClientKey);
        AssertSolveCaptchaResult(result);
    }

    public HCaptchaProxylessAnticaptchaTest(WebDriverFixture fixture) : base(fixture)
    {
    }
}