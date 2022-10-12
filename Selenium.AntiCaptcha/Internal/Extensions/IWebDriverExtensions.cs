using OpenQA.Selenium;

namespace Selenium.AntiCaptcha.Internal.Extensions;

internal static class IWebDriverExtensions
{
    public static IWebElement? FindByXPath(this IWebDriver driver, string xPath)
    {
        try
        {
            return driver.FindElement(By.XPath(xPath));
        }
        catch (Exception e)
        {
            return null;
        }
    }


    public static bool DoesAtLeastOneOfTheElementsExist(this IWebDriver driver, params string[] xPaths) =>
        xPaths.Any(xpath => driver.FindByXPath(xpath) != null);
    public static bool DoesAtLeastOneOfTheElementsExist(this IWebDriver driver, IEnumerable<string> xPaths) =>
        xPaths.Any(xpath => driver.FindByXPath(xpath) != null);
}