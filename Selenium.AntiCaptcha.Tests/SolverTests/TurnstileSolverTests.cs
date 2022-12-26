using System.ComponentModel;
using AntiCaptchaApi.Net.Models.Solutions;
using Selenium.AntiCaptcha;
using Selenium.Anticaptcha.Tests.TestCore;

namespace Selenium.Anticaptcha.Tests.SolverTests
{
    public class TurnstileSolverTests : SequentialAnticaptchaTestBase
    {
        [Fact]
        public async Task Solve_WithCaptchaTypeSpecified()
        {
            await SetDriverUrl(TestUris.Turnstile.TurnStileDemo);
            var result = await Driver.SolveCaptchaAsync<TurnstileSolution>(ClientKey, _solverProxyAdditionalArguments);
            AssertSolveCaptchaResult(result);
        }
        
        [Fact]
        public async Task Solve_WithoutCaptchaTypeSpecified()
        {
            await SetDriverUrl(TestUris.Turnstile.TurnStileDemo);
            var result = await Driver.SolveCaptchaAsync<TurnstileSolution>(ClientKey, _solverProxyAdditionalArguments);
            AssertSolveCaptchaResult(result);
        }

         [Fact]
        public async Task SolveNonGeneric_WithCaptchaTypeSpecified()
        {
            await SetDriverUrl(TestUris.Turnstile.TurnStileDemo);
            var result = await Driver.SolveCaptchaAsync(ClientKey, _solverProxyAdditionalArguments);
            AssertSolveCaptchaResult(result);
        }
        
        [Fact]
        public async Task SolveNonGeneric_WithoutCaptchaTypeSpecified()
        {
            await SetDriverUrl(TestUris.Turnstile.TurnStileDemo);
            var result = await Driver.SolveCaptchaAsync(ClientKey, _solverProxyAdditionalArguments);
            AssertSolveCaptchaResult(result);
        }

        public TurnstileSolverTests(WebDriverFixture fixture) : base(fixture)
        {
        }
    }
}

