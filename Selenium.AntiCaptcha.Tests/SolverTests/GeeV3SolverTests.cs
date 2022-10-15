using AntiCaptchaApi.Net.Models.Solutions;
using OpenQA.Selenium;
using Selenium.AntiCaptcha;
using Selenium.AntiCaptcha.Enums;
using Selenium.Anticaptcha.Tests.TestCore;

namespace Selenium.Anticaptcha.Tests.SolverTests;

public class GeeV3SolverTests : AnticaptchaTestBase
{
    [Fact]
    public void GeeV3Test()
    {
        SetDriverUrl(TestUris.GeeTest.V3.W2);
        var result = Driver.SolveCaptcha<GeeTestV3Solution>(ClientKey, CaptchaType.GeeTestV3, proxyConfig: TestEnvironment.GetCurrentTestProxyConfig());
        AssertSolveCaptchaResult(result);
    }


    public GeeV3SolverTests(WebDriverFixture fixture) : base(fixture)
    {
    }
}