using AntiCaptchaApi.Net.Models.Solutions;
using OpenQA.Selenium;
using Selenium.AntiCaptcha;
using Selenium.AntiCaptcha.enums;
using Selenium.Anticaptcha.Tests.TestCore;

namespace Selenium.Anticaptcha.Tests.SolverTests
{
    public class RecaptchaV2AnticaptchaTest : AnticaptchaTestBase
    {
        private const string Uri = "http://antigate.com/logintest.php";
        [Fact]
        public void ReCaptchaV2WithCaptchaTypeSpecified()
        {
            Driver.Url = "http://antigate.com/logintest.php";
            var result = Driver.SolveCaptcha<RecaptchaSolution>(ClientKey, captchaType: CaptchaType.ReCaptchaV2Proxyless, submitElement: Driver.FindElement(By.ClassName("btn")));
            AssertSolveCaptchaResult(result);
        }
        
        [Fact]
        public void ReCaptchaV2WithoutCaptchaTypeSpecified()
        {
            Driver.Url = "http://antigate.com/logintest.php";
            var result = Driver.SolveCaptcha<RecaptchaSolution>(ClientKey, submitElement: Driver.FindElement(By.ClassName("btn")));
            AssertSolveCaptchaResult(result);
        }

        public RecaptchaV2AnticaptchaTest(WebDriverFixture fixture) : base(fixture)
        {
        }
    }
}

