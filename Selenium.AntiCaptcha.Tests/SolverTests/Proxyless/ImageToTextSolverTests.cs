using System.ComponentModel;
using AntiCaptchaApi.Net.Models.Solutions;
using Selenium.Anticaptcha.Tests.Core.SolverTestBases;
using Selenium.CaptchaIdentifier.Enums;
using Selenium.FramesSearcher.Extensions;
using Tests.Common.Config;
using Tests.Common.Core;

namespace Selenium.Anticaptcha.Tests.SolverTests.Proxyless;

[Category(TestCategories.Proxyless)]
public class ImageToTextSolverTests : SolverTestBase<ImageToTextSolution>
{
    protected override string TestedUri { get; set; } = TestUris.ImageToText.Wikipedia;
    protected override CaptchaType CaptchaType { get; set; } = CaptchaType.ImageToText;

    public ImageToTextSolverTests(WebDriverFixture fixture) : base(fixture)
    {
        
    }

    protected override async Task BeforeTestAction()
    {
        var imageElement = Driver.FindByXPathInCurrentFrame("//img[contains(@class, 'captcha')]");
        SolverArgumentsWithCaptchaType.ImageElement = imageElement;
        SolverArgumentsWithoutCaptchaType.ImageElement = imageElement;
    }
}