using AntiCaptchaApi.Net.Models.Solutions;
using OpenQA.Selenium;
using Selenium.AntiCaptcha;
using Selenium.AntiCaptcha.enums;
using Selenium.Anticaptcha.Tests.TestCore;

namespace Selenium.Anticaptcha.Tests.SolverTests;

public class GeeV3SolverTests : AnticaptchaTestBase
{
    [Fact]
    public void GeeV3Test()
    {
        Driver.Url = TestUris.GeeTest.V3.W1;
        var allButtonParents = Driver.FindElements(By.XPath("//button/parent::*"));
        var slideButton = FindSlideButton(allButtonParents);
        
        Assert.NotNull(slideButton);
        Thread.Sleep(1000);
        slideButton.Click();
        var result = Driver.SolveCaptcha<GeeTestV3Solution>(ClientKey, CaptchaType.GeeTestV3);
        
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


    public GeeV3SolverTests(WebDriverFixture fixture) : base(fixture)
    {
    }
}