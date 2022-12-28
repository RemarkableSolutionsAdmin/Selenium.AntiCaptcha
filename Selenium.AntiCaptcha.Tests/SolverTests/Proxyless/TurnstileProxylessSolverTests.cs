using System.ComponentModel;
using AntiCaptchaApi.Net.Models.Solutions;
using Selenium.AntiCaptcha.Enums;
using Selenium.Anticaptcha.Tests.Core;
using Selenium.Anticaptcha.Tests.Core.Config;
using Selenium.Anticaptcha.Tests.Core.SolverTestBases;

namespace Selenium.Anticaptcha.Tests.SolverTests.Proxyless
{
    [Category(TestCategories.Proxyless)]
    public class TurnstileProxylessSolverTests : SolverTestBase<TurnstileSolution>
    {
        protected override string TestedUri { get; set; }  = TestUris.Turnstile.TurnStileDemo;
        protected override CaptchaType CaptchaType { get; set; } = CaptchaType.TurnstileProxyless;

        public TurnstileProxylessSolverTests(WebDriverFixture fixture) : base(fixture)
        {
        }
    }
}

