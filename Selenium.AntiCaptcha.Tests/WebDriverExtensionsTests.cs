using AntiCaptchaApi.Net.Models.Solutions;
using OpenQA.Selenium.Chrome;
using Selenium.AntiCaptcha.enums;

namespace Selenium.AntiCaptcha.Tests;

public class WebDriverExtensionsTests : AnticaptchaTestBase
{
    public WebDriverExtensionsTests(WebDriverFixture fixture) : base(fixture) {}
    [Fact]
    public void ShouldThrowException_WhenSolutionTypeAndCaptchaTypeDoNotMatch()
    {
        Assert.Throws<ArgumentException>(() => Driver.SolveCaptcha<GeeTestV3Solution>(ClientKey, captchaType: CaptchaType.AntiGate));
    }
}