using System.ComponentModel;
using AntiCaptchaApi.Net.Models.Solutions;
using Selenium.AntiCaptcha;
using Selenium.Anticaptcha.Tests.TestCore;

namespace Selenium.Anticaptcha.Tests.SolverTests
{
    [Category(TestCategories.Proxyless)]
    public class RecaptchaV2EnterpriseProxylessSolverTests : SequentialAnticaptchaTestBase
    {
        private const string WorkingUri = TestUris.Recaptcha.V2.Enterprise.Steam; 
        
        [Fact]
        public async Task Solve_WithCaptchaTypeSpecified()
        {
            await SetDriverUrl(WorkingUri);
            var result = await Driver.SolveCaptchaAsync<RecaptchaSolution>(ClientKey);
            AssertSolveCaptchaResult(result);
        }
        
        [Fact]
        public async Task Solve_WithoutCaptchaTypeSpecified()
        {
            await SetDriverUrl(WorkingUri);
            var result = await Driver.SolveCaptchaAsync<RecaptchaSolution>(ClientKey);
            AssertSolveCaptchaResult(result);
        }

        [Fact]
        public async Task SolveNonGeneric_WithCaptchaTypeSpecified()
        {
            await SetDriverUrl(WorkingUri);
            var result = await Driver.SolveCaptchaAsync(ClientKey);
            AssertSolveCaptchaResult(result);
        }

        [Fact]
        public async Task SolveNonGeneric_WithoutCaptchaTypeSpecified()
        {
            await SetDriverUrl(WorkingUri);
            var result = await Driver.SolveCaptchaAsync(ClientKey);
            AssertSolveCaptchaResult(result);
        }

        public RecaptchaV2EnterpriseProxylessSolverTests(WebDriverFixture fixture) : base(fixture)
        {
        }
    }
}

