using System.ComponentModel;
using AntiCaptchaApi.Net.Models.Solutions;
using OpenQA.Selenium;
using Selenium.AntiCaptcha;
using Selenium.AntiCaptcha.Enums;
using Selenium.AntiCaptcha.Models;
using Selenium.Anticaptcha.Tests.TestCore;

namespace Selenium.Anticaptcha.Tests.SolverTests;

[Category(TestCategories.Proxyless)]
public class ImageToTextAnticaptchaTests : SequentialAnticaptchaTestBase
{
    private SolverArguments _solverArguments;
    
    [Fact]        
    public async Task Solve_WithCaptchaTypeSpecified()
    {
        await SetDriverUrl(TestUris.ImageToText.Wikipedia);
        _solverArguments = new SolverArguments
        {
            ImageElement = Driver.FindElement(By.XPath("//img[contains(@class, 'captcha')]"))
        };
        var result = await Driver.SolveCaptchaAsync<ImageToTextSolution>(ClientKey, _solverArguments);
            
        AssertSolveCaptchaResult(result);
    }
    
    [Fact]        
    public async Task Solve_WithoutCaptchaTypeSpecified()
    {
        await SetDriverUrl(TestUris.ImageToText.Wikipedia);
        _solverArguments = new SolverArguments
        {
            ImageElement = Driver.FindElement(By.XPath("//img[contains(@class, 'captcha')]"))
        };
        var result = await Driver.SolveCaptchaAsync<ImageToTextSolution>(ClientKey, _solverArguments);
        AssertSolveCaptchaResult(result);
    }

    [Fact]        
    public async Task SolveNonGeneric_WithCaptchaTypeSpecified()
    {
        await SetDriverUrl(TestUris.ImageToText.Wikipedia);
        _solverArguments = new SolverArguments
        {
            CaptchaType = CaptchaType.ImageToText,
            ImageElement = Driver.FindElement(By.XPath("//img[contains(@class, 'captcha')]"))
        };
        var result = await Driver.SolveCaptchaAsync(ClientKey, _solverArguments);
            
        AssertSolveCaptchaResult(result);
    }
    
    [Fact]        
    public async Task SolveNonGeneric_WithoutCaptchaTypeSpecified()
    {
        await SetDriverUrl(TestUris.ImageToText.Wikipedia);
        _solverArguments = new SolverArguments
        {
            ImageElement = Driver.FindElement(By.XPath("//img[contains(@class, 'captcha')]"))
        };
        var result = await Driver.SolveCaptchaAsync(ClientKey, _solverArguments);
        AssertSolveCaptchaResult(result);
    }

    [Fact]        
    public async Task SolveNonGeneric_WithoutCaptchaTypeAndNoImageElementConfiguredSpecified()
    {
        await SetDriverUrl(TestUris.ImageToText.Wikipedia);
        var result = await Driver.SolveCaptchaAsync(ClientKey, _solverArguments);
        AssertSolveCaptchaResult(result);
    }

    public ImageToTextAnticaptchaTests(WebDriverFixture fixture) : base(fixture)
    {
    }
}