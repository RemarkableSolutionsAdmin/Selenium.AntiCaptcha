using System.ComponentModel;
using AntiCaptchaApi.Net.Models.Solutions;
using Selenium.AntiCaptcha;
using Selenium.Anticaptcha.Tests.TestCore;

namespace Selenium.Anticaptcha.Tests.SolverTests;

[Category(TestCategories.Proxyless)]
public class GeeV3ProxylessSolverTests : SequentialAnticaptchaTestBase
{
    [Fact]
    public async Task GeeV3Test()
    {
        await SetDriverUrl(TestUris.GeeTest.V3.W2);
        var result = await Driver.SolveCaptchaAsync<GeeTestV3Solution>(ClientKey);
        AssertSolveCaptchaResult(result);
    }


    public GeeV3ProxylessSolverTests(WebDriverFixture fixture) : base(fixture)
    {
    }
}