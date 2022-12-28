using AntiCaptchaApi.Net.Models.Solutions;
using Selenium.AntiCaptcha;
using Selenium.AntiCaptcha.Enums;
using Selenium.AntiCaptcha.Models;
using Selenium.Anticaptcha.Tests.Core;
using Selenium.Anticaptcha.Tests.Core.Config;
using Selenium.Anticaptcha.Tests.Core.SolverTestBases;

namespace Selenium.Anticaptcha.Tests.SolverTests
{
    public class FunCaptchaSolverTests : SolverTestBase <FunCaptchaSolution>
    {
        protected override string TestedUri { get; set; } = TestUris.FunCaptcha.FunCaptchaDemo;
        protected override CaptchaType CaptchaType { get; set; }  = CaptchaType.FunCaptcha;

        public FunCaptchaSolverTests(WebDriverFixture fixture) : base(fixture)
        {
        }
    }
}

