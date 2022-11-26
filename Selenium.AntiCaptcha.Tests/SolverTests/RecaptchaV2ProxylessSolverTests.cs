using AntiCaptchaApi.Net.Models.Solutions;
using OpenQA.Selenium;
using Selenium.AntiCaptcha;
using Selenium.AntiCaptcha.Enums;
using Selenium.Anticaptcha.Tests.TestCore;

namespace Selenium.Anticaptcha.Tests.SolverTests
{
    public class RecaptchaV2ProxylessSolverTests : SequentialAnticaptchaTestBase
    {
        [Fact]
        public void Solve_WithCaptchaTypeSpecified()
        {
            SetDriverUrl(TestUris.Recaptcha.V2.NonEnterprise.W2);
            var result = Driver.SolveCaptcha<RecaptchaSolution>(ClientKey);
            AssertSolveCaptchaResult(result);
        }
        
        [Fact]
        public void Solve_WithoutCaptchaTypeSpecified()
        {
            SetDriverUrl(TestUris.Recaptcha.V2.NonEnterprise.W2);
            var result = Driver.SolveCaptcha<RecaptchaSolution>(ClientKey);
            AssertSolveCaptchaResult(result);
        }

         [Fact]
        public void SolveNonGeneric_WithCaptchaTypeSpecified()
        {
            SetDriverUrl(TestUris.Recaptcha.V2.NonEnterprise.W2);
            var result = Driver.SolveCaptcha(ClientKey);
            AssertSolveCaptchaResult(result);
        }
        
        [Fact]
        public void SolveNonGeneric_WithoutCaptchaTypeSpecified()
        {
            SetDriverUrl(TestUris.Recaptcha.V2.NonEnterprise.W2);
            var result = Driver.SolveCaptcha(ClientKey);
            AssertSolveCaptchaResult(result);
        }

        public RecaptchaV2ProxylessSolverTests(WebDriverFixture fixture) : base(fixture)
        {
        }
    }
}

