using AntiCaptchaApi.Net.Models.Solutions;
using Selenium.Anticaptcha.Tests.Core.SolverTestBases;
using Selenium.CaptchaIdentifier.Enums;
using Tests.Common.Config;
using Tests.Common.Core;
using Xunit.Abstractions;

namespace Selenium.Anticaptcha.Tests.SolverTests.Proxy;

public class GeeV3SolverTests : SolverTestBase <GeeTestV3Solution>
{
    protected override string TestedUri { get; set; } = TestUris.GeeTest.V3.GeeTestV3Demo;
    protected override CaptchaType CaptchaType { get; set; } = CaptchaType.GeeTestV3;


    public GeeV3SolverTests(WebDriverFixture fixture, ITestOutputHelper output) : base(fixture, output)
    {
    }
}