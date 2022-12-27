using System.ComponentModel;
using AntiCaptchaApi.Net.Models.Solutions;
using Selenium.AntiCaptcha;
using Selenium.AntiCaptcha.Enums;
using Selenium.AntiCaptcha.Models;
using Selenium.Anticaptcha.Tests.TestCore;

namespace Selenium.Anticaptcha.Tests.SolverTests;

[Category(TestCategories.Proxyless)]
public class HCaptchaProxylessAnticaptchaTests : SequentialAnticaptchaTestBase
{
    [Fact]
    public async Task SolveGeneric_HCaptchaWithCaptchaTypeSpecified()
    {
        await SetDriverUrl(TestUris.HCaptcha.EntiwicklerEbay);
        var result = await Driver.SolveCaptchaAsync<HCaptchaSolution>(clientKey: ClientKey, solverArguments: new SolverArguments
        {
            CaptchaType = CaptchaType.HCaptchaProxyless,
        });
        AssertSolveCaptchaResult(result);
    }
    
    [Fact]
    public async Task SolveGeneric_HCaptchaWithProxyWithoutCaptchaType()
    {
        await SetDriverUrl(TestUris.HCaptcha.EntiwicklerEbay);
        var result = await Driver.SolveCaptchaAsync<HCaptchaSolution>(clientKey: ClientKey);
        AssertSolveCaptchaResult(result);
    }

    [Fact]
    public async Task SolveNonGeneric_HCaptchaWithCaptchaTypeSpecified()
    {
        await SetDriverUrl(TestUris.HCaptcha.EntiwicklerEbay);
        var result = await Driver.SolveCaptchaAsync(clientKey: ClientKey, solverArguments: new SolverArguments
        {
            CaptchaType = CaptchaType.HCaptchaProxyless
        });
        AssertSolveCaptchaResult(result);
    }
    
    
    [Fact]
    public async Task SolveNonGeneric_HCaptchaWithProxyWithoutCaptchaType()
    {
        await SetDriverUrl(TestUris.HCaptcha.EntiwicklerEbay);
        var result = await Driver.SolveCaptchaAsync(clientKey: ClientKey);
        AssertSolveCaptchaResult(result);
    }

    public HCaptchaProxylessAnticaptchaTests(WebDriverFixture fixture) : base(fixture)
    {
    }
}