using OpenQA.Selenium;

namespace Selenium.AntiCaptcha.Internal.Extensions;

internal static class IWebDriverExtensions
{
    public static IWebElement? FindByXPath(this IWebDriver driver, string xpath)
    {
        try
        {
            return driver.FindElement(By.XPath(xpath));
        }
        catch (Exception e)
        {
            return null;
        }
    }
}