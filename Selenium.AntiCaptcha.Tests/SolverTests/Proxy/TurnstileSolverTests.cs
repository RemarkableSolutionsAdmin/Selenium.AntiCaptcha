using AntiCaptchaApi.Net.Models.Solutions;
using Selenium.Anticaptcha.Tests.Core.SolverTestBases;
using Selenium.CaptchaIdentifier.Enums;
using Tests.Common.Config;
using Tests.Common.Core;
using Xunit.Abstractions;

namespace Selenium.Anticaptcha.Tests.SolverTests.Proxy
{
    public class TurnstileSolverTests : SolverTestBase<TurnstileSolution>
    {
        protected override string TestedUri { get; set; }  = TestUris.Turnstile.TurnStileDemo;
        protected override CaptchaType CaptchaType { get; set; } =  CaptchaType.Turnstile;
        
        public TurnstileSolverTests(WebDriverFixture fixture, ITestOutputHelper output) : base(fixture, output)
        {
        }
    }
}

