using System.Text;
using OpenQA.Selenium;

namespace Selenium.AntiCaptcha.Internal.Extensions;

internal static class IWebDriverExtensions
{
    public static void ForEachFrame(this IWebDriver driver, Action? action)
    {
        try
        {
            action?.Invoke();
            var frames = driver.GetCurrentFrames();
            foreach (var frame in frames)
            {
                driver.SwitchTo().Frame(frame);
                driver.ForEachFrame(action);
            }

            driver.SwitchTo().DefaultContent();
        }
        catch (Exception)
        {
            //ignore
        }
    }
    
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

    public static IEnumerable<IWebElement> GetCurrentFrames(this IWebDriver driver)
    {
        return driver.FindElements(By.XPath("//iframe"));
    }

    public static IWebElement? FindByXPathAllFrames(this IWebDriver driver, string xPath)
    {
        try
        {
            var result = driver.FindByXPath(xPath);

            if (result != null)
            {
                return result;
            }

            var frames = driver.GetCurrentFrames();
            foreach (var frame in frames)
            {
                driver.SwitchTo().Frame(frame);
                result = driver.FindByXPathAllFrames(xPath);
                if (result != null)
                {
                    return result;
                }
            }

            return null;
        }
        catch (Exception)
        {
            return null;
        }
        finally
        {
            driver.SwitchTo().DefaultContent();
        }
    }
    
    public static List<IWebElement> FindManyByXPathAllFrames(this IWebDriver driver, string xPath)
    {
        var result = new List<IWebElement>();
        try
        {
            driver.ForEachFrame(() => result.AddRange(driver.FindManyByXPathCurrentFrame(xPath)));
            return result;
        }
        catch (Exception)
        {
            return new List<IWebElement>();
        }
        finally
        {
            driver.SwitchTo().DefaultContent();
        }
    }

    public static List<IWebElement> FindManyByXPathCurrentFrame(this IWebDriver driver, string xPath)
    {
        try
        {
            return driver.FindElements(By.XPath(xPath)).ToList();
        }
        catch (Exception e)
        {
            return new List<IWebElement>();
        }
    }

    public static string GetAllPageSource(this IWebDriver driver)
    {
        var result = GatherAllPageSourcesInFrames(driver);
        driver.SwitchTo().DefaultContent();
        return result;
    }

    private static string GatherAllPageSourcesInFrames(IWebDriver driver)
    {
        var builder = new StringBuilder();

        builder.Append(driver.PageSource);
        var iframes = driver.FindManyByXPathCurrentFrame("//iframe");
        
        foreach (var iframe in iframes)
        {
            try
            {
                driver.SwitchTo().Frame(iframe);
                builder.Append(GatherAllPageSourcesInFrames(driver));
            }
            catch (Exception)
            {
                // ignored
            }
        }
        
        return builder.ToString();
    }
    
    
    public static bool DoesAtLeastOneOfTheElementsExist(this IWebDriver driver, params string[] xPaths) =>
        xPaths.Any(xpath => driver.FindByXPath(xpath) != null);
    public static bool DoesAtLeastOneOfTheElementsExist(this IWebDriver driver, IEnumerable<string> xPaths) =>
        xPaths.Any(xpath => driver.FindByXPath(xpath) != null);
}