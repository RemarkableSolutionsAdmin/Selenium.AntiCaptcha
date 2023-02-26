using System.ComponentModel;
using Selenium.Anticaptcha.Tests.Core.SolverTestBases;
using Selenium.CaptchaIdentifier.Enums;
using Tests.Common.Config;
using Tests.Common.Core;
using Xunit.Abstractions;

namespace Selenium.Anticaptcha.Tests.SolverTests.Proxyless;


[Category(TestCategories.Proxyless)]
public class GeeV4ProxylessSolverTests : GeeV4SolverTestBase
{
    protected override CaptchaType CaptchaType { get; set; } = CaptchaType.GeeTestV4Proxyless;

    public GeeV4ProxylessSolverTests(WebDriverFixture fixture, ITestOutputHelper output) : base(fixture, output)
    {
    }
}