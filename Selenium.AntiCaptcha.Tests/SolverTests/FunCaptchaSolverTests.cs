using AntiCaptchaApi.Net.Models.Solutions;
using OpenQA.Selenium;
using Selenium.AntiCaptcha;
using Selenium.AntiCaptcha.Enums;
using Selenium.Anticaptcha.Tests.TestCore;

namespace Selenium.Anticaptcha.Tests.SolverTests
{
    public class FunCaptchaSolverTests : SequentialAnticaptchaTestBase
    {
        [Fact]
        public void Solve_CaptchaTypeSpecified()
        {
            SetDriverUrl(TestUris.FunCaptcha.W1);
            var result = Driver.SolveCaptcha<FunCaptchaSolution>(ClientKey, 
                captchaType: CaptchaType.FunCaptcha,
                proxyConfig: TestEnvironment.GetCurrentTestProxyConfig());
            AssertSolveCaptchaResult(result);
        }
        
        [Fact]
        public void Solve_WithoutCaptchaTypeSpecified()
        {
            SetDriverUrl(TestUris.FunCaptcha.W1);
            var result = Driver.SolveCaptcha<FunCaptchaSolution>(ClientKey,
                proxyConfig: TestEnvironment.GetCurrentTestProxyConfig());
            AssertSolveCaptchaResult(result);
        }

        [Fact]
        public void SolveNonGeneric_CaptchaTypeSpecified()
        {
            SetDriverUrl(TestUris.FunCaptcha.W1);
            var result = Driver.SolveCaptcha(ClientKey, 
                captchaType: CaptchaType.FunCaptcha,
                proxyConfig: TestEnvironment.GetCurrentTestProxyConfig());
            AssertSolveCaptchaResult(result);
        }
        
        [Fact]
        public void SolveNonGeneric_WithoutCaptchaTypeSpecified()
        {
            SetDriverUrl(TestUris.FunCaptcha.W1);
            var result = Driver.SolveCaptcha(ClientKey,
                proxyConfig: TestEnvironment.GetCurrentTestProxyConfig());
            AssertSolveCaptchaResult(result);
        }

        public FunCaptchaSolverTests(WebDriverFixture fixture) : base(fixture)
        {
        }
    }
}

