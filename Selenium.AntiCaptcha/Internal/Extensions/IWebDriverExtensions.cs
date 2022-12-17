using System.Text;
using OpenQA.Selenium;
using Selenium.AntiCaptcha.Internal.Models;

namespace Selenium.AntiCaptcha.Internal.Extensions;

internal static class IWebDriverExtensions
{
    private const int MaxSearchDepth = 10;
    
    public static void ForEachFrame(this IWebDriver driver, Action? action)
    {
        var currentFrame = driver.GetCurrentFrame();
        try
        {
            action?.Invoke();
            var frames = driver.FindIFramesInCurrentFrame();
            foreach (var frame in frames)
            {
                if (!driver.TryToSwitchToFrame(frame))
                    continue;
                driver.ForEachFrame(action);
                driver.SwitchTo().ParentFrame();
            }

        }
        catch (Exception)
        {
            //ignore
        }
        finally
        {
            driver.TryToSwitchToFrame(currentFrame);
        }
    }
    
    public static IWebElement? FindByXPathInCurrentFrame(this IWebDriver driver, params string[] xPathPatterns)
    {
        foreach (var xPathPattern in xPathPatterns)
        {
            try
            {
                return driver.FindElement(By.XPath(xPathPattern));
            }
            catch
            {
                //ignore
            }
        }

        return null;
    }
    

    public static IWebElement? FindByXPathAllFrames(this IWebDriver driver, params string[] xPathPatterns)
    {
        if (!xPathPatterns.Any())
            return null;
        
        var result = driver.FindByXPathInCurrentFrame(xPathPatterns);
        if (result != null)
        {
            return result;
        }

        var childrenFrames = driver.FindIFramesInCurrentFrame();
        foreach (var frame in childrenFrames)
        {
            if (driver.TryToSwitchToFrame(frame))
            {
                result = driver.FindByXPathAllFrames(xPathPatterns);
                if (result != null)
                {
                    return result;
                }
            };
        }

        driver.SwitchTo().ParentFrame();
        return null;
    }

    public static IList<ExtendedWebElement> FindIFramesInCurrentFrame(this IWebDriver driver)
    {
        return driver.FindElements(By.TagName("iframe")).Select(x => new ExtendedWebElement(x)).ToList();
    }

    public static IEnumerable<IWebElement> FindManyByXPathAllFrames(this IWebDriver driver, params string[] xPathPatterns)
    {
        if (!xPathPatterns.Any())
            return new List<IWebElement>();
        var currentFrame = driver.GetCurrentFrame();
        var result = new List<IWebElement>();
        try
        {
            driver.ForEachFrame(() => result.AddRange(driver.FindManyByXPathCurrentFrame(xPathPatterns)));
            return result;
        }
        catch (Exception)
        {
            return new List<IWebElement>();
        }
        finally
        {
            driver.TryToSwitchToFrame(currentFrame);
        }
    }

    public static List<IWebElement> FindManyByXPathCurrentFrame(this IWebDriver driver, params string[] xPathPatterns)
    {
        var results = new List<IWebElement>();
        foreach (var xPathPattern in xPathPatterns)
        {
            try
            {
                results.AddRange(driver.FindElements(By.XPath(xPathPattern)).ToList());
            }
            catch (Exception)
            {
                // continue;
            }
        }

        return results;
    }

    public static string GetAllPageSource(this IWebDriver driver)
    {
        var currentFrame = driver.GetCurrentFrame();
        if(currentFrame?.IsRoot is false)
            driver.SwitchTo().DefaultContent();
        var result = GatherAllPageSourcesInFrames(driver);
        driver.TryToSwitchToFrame(currentFrame);
        return result;
    }
    

    public static ExtendedWebElement? GetCurrentRootWebElement(this IWebDriver driver)
    {
        var inputWebElements = driver.FindElements(By.TagName("html"));
        if (inputWebElements.Count != 1)
        {
            //TODO! Exception.
            return null;
        }

        return new ExtendedWebElement(inputWebElements.Single());
    }

    //Must start from root.
    public static TreeNode<ExtendedWebElement>? GetFramesTree(this IWebDriver driver)
    {
        var currentHtmlElement = driver.GetCurrentFrame();

        if (currentHtmlElement == null)
            return null;
        
        var result = new TreeNode<ExtendedWebElement>(currentHtmlElement);
        var iframes = driver.FindIFramesInCurrentFrame();

        foreach (var frame in iframes)
        {
            try
            {
                var childNode = result.AddChild(frame);
                driver.SwitchTo().Frame(childNode.Value.WebElement);
                var grandChildren = driver.GetFramesTree();
                if (grandChildren != null)
                {
                    foreach (var grandChild in grandChildren.Children)
                    {
                        childNode.AddChild(grandChild.Value);
                    }   
                }
            }
            catch (Exception)
            {
                // ignore
            }
            finally
            {
                driver.SwitchTo().ParentFrame();
            }
        }
        return result;
    }


    public static TreeNode<ExtendedWebElement> GetFullFramesTree(this IWebDriver driver)
    {
        var originalFrame = driver.GetCurrentFrame();
        TreeNode<ExtendedWebElement>? framesTree = null;
        try
        {
            driver.SwitchTo().DefaultContent();
            framesTree = driver.GetFramesTree();
            return framesTree; //TODO: Throw an error if null. 
        }
        catch (Exception)
        {
            return null; //TODO. Throw an error.
        }
        finally
        {
            driver.TraverseToFrame(framesTree, originalFrame);
        }
    }

    public static void TraverseToFrame(this IWebDriver driver, TreeNode<ExtendedWebElement>? framesTree, ExtendedWebElement? destinationFrame)
    {
        driver.SwitchTo().DefaultContent();
        if (framesTree == null || destinationFrame == null)
        {
            return;
        }
        
        var destinationNode = framesTree.FindInTree(destinationFrame);

        if (destinationNode == null)
        {
            return;
        }

        var path = destinationNode.GetNodesPathToParent();
        
        foreach (var node in path)
        {
            if (!driver.TryToSwitchToFrame(node.Value))
            {
                // todo: throw
            }
        }
    }

    public static bool TryToSwitchToFrame(this IWebDriver driver, ExtendedWebElement? element)
    {
        if (element == null)
            return false;

        if (element.IsRoot)
        {
            driver.SwitchTo().DefaultContent();
            return true;
        }
        
        try
        {
            driver.SwitchTo().Frame(element.WebElement);
            return true;
        }
        catch (Exception)
        {
            return false;
        }
    }
    
    public static ExtendedWebElement? GetCurrentFrame(this IWebDriver driver) //TODO Refresh cache.
    {
        var x = driver.PageSource;
        var inputWebElement = driver.GetCurrentRootWebElement();
        var inputChildrenFrames = FindIFramesInCurrentFrame(driver);
        
        if (inputWebElement == null)
        {
            return null;
        }

        driver.SwitchTo().ParentFrame();
        
        var childrenFrames = FindIFramesInCurrentFrame(driver);

        if (childrenFrames.All(frame => inputChildrenFrames.Any(frame2 => frame2.Equals(frame))))
        {
            return new ExtendedWebElement(inputWebElement, true);
        }
        
        foreach (var frame in childrenFrames)
        {
            if (driver.TryToSwitchToFrame(frame))
            {
                var currentRootWebElement = driver.GetCurrentRootWebElement();
                if (Equals(currentRootWebElement, inputWebElement))
                {
                    return new ExtendedWebElement(frame.WebElement);
                }
            }
        }

        return null;
    }
    

    private static string GatherAllPageSourcesInFrames(IWebDriver driver)
    {
        var builder = new StringBuilder();
        builder.Append(driver.PageSource);
        
        var childrenFrames = driver.FindIFramesInCurrentFrame();
        
        foreach (var iframe in childrenFrames)
        {
            if (driver.TryToSwitchToFrame(iframe))
            {
                builder.Append(GatherAllPageSourcesInFrames(driver));
                driver.SwitchTo().ParentFrame();
            }
        }
        
        return builder.ToString();
    }
    
    
    public static bool DoesAtLeastOneOfTheElementsExist(this IWebDriver driver, params string[] xPaths) =>
        xPaths.Any(xpath => driver.FindByXPathInCurrentFrame(xpath) != null);
    public static bool DoesAtLeastOneOfTheElementsExist(this IWebDriver driver, IEnumerable<string> xPaths) =>
        xPaths.Any(xpath => driver.FindByXPathInCurrentFrame(xpath) != null);

    public static bool DoesAtLeastOneOfTheElementsExistInAllFrames(this IWebDriver driver, IEnumerable<string> xPaths)
    {
        return xPaths.Any(xpath => driver.FindByXPathAllFrames(xpath) != null);
    }
}