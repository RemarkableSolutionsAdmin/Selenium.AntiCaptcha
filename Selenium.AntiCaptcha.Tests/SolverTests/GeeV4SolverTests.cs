using AntiCaptchaApi.Net.Models.Solutions;
using OpenQA.Selenium;
using Selenium.AntiCaptcha;
using Selenium.AntiCaptcha.enums;
using Selenium.Anticaptcha.Tests.TestCore;

namespace Selenium.Anticaptcha.Tests.SolverTests;

public class GeeV4SolverTests : AnticaptchaTestBase
{
    [Theory]
    [InlineData(TestUris.GeeTest.V4.W1)]
    public void GeeV4Test(string websiteUrl)
    {
        Driver.Url = "https://www.geetest.com/en/demo";
        var allButtonParents = Driver.FindElements(By.XPath("//button/parent::*"));
        var slideButton = FindSlideButton(allButtonParents);
        
        Assert.NotNull(slideButton);
        slideButton.Click();

        var result = Driver.SolveCaptcha<GeeTestV4Solution>(ClientKey, CaptchaType.GeeTestV4);
            
        Assert.False(result.IsErrorResponse);
        Assert.NotNull(result.Solution);
        Assert.True(result.Solution.IsValid());
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