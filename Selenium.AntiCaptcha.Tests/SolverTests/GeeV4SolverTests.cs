using AntiCaptchaApi.Net.Models.Solutions;
using OpenQA.Selenium;
using Selenium.AntiCaptcha;
using Selenium.AntiCaptcha.Enums;
using Selenium.Anticaptcha.Tests.TestCore;

namespace Selenium.Anticaptcha.Tests.SolverTests;

public class GeeV4SolverTests : SequentialAnticaptchaTestBase
{
    [Fact]
    public async Task Solve_CaptchaTypeSpecified()
    {
        SetDriverUrl(TestUris.GeeTest.V4.W1);
        var allButtonParents = Driver.FindElements(By.XPath("//button/parent::*"));
        var slideButton = FindSlideButton(allButtonParents);
        
        Assert.NotNull(slideButton);
        slideButton.Click();
        Thread.Sleep(1000);
        var result = await Driver.SolveCaptchaAsync<GeeTestV4Solution>(ClientKey);

        AssertSolveCaptchaResult(result);
    }
        
    [Fact]
    public async Task Solve_WithoutCaptchaTypeSpecified()
    {
        SetDriverUrl(TestUris.GeeTest.V4.W1);
        var allButtonParents = Driver.FindElements(By.XPath("//button/parent::*"));
        var slideButton = FindSlideButton(allButtonParents);
        
        Assert.NotNull(slideButton);
        slideButton.Click();
        Thread.Sleep(1000);
        var result = await Driver.SolveCaptchaAsync<GeeTestV4Solution>(ClientKey);

        AssertSolveCaptchaResult(result);
    }

    [Fact]
    public async Task SolveNonGeneric_CaptchaTypeSpecified()
    {
        SetDriverUrl(TestUris.GeeTest.V4.W1);
        var allButtonParents = Driver.FindElements(By.XPath("//button/parent::*"));
        var slideButton = FindSlideButton(allButtonParents);
        
        Assert.NotNull(slideButton);
        slideButton.Click();
        Thread.Sleep(1000);
        var result = await Driver.SolveCaptchaAsync(ClientKey);

        AssertSolveCaptchaResult(result);
    }
        
    [Fact]
    public async Task SolveNonGeneric_WithoutCaptchaTypeSpecified()
    {
        SetDriverUrl(TestUris.GeeTest.V4.W1);
        var allButtonParents = Driver.FindElements(By.XPath("//button/parent::*"));
        var slideButton = FindSlideButton(allButtonParents);
        
        Assert.NotNull(slideButton);
        slideButton.Click();
        Thread.Sleep(1000);
        var result = await Driver.SolveCaptchaAsync(ClientKey);

        AssertSolveCaptchaResult(result);
    }


    private IWebElement FindSlideButton(IEnumerable<IWebElement> buttons)
    {
        try
        {

            foreach (var button in buttons)
            {
                var buttonText = button.Text;

                if (buttonText.Contains("Slide"))
                {
                    return button;
                }
            }
        }
        catch (Exception)
        {
            
        }

        return null;
    }


    public GeeV4SolverTests(WebDriverFixture fixture) : base(fixture)
    {
    }
}