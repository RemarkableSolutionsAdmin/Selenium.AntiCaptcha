using AntiCaptchaApi.Net.Models.Solutions;
using OpenQA.Selenium;
using Tests.Common.Config;
using Tests.Common.Core;
using Xunit;

namespace Selenium.Anticaptcha.Tests.Core.SolverTestBases;

public abstract class GeeV4SolverTestBase : SolverTestBase<GeeTestV4Solution>
{
    protected override string TestedUri { get; set; } = TestUris.GeeTest.V4.GeeTestV4Demo;
    
    protected GeeV4SolverTestBase(WebDriverFixture fixture) : base(fixture)
    {
        
    }
    
    protected override async Task BeforeTestAction()
    {
        var allButtonParents = Driver.FindElements(By.XPath("//button/parent::*"));
        var slideButton = FindSlideButton(allButtonParents);
        
        Assert.NotNull(slideButton);
        slideButton.Click();
        await Task.Delay(1000);
    }
    
    
    private static IWebElement FindSlideButton(IEnumerable<IWebElement> buttons)
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
            // ignored
        }

        return null;
    }


}