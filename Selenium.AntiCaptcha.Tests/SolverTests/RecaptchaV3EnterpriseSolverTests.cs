using AntiCaptchaApi.Net.Models.Solutions;
using OpenQA.Selenium;
using Selenium.AntiCaptcha;
using Selenium.AntiCaptcha.Enums;
using Selenium.Anticaptcha.Tests.TestCore;

namespace Selenium.Anticaptcha.Tests.SolverTests
{
    public class RecaptchaV3EnterpriseSolverTests : AnticaptchaTestBase
    {
        [Fact]
        public void Solve_WithCaptchaTypeSpecified()
        {
            Driver.Url = TestUris.Recaptcha.V3.EnterpriseW1;
            var result = Driver.SolveCaptcha<RecaptchaSolution>(ClientKey, captchaType: CaptchaType.ReCaptchaV3Enterprise);
            AssertSolveCaptchaResult(result);
        }
        
        [Fact]
        public void Solve_WithoutCaptchaTypeSpecified()
        {
            Driver.Url = TestUris.Recaptcha.V3.EnterpriseW1;
            var result = Driver.SolveCaptcha<RecaptchaSolution>(ClientKey);
            AssertSolveCaptchaResult(result);
        }

        public RecaptchaV3EnterpriseSolverTests(WebDriverFixture fixture) : base(fixture)
        {
        }
    }
}

