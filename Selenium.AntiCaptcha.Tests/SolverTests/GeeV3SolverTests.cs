using AntiCaptchaApi.Net.Models.Solutions;
using Selenium.AntiCaptcha;
using Selenium.AntiCaptcha.Enums;
using Selenium.Anticaptcha.Tests.Core;
using Selenium.Anticaptcha.Tests.Core.Config;
using Selenium.Anticaptcha.Tests.Core.SolverTestBases;

namespace Selenium.Anticaptcha.Tests.SolverTests;

public class GeeV3SolverTests : SolverTestBase <GeeTestV3Solution>
{
    protected override string TestedUri { get; set; } = TestUris.GeeTest.V3.GeeTestV3Demo;
    protected override CaptchaType CaptchaType { get; set; } = CaptchaType.GeeTestV3Proxyless;


    public GeeV3SolverTests(WebDriverFixture fixture) : base(fixture)
    {
    }
}