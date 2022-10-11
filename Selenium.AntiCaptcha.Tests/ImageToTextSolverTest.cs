﻿using AntiCaptchaApi.Net.Models.Solutions;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using Selenium.AntiCaptcha.enums;

namespace Selenium.AntiCaptcha.Tests;

public class ImageToTextSolverTest : SolverTestsBase
{
    [Fact]        
    public void ImageToTextTest()
    {
        using (var driver = new ChromeDriver())
        {
            driver.Url = "https://en.wikipedia.org/w/index.php?title=Special:CreateAccount&returnto=Main+Page";
            var result = driver.SolveCaptcha<RawSolution>(ClientKey,
                imageElement: driver.FindElement(By.ClassName("fancycaptcha-image")),
                responseElement: driver.FindElement(By.Id("mw-input-captchaWord")),
                captchaType: CaptchaType.ImageToText);
            
            AssertSolveCaptchaResult(result);
        }
    }
    [Fact]        
    public void ImageToTextTestWithoutCaptchaTypeSpecified()
    {
        using (var driver = new ChromeDriver())
        {
            driver.Url = "https://en.wikipedia.org/w/index.php?title=Special:CreateAccount&returnto=Main+Page";
            var result = driver.SolveCaptcha<ImageToTextSolution>(ClientKey,
                imageElement: driver.FindElement(By.ClassName("fancycaptcha-image")),
                responseElement: driver.FindElement(By.Id("mw-input-captchaWord")));
            AssertSolveCaptchaResult(result);
        }
    }
}