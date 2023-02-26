using System.ComponentModel;
using AntiCaptchaApi.Net.Models.Solutions;
using Selenium.Anticaptcha.Tests.Core.SolverTestBases;
using Selenium.CaptchaIdentifier.Enums;
using Tests.Common.Config;
using Tests.Common.Core;
using Xunit.Abstractions;

namespace Selenium.Anticaptcha.Tests.SolverTests.Proxyless;

[Category(TestCategories.Proxyless)]
public class HCaptchaProxylessAnticaptchaTests : SolverTestBase<HCaptchaSolution>
{
    protected override string TestedUri { get; set; } = TestUris.HCaptcha.ChartBoost;
    protected override CaptchaType CaptchaType { get; set; } = CaptchaType.HCaptchaProxyless;
    
    public HCaptchaProxylessAnticaptchaTests(WebDriverFixture fixture, ITestOutputHelper output) : base(fixture, output)
    {
    }
}