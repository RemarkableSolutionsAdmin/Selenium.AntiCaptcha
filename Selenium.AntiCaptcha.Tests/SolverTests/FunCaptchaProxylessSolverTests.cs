using AntiCaptchaApi.Net.Models.Solutions;
using OpenQA.Selenium;
using Selenium.AntiCaptcha;
using Selenium.AntiCaptcha.Enums;
using Selenium.Anticaptcha.Tests.TestCore;

namespace Selenium.Anticaptcha.Tests.SolverTests
{
    public class FunCaptchaProxylessSolverTests : AnticaptchaTestBase
    {
        [Fact]
        public void Solve_CaptchaTypeSpecified()
        {
            Driver.Url = TestUris.FunCaptcha.W1;
            var result = Driver.SolveCaptcha<FunCaptchaSolution>(ClientKey, 
                captchaType: CaptchaType.FunCaptchaProxyless);
            AssertSolveCaptchaResult(result);
        }
        
        [Fact]
        public void Solve_WithoutCaptchaTypeSpecified()
        {
            Driver.Url = TestUris.FunCaptcha.W1;
            var result = Driver.SolveCaptcha<FunCaptchaSolution>(ClientKey);
            AssertSolveCaptchaResult(result);
        }

        public FunCaptchaProxylessSolverTests(WebDriverFixture fixture) : base(fixture)
        {
        }
    }
}

