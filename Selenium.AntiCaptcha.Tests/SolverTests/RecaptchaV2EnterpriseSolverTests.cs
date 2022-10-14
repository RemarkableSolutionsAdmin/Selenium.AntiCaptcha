using AntiCaptchaApi.Net.Models.Solutions;
using OpenQA.Selenium;
using Selenium.AntiCaptcha;
using Selenium.AntiCaptcha.Enums;
using Selenium.Anticaptcha.Tests.TestCore;

namespace Selenium.Anticaptcha.Tests.SolverTests
{
    public class RecaptchaV2EnterpriseSolverTests : AnticaptchaTestBase
    {
        [Fact]
        public void Solve_WithCaptchaTypeSpecified()
        {
            Driver.Url = TestUris.Recaptcha.V2.EnterpriseW1;
            var result = Driver.SolveCaptcha<RecaptchaSolution>(ClientKey, captchaType: CaptchaType.ReCaptchaV2Enterprise, proxyConfig: TestEnvironment.GetCurrentTestProxyConfig());
            AssertSolveCaptchaResult(result);
        }
        
        [Fact]
        public void Solve_WithoutCaptchaTypeSpecified()
        {
            Driver.Url = TestUris.Recaptcha.V2.EnterpriseW1;
            var result = Driver.SolveCaptcha<RecaptchaSolution>(ClientKey, proxyConfig: TestEnvironment.GetCurrentTestProxyConfig());
            AssertSolveCaptchaResult(result);
        }

        public RecaptchaV2EnterpriseSolverTests(WebDriverFixture fixture) : base(fixture)
        {
        }
    }
}

