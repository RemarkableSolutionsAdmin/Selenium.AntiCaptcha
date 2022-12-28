using System.ComponentModel;
using AntiCaptchaApi.Net.Models.Solutions;
using OpenQA.Selenium;
using Selenium.AntiCaptcha;
using Selenium.AntiCaptcha.Enums;
using Selenium.Anticaptcha.Tests.Core;
using Selenium.Anticaptcha.Tests.Core.Config;
using Selenium.Anticaptcha.Tests.Core.SolverTestBases;

namespace Selenium.Anticaptcha.Tests.SolverTests;


[Category(TestCategories.Proxyless)]
public class GeeV4ProxylessSolverTests : GeeV4SolverTestBase
{
    protected override CaptchaType CaptchaType { get; set; } = CaptchaType.GeeTestV4Proxyless;

    public GeeV4ProxylessSolverTests(WebDriverFixture fixture) : base(fixture)
    {
    }
}