using System.ComponentModel;
using AntiCaptchaApi.Net.Models.Solutions;
using Selenium.AntiCaptcha;
using Selenium.Anticaptcha.Tests.TestCore;

namespace Selenium.Anticaptcha.Tests.SolverTests
{
    [Category(TestCategories.Proxyless)]
    public class FunCaptchaProxylessSolverTests : SequentialAnticaptchaTestBase
    {
        [Fact]
        public async Task Solve_CaptchaTypeSpecified()
        {
            await SetDriverUrl(TestUris.FunCaptcha.W1);
            var result = await Driver.SolveCaptchaAsync<FunCaptchaSolution>(ClientKey);
            AssertSolveCaptchaResult(result);
        }
        
        [Fact]
        public async Task Solve_WithoutCaptchaTypeSpecified()
        {
            await SetDriverUrl(TestUris.FunCaptcha.W1);
            var result = await Driver.SolveCaptchaAsync<FunCaptchaSolution>(ClientKey);
            AssertSolveCaptchaResult(result);
        }

        public FunCaptchaProxylessSolverTests(WebDriverFixture fixture) : base(fixture)
        {
        }
    }
}

