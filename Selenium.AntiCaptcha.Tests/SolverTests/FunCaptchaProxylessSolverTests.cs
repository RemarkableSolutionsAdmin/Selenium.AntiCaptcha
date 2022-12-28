using System.ComponentModel;
using AntiCaptchaApi.Net.Models.Solutions;
using Selenium.AntiCaptcha;
using Selenium.AntiCaptcha.Enums;
using Selenium.Anticaptcha.Tests.Core;
using Selenium.Anticaptcha.Tests.Core.Config;
using Selenium.Anticaptcha.Tests.Core.SolverTestBases;

namespace Selenium.Anticaptcha.Tests.SolverTests
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

