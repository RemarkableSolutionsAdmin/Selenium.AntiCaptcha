using AntiCaptchaApi.Net.Models.Solutions;
using Selenium.Anticaptcha.Tests.Core.SolverTestBases;
using Selenium.CaptchaIdentifier.Enums;
using Tests.Common.Config;
using Tests.Common.Core;

namespace Selenium.Anticaptcha.Tests.SolverTests
{
    public class AntiGateSolverTests : SolverTestBase  <AntiGateSolution>
    {
        protected override string TestedUri { get; set; } = TestUris.AntiGate.AntiCaptchaTuttorialAntiGate;
        protected override CaptchaType CaptchaType { get; set; }  = CaptchaType.AntiGate;

        public AntiGateSolverTests(WebDriverFixture fixture) : base(fixture)
        {
            SolverArgumentsWithoutCaptchaType.TemplateName = "CloudFlare cookies for a proxy";
            SolverArgumentsWithCaptchaType.TemplateName = "CloudFlare cookies for a proxy";
        }
    }
}

