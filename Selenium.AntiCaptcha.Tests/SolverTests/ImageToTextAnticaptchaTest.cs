using AntiCaptchaApi.Net.Models.Solutions;
using OpenQA.Selenium;
using Selenium.AntiCaptcha;
using Selenium.Anticaptcha.Tests.TestCore;

namespace Selenium.Anticaptcha.Tests.SolverTests;

public class ImageToTextAnticaptchaTest : AnticaptchaTestBase
{
    [Fact]        
    public void Solve_WithCaptchaTypeSpecified()
    {
        SetDriverUrl(TestUris.ImageToText.W1);
        var result = Driver.SolveCaptcha<ImageToTextSolution>(ClientKey,
            imageElement: Driver.FindElement(By.ClassName("fancycaptcha-image")),
            responseElement: Driver.FindElement(By.Id("mw-input-captchaWord")));
            
        AssertSolveCaptchaResult(result);
    }
    
    [Fact]        
    public void Solve_WithoutCaptchaTypeSpecified()
    {
        SetDriverUrl(TestUris.ImageToText.W1);
        var result = Driver.SolveCaptcha<ImageToTextSolution>(ClientKey,
            imageElement: Driver.FindElement(By.ClassName("fancycaptcha-image")),
            responseElement: Driver.FindElement(By.Id("mw-input-captchaWord")));
        AssertSolveCaptchaResult(result);
    }

    [Fact]        
    public void SolveNonGeneric_WithCaptchaTypeSpecified()
    {
        SetDriverUrl(TestUris.ImageToText.W1);
        var result = Driver.SolveCaptcha(ClientKey,
            imageElement: Driver.FindElement(By.ClassName("fancycaptcha-image")),
            responseElement: Driver.FindElement(By.Id("mw-input-captchaWord")));
            
        AssertSolveCaptchaResult(result);
    }
    
    [Fact]        
    public void SolveNonGeneric_WithoutCaptchaTypeSpecified()
    {
        SetDriverUrl(TestUris.ImageToText.W1);
        var result = Driver.SolveCaptcha(ClientKey,
            imageElement: Driver.FindElement(By.ClassName("fancycaptcha-image")),
            responseElement: Driver.FindElement(By.Id("mw-input-captchaWord")));
        AssertSolveCaptchaResult(result);
    }

    public ImageToTextAnticaptchaTest(WebDriverFixture fixture) : base(fixture)
    {
    }
}