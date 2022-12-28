using Selenium.AntiCaptcha.Enums;
using Selenium.Anticaptcha.Tests.Core;
using Selenium.Anticaptcha.Tests.Core.SolverTestBases;

namespace Selenium.Anticaptcha.Tests.SolverTests.Proxy;

public class GeeV4SolverTests : GeeV4SolverTestBase
{
    protected override CaptchaType CaptchaType { get; set; } = CaptchaType.GeeTestV4;

    public GeeV4SolverTests(WebDriverFixture fixture) : base(fixture)
    {
    }
}