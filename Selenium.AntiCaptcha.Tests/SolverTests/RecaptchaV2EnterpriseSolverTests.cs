using AntiCaptchaApi.Net.Models.Solutions;
using Selenium.AntiCaptcha;
using Selenium.AntiCaptcha.Enums;
using Selenium.Anticaptcha.Tests.Core;
using Selenium.Anticaptcha.Tests.Core.Config;
using Selenium.Anticaptcha.Tests.Core.SolverTestBases;

namespace Selenium.Anticaptcha.Tests.SolverTests
{
    public class RecaptchaV2EnterpriseSolverTests : SolverTestBase<RecaptchaSolution>
    {
        protected override string TestedUri { get; set; }  = TestUris.Recaptcha.V2.Enterprise.Steam;
        protected override CaptchaType CaptchaType { get; set; } = CaptchaType.ReCaptchaV2Enterprise;


        public RecaptchaV2EnterpriseSolverTests(WebDriverFixture fixture) : base(fixture)
        {
        }
    }
}

