
using AntiCaptchaApi.Net.Models.Solutions;
using AntiCaptchaApi.Net.Responses;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using Selenium.AntiCaptcha.enums;


namespace Selenium.AntiCaptcha.Tests;

public class GeeSolverTests : SolverTestsBase
{
    [Fact]
    public void GeeTest()
    {
            
        using (var driver = new ChromeDriver(Environment.CurrentDirectory))
        {
            driver.Url = "https://www.geetest.com/en/demo";
            var allButtonParents = driver.FindElements(By.XPath("//button/parent::*"));
            var slideButton = FindSlideButton(allButtonParents);
                
            
            Assert.NotNull(slideButton);
            slideButton.Click();

            var result = driver.SolveCaptcha<GeeTestV3Solution>(ClientKey);
            
            Assert.False(result.IsErrorResponse);
            Assert.NotNull(result.Solution);
            Assert.True(result.Solution.IsValid());
        }
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
    
    
}