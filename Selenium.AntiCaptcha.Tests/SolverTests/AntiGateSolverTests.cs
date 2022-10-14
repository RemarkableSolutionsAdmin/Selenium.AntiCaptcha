using AntiCaptchaApi.Net.Models.Solutions;
using OpenQA.Selenium;
using Selenium.AntiCaptcha;
using Selenium.AntiCaptcha.Enums;
using Selenium.Anticaptcha.Tests.TestCore;

namespace Selenium.Anticaptcha.Tests.SolverTests
{
    public class AntiGateSolverTests : AnticaptchaTestBase
    {
        [Fact]
        public void Solve_CaptchaTypeSpecified()
        {
            Driver.Url = TestUris.AntiGate.W1;
            var result = Driver.SolveCaptcha<AntiGateSolution>(ClientKey, 
                captchaType: CaptchaType.AntiGate,
                proxyConfig: TestEnvironment.GetCurrentTestProxyConfig());
            AssertSolveCaptchaResult(result);
        }
        
        [Fact]
        public void Solve_WithoutCaptchaTypeSpecified()
        {
            Driver.Url = TestUris.AntiGate.W1;
            var result = Driver.SolveCaptcha<AntiGateSolution>(ClientKey,
                proxyConfig: TestEnvironment.GetCurrentTestProxyConfig());
            AssertSolveCaptchaResult(result);
        }

        public AntiGateSolverTests(WebDriverFixture fixture) : base(fixture)
        {
        }
    }
}

