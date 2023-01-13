using System.ComponentModel;
using AntiCaptchaApi.Net.Models.Solutions;
using Selenium.AntiCaptcha;
using Selenium.AntiCaptcha.Enums;
using Selenium.Anticaptcha.Tests.Core;
using Selenium.Anticaptcha.Tests.Core.Config;
using Selenium.Anticaptcha.Tests.Core.SolverTestBases;

namespace Selenium.Anticaptcha.Tests.SolverTests.Proxyless
{
    [Category(TestCategories.Proxyless)]
    public class RecaptchaV2EnterpriseProxylessSolverTests : SolverTestBase<RecaptchaSolution>
    {
        protected override string TestedUri { get; set; }  = TestUris.Recaptcha.V2.Enterprise.Steam;
        protected override CaptchaType CaptchaType { get; set; } = CaptchaType.ReCaptchaV2EnterpriseProxyless;

        [Fact]
        public async Task SpotifyTest()
        {
            await SetDriverUrl("https://www.spotify.com/pl/signup");
            var result = await Driver.SolveCaptchaAsync<RecaptchaSolution>(ClientKey);
            AssertSolveCaptchaResult(result, expectedCaptchaType: CaptchaType);
        }
        
        public RecaptchaV2EnterpriseProxylessSolverTests(WebDriverFixture fixture) : base(fixture)
        {
        }
    }
}

