using AntiCaptchaApi.Net.Models.Solutions;
using OpenQA.Selenium;
using Selenium.AntiCaptcha;
using Selenium.AntiCaptcha.Enums;
using Selenium.Anticaptcha.Tests.TestCore;

namespace Selenium.Anticaptcha.Tests.SolverTests
{
    public class FunCaptchaProxylessSolverTests : SequentialAnticaptchaTestBase
    {
        [Fact]
        public async Task Solve_CaptchaTypeSpecified()
        {
            SetDriverUrl(TestUris.FunCaptcha.W1);
            var result = await Driver.SolveCaptchaAsync<FunCaptchaSolution>(ClientKey);
            AssertSolveCaptchaResult(result);
        }
        
        [Fact]
        public async Task Solve_WithoutCaptchaTypeSpecified()
        {
            SetDriverUrl(TestUris.FunCaptcha.W1);
            var result = await Driver.SolveCaptchaAsync<FunCaptchaSolution>(ClientKey);
            AssertSolveCaptchaResult(result);
        }

        public FunCaptchaProxylessSolverTests(WebDriverFixture fixture) : base(fixture)
        {
        }
    }
}

