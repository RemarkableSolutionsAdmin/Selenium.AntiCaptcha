using AntiCaptchaApi.Net.Models.Solutions;
using Selenium.AntiCaptcha;
using Selenium.Anticaptcha.Tests.TestCore;

namespace Selenium.Anticaptcha.Tests.SolverTests
{
    public class RecaptchaV2EnterpriseSolverTests : SequentialAnticaptchaTestBase
    {
        [Fact]
        public async Task Solve_WithCaptchaTypeSpecified()
        {
            await SetDriverUrl(TestUris.Recaptcha.V2.NonEnterprise.W1);
            var result = await Driver.SolveCaptchaAsync<RecaptchaSolution>(ClientKey);
            AssertSolveCaptchaResult(result);
        }
        
        [Fact]
        public async Task Solve_WithoutCaptchaTypeSpecified()
        {
            await SetDriverUrl(TestUris.Recaptcha.V2.NonEnterprise.W1);
            var result = await Driver.SolveCaptchaAsync<RecaptchaSolution>(ClientKey);
            AssertSolveCaptchaResult(result);
        }
        [Fact]
        public async Task SolveNonGeneric_WithCaptchaTypeSpecified()
        {
            await SetDriverUrl(TestUris.Recaptcha.V2.NonEnterprise.W1);
            var result = await Driver.SolveCaptchaAsync(ClientKey);
            AssertSolveCaptchaResult(result);
        }
        
        [Fact]
        public async Task SolveNonGeneric_WithoutCaptchaTypeSpecified()
        {
            await SetDriverUrl(TestUris.Recaptcha.V2.NonEnterprise.W1);
            var result = await Driver.SolveCaptchaAsync(ClientKey);
            AssertSolveCaptchaResult(result);
        }

        public RecaptchaV2EnterpriseSolverTests(WebDriverFixture fixture) : base(fixture)
        {
        }
    }
}

