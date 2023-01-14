using System.ComponentModel;
using AntiCaptchaApi.Net.Models.Solutions;
using Selenium.Anticaptcha.Tests.Core.SolverTestBases;
using Selenium.CaptchaIdentifier.Enums;
using Tests.Common.Config;
using Tests.Common.Core;

namespace Selenium.Anticaptcha.Tests.SolverTests.Proxyless
{
    [Category(TestCategories.Proxyless)]
    public class RecaptchaV2EnterpriseProxylessSolverTests : SolverTestBase<RecaptchaSolution>
    {
        protected override string TestedUri { get; set; }  = TestUris.Recaptcha.V2.Enterprise.Steam;
        protected override CaptchaType CaptchaType { get; set; } = CaptchaType.ReCaptchaV2EnterpriseProxyless;
        
        public RecaptchaV2EnterpriseProxylessSolverTests(WebDriverFixture fixture) : base(fixture)
        {
        }
    }
}

