using System.ComponentModel;
using AntiCaptchaApi.Net.Models.Solutions;
using OpenQA.Selenium;
using Selenium.AntiCaptcha.Enums;
using Selenium.AntiCaptcha.Internal.Extensions;
using Selenium.Anticaptcha.Tests.Core;
using Selenium.Anticaptcha.Tests.Core.Config;
using Selenium.Anticaptcha.Tests.Core.SolverTestBases;

namespace Selenium.Anticaptcha.Tests.SolverTests;

[Category(TestCategories.Proxyless)]
public class ImageToTextAnticaptchaTests : SolverTestBase<ImageToTextSolution>
{
    protected override string TestedUri { get; set; } = TestUris.ImageToText.Wikipedia;
    protected override CaptchaType CaptchaType { get; set; } = CaptchaType.ImageToText;

    public ImageToTextAnticaptchaTests(WebDriverFixture fixture) : base(fixture)
    {
        
    }

    protected override async Task BeforeTestAction()
    {
        var imageElement = Driver.FindByXPathInCurrentFrame("//img[contains(@class, 'captcha')]");
        SolverArgumentsWithCaptchaType.ImageElement = imageElement;
        SolverArgumentsWithoutCaptchaType.ImageElement = imageElement;
    
    }
}