using AntiCaptchaApi.Net.Models.Solutions;
using OpenQA.Selenium;
using Selenium.AntiCaptcha;
using Selenium.AntiCaptcha.Enums;
using Selenium.AntiCaptcha.Models;
using Selenium.Anticaptcha.Tests.TestCore;

namespace Selenium.Anticaptcha.Tests.SolverTests;

public class ImageToTextAnticaptchaTest : SequentialAnticaptchaTestBase
{
    private SolverAdditionalArguments _solverAdditionalArguments;
    
    [Fact]        
    public void Solve_WithCaptchaTypeSpecified()
    {
        SetDriverUrl(TestUris.ImageToText.W1);
        _solverAdditionalArguments = new SolverAdditionalArguments
        {
            ImageElement = Driver.FindElement(By.XPath("//img[contains(@class, 'captcha')]"))
        };
        var result = Driver.SolveCaptcha<ImageToTextSolution>(ClientKey, _solverAdditionalArguments);
            
        AssertSolveCaptchaResult(result);
    }
    
    [Fact]        
    public void Solve_WithoutCaptchaTypeSpecified()
    {
        SetDriverUrl(TestUris.ImageToText.W1);
        _solverAdditionalArguments = new SolverAdditionalArguments
        {
            ImageElement = Driver.FindElement(By.XPath("//img[contains(@class, 'captcha')]"))
        };
        var result = Driver.SolveCaptcha<ImageToTextSolution>(ClientKey, _solverAdditionalArguments);
        AssertSolveCaptchaResult(result);
    }

    [Fact]        
    public void SolveNonGeneric_WithCaptchaTypeSpecified()
    {
        SetDriverUrl(TestUris.ImageToText.W1);
        _solverAdditionalArguments = new SolverAdditionalArguments
        {
            CaptchaType = CaptchaType.ImageToText,
            ImageElement = Driver.FindElement(By.XPath("//img[contains(@class, 'captcha')]"))
        };
        var result = Driver.SolveCaptcha(ClientKey, _solverAdditionalArguments);
            
        AssertSolveCaptchaResult(result);
    }
    
    [Fact]        
    public void SolveNonGeneric_WithoutCaptchaTypeSpecified()
    {
        SetDriverUrl(TestUris.ImageToText.W1);
        _solverAdditionalArguments = new SolverAdditionalArguments
        {
            ImageElement = Driver.FindElement(By.XPath("//img[contains(@class, 'captcha')]"))
        };
        var result = Driver.SolveCaptcha(ClientKey, _solverAdditionalArguments);
        AssertSolveCaptchaResult(result);
    }

    public ImageToTextAnticaptchaTest(WebDriverFixture fixture) : base(fixture)
    {
    }
}