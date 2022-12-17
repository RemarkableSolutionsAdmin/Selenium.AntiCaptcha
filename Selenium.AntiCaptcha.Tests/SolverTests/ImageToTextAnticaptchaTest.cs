﻿using System.ComponentModel;
using AntiCaptchaApi.Net.Models.Solutions;
using OpenQA.Selenium;
using Selenium.AntiCaptcha;
using Selenium.AntiCaptcha.Enums;
using Selenium.AntiCaptcha.Models;
using Selenium.Anticaptcha.Tests.TestCore;

namespace Selenium.Anticaptcha.Tests.SolverTests;

[Category(TestCategories.Proxyless)]
public class ImageToTextAnticaptchaTest : SequentialAnticaptchaTestBase
{
    private SolverAdditionalArguments _solverAdditionalArguments;
    
    [Fact]        
    public async Task Solve_WithCaptchaTypeSpecified()
    {
        await SetDriverUrl(TestUris.ImageToText.W1);
        _solverAdditionalArguments = new SolverAdditionalArguments
        {
            ImageElement = Driver.FindElement(By.XPath("//img[contains(@class, 'captcha')]"))
        };
        var result = await Driver.SolveCaptchaAsync<ImageToTextSolution>(ClientKey, _solverAdditionalArguments);
            
        AssertSolveCaptchaResult(result);
    }
    
    [Fact]        
    public async Task Solve_WithoutCaptchaTypeSpecified()
    {
        await SetDriverUrl(TestUris.ImageToText.W1);
        _solverAdditionalArguments = new SolverAdditionalArguments
        {
            ImageElement = Driver.FindElement(By.XPath("//img[contains(@class, 'captcha')]"))
        };
        var result = await Driver.SolveCaptchaAsync<ImageToTextSolution>(ClientKey, _solverAdditionalArguments);
        AssertSolveCaptchaResult(result);
    }

    [Fact]        
    public async Task SolveNonGeneric_WithCaptchaTypeSpecified()
    {
        await SetDriverUrl(TestUris.ImageToText.W1);
        _solverAdditionalArguments = new SolverAdditionalArguments
        {
            CaptchaType = CaptchaType.ImageToText,
            ImageElement = Driver.FindElement(By.XPath("//img[contains(@class, 'captcha')]"))
        };
        var result = await Driver.SolveCaptchaAsync(ClientKey, _solverAdditionalArguments);
            
        AssertSolveCaptchaResult(result);
    }
    
    [Fact]        
    public async Task SolveNonGeneric_WithoutCaptchaTypeSpecified()
    {
        await SetDriverUrl(TestUris.ImageToText.W1);
        _solverAdditionalArguments = new SolverAdditionalArguments
        {
            ImageElement = Driver.FindElement(By.XPath("//img[contains(@class, 'captcha')]"))
        };
        var result = await Driver.SolveCaptchaAsync(ClientKey, _solverAdditionalArguments);
        AssertSolveCaptchaResult(result);
    }

    public ImageToTextAnticaptchaTest(WebDriverFixture fixture) : base(fixture)
    {
    }
}