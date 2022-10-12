using AntiCaptchaApi.Net.Models.Solutions;
using OpenQA.Selenium;
using Selenium.AntiCaptcha;
using Selenium.Anticaptcha.Tests.TestCore;

namespace Selenium.Anticaptcha.Tests.SolverTests;

public class ImageToTextAnticaptchaTest : AnticaptchaTestBase
{
    [Fact]        
    public void ImageToTextTest()
    {
        Driver.Url = "https://en.wikipedia.org/w/index.php?title=Special:CreateAccount&returnto=Main+Page";
        var result = Driver.SolveCaptcha<ImageToTextSolution>(ClientKey,
            imageElement: Driver.FindElement(By.ClassName("fancycaptcha-image")),
            responseElement: Driver.FindElement(By.Id("mw-input-captchaWord")));
            
        AssertSolveCaptchaResult(result);
    }
    
    [Fact]        
    public void ImageToTextTestWithoutCaptchaTypeSpecified()
    {
        Driver.Url = "https://en.wikipedia.org/w/index.php?title=Special:CreateAccount&returnto=Main+Page";
        var result = Driver.SolveCaptcha<ImageToTextSolution>(ClientKey,
            imageElement: Driver.FindElement(By.ClassName("fancycaptcha-image")),
            responseElement: Driver.FindElement(By.Id("mw-input-captchaWord")));
        AssertSolveCaptchaResult(result);
    }

    public ImageToTextAnticaptchaTest(WebDriverFixture fixture) : base(fixture)
    {
    }
}