using AntiCaptchaApi.Net.Models.Solutions;
using OpenQA.Selenium;
using Selenium.AntiCaptcha;
using Selenium.AntiCaptcha.Enums;
using Selenium.Anticaptcha.Tests.TestCore;

namespace Selenium.Anticaptcha.Tests.SolverTests
{
    public class AntiGateSolverTests : SequentialAnticaptchaTestBase
    {
        [Fact]
        public void Solve_CaptchaTypeSpecified()
        {
            SetDriverUrl(TestUris.AntiGate.W1);
            var result = Driver.SolveCaptcha<AntiGateSolution>(ClientKey, 
                captchaType: CaptchaType.AntiGate,
                proxyConfig: TestEnvironment.GetCurrentTestProxyConfig());
            AssertSolveCaptchaResult(result);
        }
        
        [Fact]
        public void Solve_WithoutCaptchaTypeSpecified()
        {
            SetDriverUrl(TestUris.AntiGate.W1);
            var result = Driver.SolveCaptcha<AntiGateSolution>(ClientKey,
                proxyConfig: TestEnvironment.GetCurrentTestProxyConfig());
            AssertSolveCaptchaResult(result);
        }

        [Fact]
        public void SolveNonGeneric_CaptchaTypeSpecified()
        {
            SetDriverUrl(TestUris.AntiGate.W1);
            var result = Driver.SolveCaptcha(ClientKey, 
                captchaType: CaptchaType.AntiGate,
                proxyConfig: TestEnvironment.GetCurrentTestProxyConfig());
            AssertSolveCaptchaResult(result);
        }
        
        [Fact]
        public void SolveNonGeneric_WithoutCaptchaTypeSpecified()
        {
            SetDriverUrl(TestUris.AntiGate.W1);
            var result = Driver.SolveCaptcha(ClientKey,
                proxyConfig: TestEnvironment.GetCurrentTestProxyConfig());
            AssertSolveCaptchaResult(result);
        }

        public AntiGateSolverTests(WebDriverFixture fixture) : base(fixture)
        {
        }
    }
}

