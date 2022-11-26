using AntiCaptchaApi.Net.Models.Solutions;
using Selenium.AntiCaptcha;
using Selenium.AntiCaptcha.Enums;
using Selenium.AntiCaptcha.Models;
using Selenium.Anticaptcha.Tests.TestCore;

namespace Selenium.Anticaptcha.Tests.FunctionalityTests;

public class WebDriverExtensionsTests : AnticaptchaTestBase
{
    public WebDriverExtensionsTests(WebDriverFixture fixture) : base(fixture) {}
    [Fact]
    public async Task ShouldThrowException_WhenSolutionTypeAndCaptchaTypeDoNotMatch()
    {
        await Assert.ThrowsAsync<ArgumentException>(() => Driver.SolveCaptchaAsync<GeeTestV3Solution>(ClientKey, new SolverAdditionalArguments(CaptchaType: CaptchaType.ReCaptchaV2)));
    }
}