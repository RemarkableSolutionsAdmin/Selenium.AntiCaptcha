using AntiCaptchaApi.Net.Models.Solutions;
using OpenQA.Selenium;
using Selenium.AntiCaptcha;
using Selenium.AntiCaptcha.Enums;
using Selenium.Anticaptcha.Tests.TestCore;

namespace Selenium.Anticaptcha.Tests.SolverTests;

public class GeeV4SolverTests : AnticaptchaTestBase
{
    [Fact]
    public void Solve_CaptchaTypeSpecified()
    {
        SetDriverUrl(TestUris.GeeTest.V4.W1);
        var allButtonParents = Driver.FindElements(By.XPath("//button/parent::*"));
        var slideButton = FindSlideButton(allButtonParents);
        
        Assert.NotNull(slideButton);
        slideButton.Click();
        Thread.Sleep(1000);
        var result = Driver.SolveCaptcha<GeeTestV4Solution>(ClientKey, CaptchaType.GeeTestV4, proxyConfig: TestEnvironment.GetCurrentTestProxyConfig());

        AssertSolveCaptchaResult(result);
    }
        
    [Fact]
    public void Solve_WithoutCaptchaTypeSpecified()
    {
        SetDriverUrl(TestUris.GeeTest.V4.W1);
        var allButtonParents = Driver.FindElements(By.XPath("//button/parent::*"));
        var slideButton = FindSlideButton(allButtonParents);
        
        Assert.NotNull(slideButton);
        slideButton.Click();
        Thread.Sleep(1000);
        var result = Driver.SolveCaptcha<GeeTestV4Solution>(ClientKey, proxyConfig: TestEnvironment.GetCurrentTestProxyConfig());

        AssertSolveCaptchaResult(result);
    }

    [Fact]
    public void SolveNonGeneric_CaptchaTypeSpecified()
    {
        SetDriverUrl(TestUris.GeeTest.V4.W1);
        var allButtonParents = Driver.FindElements(By.XPath("//button/parent::*"));
        var slideButton = FindSlideButton(allButtonParents);
        
        Assert.NotNull(slideButton);
        slideButton.Click();
        Thread.Sleep(1000);
        var result = Driver.SolveCaptcha(ClientKey, CaptchaType.GeeTestV4, proxyConfig: TestEnvironment.GetCurrentTestProxyConfig());

        AssertSolveCaptchaResult(result);
    }
        
    [Fact]
    public void SolveNonGeneric_WithoutCaptchaTypeSpecified()
    {
        SetDriverUrl(TestUris.GeeTest.V4.W1);
        var allButtonParents = Driver.FindElements(By.XPath("//button/parent::*"));
        var slideButton = FindSlideButton(allButtonParents);
        
        Assert.NotNull(slideButton);
        slideButton.Click();
        Thread.Sleep(1000);
        var result = Driver.SolveCaptcha(ClientKey, proxyConfig: TestEnvironment.GetCurrentTestProxyConfig());

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