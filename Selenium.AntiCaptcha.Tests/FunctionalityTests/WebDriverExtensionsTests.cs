using AntiCaptchaApi.Net.Models.Solutions;
using Selenium.AntiCaptcha;
using Selenium.AntiCaptcha.enums;
using Selenium.Anticaptcha.Tests.TestCore;

namespace Selenium.Anticaptcha.Tests.FunctionalityTests;

public class WebDriverExtensionsTests : AnticaptchaTestBase
{
    public WebDriverExtensionsTests(WebDriverFixture fixture) : base(fixture) {}
    [Fact]
    public void ShouldThrowException_WhenSolutionTypeAndCaptchaTypeDoNotMatch()
    {
        Assert.Throws<ArgumentException>(() => Driver.SolveCaptcha<GeeTestV3Solution>(ClientKey, captchaType: CaptchaType.AntiGate));
    }
}