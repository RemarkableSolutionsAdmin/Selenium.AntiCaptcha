using System.ComponentModel;
using AntiCaptchaApi.Net.Models.Solutions;
using Selenium.AntiCaptcha.Enums;
using Selenium.Anticaptcha.Tests.Core;
using Selenium.Anticaptcha.Tests.Core.Config;
using Selenium.Anticaptcha.Tests.Core.SolverTestBases;

namespace Selenium.Anticaptcha.Tests.SolverTests.Proxyless;

[Category(TestCategories.Proxyless)]
public class HCaptchaProxylessAnticaptchaTests : SolverTestBase<HCaptchaSolution>
{
    protected override string TestedUri { get; set; } = TestUris.HCaptcha.ChartBoost;
    protected override CaptchaType CaptchaType { get; set; } = CaptchaType.HCaptchaProxyless;
    
    public HCaptchaProxylessAnticaptchaTests(WebDriverFixture fixture) : base(fixture)
    {
    }
}