using AntiCaptchaApi.Net.Models.Solutions;
using AntiCaptchaApi.Net.Requests;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using Selenium.AntiCaptcha.enums;
using Xunit;

namespace Selenium.AntiCaptcha.Tests
{
    public class RecaptchaV2SolverTests : SolverTestsBase
    {
        [Fact]
        public void ReCaptchaV2()
        {
            using (var driver = new ChromeDriver())
            {
                driver.Url = "http://antigate.com/logintest.php";
                var result = driver.SolveCaptcha<RecaptchaSolution>(ClientKey, captchaType: CaptchaType.ReCaptchaV2, submitElement: driver.FindElement(By.ClassName("btn")));
                Assert.False(result.IsErrorResponse);
                Assert.NotNull(result.Solution);
                Assert.True(result.Solution.IsValid());
            }
        }
    }
}

