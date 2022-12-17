using AntiCaptchaApi.Net.Models.Solutions;
using Selenium.AntiCaptcha;
using Selenium.Anticaptcha.Tests.TestCore;

namespace Selenium.Anticaptcha.Tests.SolverTests
{
    public class RecaptchaV2SolverTests : SequentialAnticaptchaTestBase
    {
        [Fact]
        public async Task Solve_CaptchaTypeSpecified()
        {
            await SetDriverUrl(TestUris.Recaptcha.V2.NonEnterprise.W2);
            var result = await Driver.SolveCaptchaAsync<RecaptchaSolution>(ClientKey);
            AssertSolveCaptchaResult(result);
        }
        
        [Fact]
        public async Task Solve_WithoutCaptchaTypeSpecified()
        {
            await SetDriverUrl(TestUris.Recaptcha.V2.NonEnterprise.W2);
            var result = await Driver.SolveCaptchaAsync<RecaptchaSolution>(ClientKey);
            AssertSolveCaptchaResult(result);
        }

        [Fact]
        public async Task SolveNonGeneric_CaptchaTypeSpecified()
        {
            await SetDriverUrl(TestUris.Recaptcha.V2.NonEnterprise.W2);
            var result = await Driver.SolveCaptchaAsync(ClientKey);
            AssertSolveCaptchaResult(result);
        }
        
        [Fact]
        public async Task SolveNonGeneric_WithoutCaptchaTypeSpecified()
        {
            await SetDriverUrl(TestUris.Recaptcha.V2.NonEnterprise.W2);
            var result = await Driver.SolveCaptchaAsync(ClientKey);
            AssertSolveCaptchaResult(result);
        }

        public RecaptchaV2SolverTests(WebDriverFixture fixture) : base(fixture)
        {
        }
    }
}

