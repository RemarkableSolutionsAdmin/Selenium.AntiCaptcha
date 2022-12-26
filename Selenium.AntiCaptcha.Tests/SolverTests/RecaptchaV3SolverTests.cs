using System.ComponentModel;
using AntiCaptchaApi.Net.Models.Solutions;
using Selenium.AntiCaptcha;
using Selenium.Anticaptcha.Tests.TestCore;

namespace Selenium.Anticaptcha.Tests.SolverTests
{
    [Category(TestCategories.Proxyless)]
    public class RecaptchaV3SolverTests : SequentialAnticaptchaTestBase
    {
        [Fact]
        public async Task Solve_WithCaptchaTypeSpecified()
        {
            await SetDriverUrl(TestUris.Recaptcha.V3.NonEnterprise.RecaptchaV3Demo);
            var result = await Driver.SolveCaptchaAsync<RecaptchaSolution>(ClientKey);
            AssertSolveCaptchaResult(result);
        }
        
        [Fact]
        public async Task Solve_WithoutCaptchaTypeSpecified()
        {
            await SetDriverUrl(TestUris.Recaptcha.V3.NonEnterprise.RecaptchaV3Demo);
            var result = await Driver.SolveCaptchaAsync<RecaptchaSolution>(ClientKey);
            AssertSolveCaptchaResult(result);
        }

        [Fact]
        public async Task SolveNonGeneric_WithCaptchaTypeSpecified()
        {
            await SetDriverUrl(TestUris.Recaptcha.V3.NonEnterprise.RecaptchaV3Demo);
            var result = await Driver.SolveCaptchaAsync(ClientKey);
            AssertSolveCaptchaResult(result);
        }
        
        [Fact]
        public async Task SolveNonGeneric_WithoutCaptchaTypeSpecified()
        {
            await SetDriverUrl(TestUris.Recaptcha.V3.NonEnterprise.RecaptchaV3Demo);
            var result = await Driver.SolveCaptchaAsync(ClientKey);
            AssertSolveCaptchaResult(result);
        }

        public RecaptchaV3SolverTests(WebDriverFixture fixture) : base(fixture)
        {
        }
    }
}
