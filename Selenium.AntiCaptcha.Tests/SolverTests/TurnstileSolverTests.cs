using System.ComponentModel;
using AntiCaptchaApi.Net.Models.Solutions;
using Selenium.AntiCaptcha;
using Selenium.AntiCaptcha.Enums;
using Selenium.Anticaptcha.Tests.Core;
using Selenium.Anticaptcha.Tests.Core.Config;
using Selenium.Anticaptcha.Tests.Core.SolverTestBases;

namespace Selenium.Anticaptcha.Tests.SolverTests
{
    public class TurnstileSolverTests : SolverTestBase<TurnstileSolution>
    {
        protected override string TestedUri { get; set; }  = TestUris.Turnstile.TurnStileDemo;
        protected override CaptchaType CaptchaType { get; set; } =  CaptchaType.Turnstile;
        
        public TurnstileSolverTests(WebDriverFixture fixture) : base(fixture)
        {
        }
    }
}

