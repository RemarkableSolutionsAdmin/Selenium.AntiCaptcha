using System.ComponentModel;
using AntiCaptchaApi.Net.Models.Solutions;
using Selenium.Anticaptcha.Tests.Core.SolverTestBases;
using Selenium.CaptchaIdentifier.Enums;
using Tests.Common.Config;
using Tests.Common.Core;

namespace Selenium.Anticaptcha.Tests.SolverTests.Proxyless
{
    [Category(TestCategories.Proxyless)]
    public class FunCaptchaProxylessSolverTests : SolverTestBase <FunCaptchaSolution>
    {
        protected override string TestedUri { get; set; } = TestUris.FunCaptcha.FunCaptchaDemo;
        protected override CaptchaType CaptchaType { get; set; }  = CaptchaType.FunCaptchaProxyless;
        

        public FunCaptchaProxylessSolverTests(WebDriverFixture fixture) : base(fixture)
        {
        }
    }
}

