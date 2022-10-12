using AntiCaptchaApi.Net.Models.Solutions;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using Selenium.AntiCaptcha.enums;

namespace Selenium.AntiCaptcha.Tests;

public class WebDriverExtensionsTests : SolverTestsBase
{
    [Fact]
    public void ShouldThrowException_WhenSolutionTypeAndCaptchaTypeDoNotMatch()
    {
            
        using (var driver = new ChromeDriver(Environment.CurrentDirectory))
        {
            Assert.Throws<ArgumentException>(() => driver.SolveCaptcha<GeeTestV3Solution>(ClientKey, captchaType: CaptchaType.AntiGate));
        }
    }
}