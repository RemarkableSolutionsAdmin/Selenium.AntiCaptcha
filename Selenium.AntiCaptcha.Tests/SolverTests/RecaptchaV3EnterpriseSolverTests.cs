using AntiCaptchaApi.Net.Models.Solutions;
using OpenQA.Selenium;
using Selenium.AntiCaptcha;
using Selenium.AntiCaptcha.Enums;
using Selenium.Anticaptcha.Tests.TestCore;

namespace Selenium.Anticaptcha.Tests.SolverTests
{
    public class RecaptchaV3EnterpriseSolverTests : SequentialAnticaptchaTestBase
    {
        [Fact]
        public void Solve_WithCaptchaTypeSpecified()
        {
            SetDriverUrl(TestUris.Recaptcha.V3.NonEnterprise.W1);
            var result = Driver.SolveCaptcha<RecaptchaSolution>(ClientKey);
            AssertSolveCaptchaResult(result);
        }
        
        [Fact]
        public void Solve_WithoutCaptchaTypeSpecified()
        {
            SetDriverUrl(TestUris.Recaptcha.V3.NonEnterprise.W1);
            var result = Driver.SolveCaptcha<RecaptchaSolution>(ClientKey);
            AssertSolveCaptchaResult(result);
        }

        [Fact]
        public void SolveNonGeneric_WithCaptchaTypeSpecified()
        {
            SetDriverUrl(TestUris.Recaptcha.V3.NonEnterprise.W1);
            var result = Driver.SolveCaptcha(ClientKey);
            AssertSolveCaptchaResult(result);
        }
        
        [Fact]
        public void SolveNonGeneric_WithoutCaptchaTypeSpecified()
        {
            SetDriverUrl(TestUris.Recaptcha.V3.NonEnterprise.W1);
            var result = Driver.SolveCaptcha(ClientKey);
            AssertSolveCaptchaResult(result);
        }

        public RecaptchaV3EnterpriseSolverTests(WebDriverFixture fixture) : base(fixture)
        {
        }
    }
}

