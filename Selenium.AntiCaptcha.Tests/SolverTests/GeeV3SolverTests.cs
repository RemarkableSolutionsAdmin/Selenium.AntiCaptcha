using AntiCaptchaApi.Net.Models.Solutions;
using OpenQA.Selenium;
using Selenium.AntiCaptcha;
using Selenium.AntiCaptcha.Enums;
using Selenium.Anticaptcha.Tests.TestCore;

namespace Selenium.Anticaptcha.Tests.SolverTests;

public class GeeV3SolverTests : SequentialAnticaptchaTestBase
{
    [Fact]
    public async Task GeeV3Test()
    {
        await SetDriverUrl(TestUris.GeeTest.V3.W2);
        var result = await Driver.SolveCaptchaAsync<GeeTestV3Solution>(ClientKey);
        AssertSolveCaptchaResult(result);
    }


    public GeeV3SolverTests(WebDriverFixture fixture) : base(fixture)
    {
    }
}