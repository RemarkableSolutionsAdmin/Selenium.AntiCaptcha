using AntiCaptchaApi.Net.Models.Solutions;
using Selenium.AntiCaptcha;
using Selenium.AntiCaptcha.Models;
using Selenium.Anticaptcha.Tests.TestCore;

namespace Selenium.Anticaptcha.Tests.SolverTests
{
    public class FunCaptchaSolverTests : SequentialAnticaptchaTestBase
    {
        [Fact]
        public async Task Solve_CaptchaTypeSpecified()
        {
            await SetDriverUrl(TestUris.FunCaptcha.FunCaptchaDemo);
            var result = await Driver.SolveCaptchaAsync<FunCaptchaSolution>(ClientKey, SolverProxyArguments);
            AssertSolveCaptchaResult(result);
        }
        
        [Fact]
        public async Task Solve_WithoutCaptchaTypeSpecified()
        {
            await SetDriverUrl(TestUris.FunCaptcha.FunCaptchaDemo);
            var result = await Driver.SolveCaptchaAsync<FunCaptchaSolution>(ClientKey, SolverProxyArguments);
            AssertSolveCaptchaResult(result);
        }

        [Fact]
        public async Task SolveNonGeneric_CaptchaTypeSpecified()
        {
            await SetDriverUrl(TestUris.FunCaptcha.FunCaptchaDemo);
            var result = await Driver.SolveCaptchaAsync(ClientKey, SolverProxyArguments);
            AssertSolveCaptchaResult(result);
        }
        
        [Fact]
        public async Task SolveNonGeneric_WithoutCaptchaTypeSpecified()
        {
            await SetDriverUrl(TestUris.FunCaptcha.FunCaptchaDemo);
            var result = await Driver.SolveCaptchaAsync(ClientKey, SolverProxyArguments);
            AssertSolveCaptchaResult(result);
        }

        public FunCaptchaSolverTests(WebDriverFixture fixture) : base(fixture)
        {
        }
    }
}

