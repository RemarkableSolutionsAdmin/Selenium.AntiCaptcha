using System.Text;
using OpenQA.Selenium;
using Selenium.FramesSearcher.Extensions.Exceptions;

namespace Selenium.FramesSearcher.Extensions;

public static class IWebDriverExtensions
{
    public static void ForEachFrame(this IWebDriver driver, ExtendedWebElement? currentFrame, Action<ExtendedWebElement?> action)
    {
        try
        {
            action.Invoke(currentFrame);
            var frames = driver.FindIFramesInFrame(currentFrame);
            foreach (var frame in frames)
            {
                if (!driver.TryToSwitchToFrame(frame))
                    continue;
                driver.ForEachFrame(frame, action);
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
        return driver.FindByXPathAllFrames(null, xPathPatterns);
    }

    private static IWebElement? FindByXPathAllFrames(this IWebDriver driver, ExtendedWebElement? parentElement, params string[] xPathPatterns)
    {
        if (!xPathPatterns.Any())
            return null;

        var result = driver.FindByXPathInCurrentFrame(xPathPatterns);
        
        if (result != null)
        {
            return result;
        }

        parentElement ??= driver.GetCurrentFrame();
        var childrenFrames = driver.FindIFramesInFrame(parentElement);
        foreach (var frame in childrenFrames)
        {
            if (driver.TryToSwitchToFrame(frame))
            {
                result = driver.FindByXPathAllFrames(parentElement, xPathPatterns);
                if (result != null)
                {
                    return result;
                }
            };
        }

        driver.SwitchTo().ParentFrame();
        return null;
    }

    public static IList<ExtendedWebElement> FindIFramesInFrame(this IWebDriver driver, ExtendedWebElement? currentFrame)
    {
        return driver.FindElements(By.TagName("iframe")).Select(x => new ExtendedWebElement(x, driver.GetAllElementAttributes(x), currentFrame)).ToList();
    }

    public static IEnumerable<ExtendedWebElement> FindManyByXPathAllFrames(this IWebDriver driver, params string[] xPathPatterns)
    {
        if (!xPathPatterns.Any())
            return new List<ExtendedWebElement>();
        var currentFrame = driver.GetCurrentFrame();
        var result = new List<ExtendedWebElement>();
        try
        {
            driver.ForEachFrame(currentFrame, frame => result.AddRange(driver.FindManyByXPathCurrentFrame(xPathPatterns).Select(x => new ExtendedWebElement(x, driver.GetAllElementAttributes(x), frame))));
            return result;
        }
        catch (Exception)
        {
            return new List<ExtendedWebElement>();
        }
        finally
        {
            driver.TryToSwitchToFrame(currentFrame);
        }
    }

    public static IEnumerable<string> FindManyValuesByXPathAllFrames(this IWebDriver driver, string attributeName, params string[] xPathPatterns)
    {
        if (!xPathPatterns.Any())
            return new List<string>();
        
        var currentFrame = driver.GetCurrentFrame();
        var result = new List<string>();
        try
        {
            driver.ForEachFrame(currentFrame,  _ => result.AddRange(driver.FindManyValuesByXPathCurrentFrame(attributeName, xPathPatterns).Select(x => x)));
            return result;
        }
        catch (Exception)
        {
            return new List<string>();
        }
        finally
        {
            driver.TryToSwitchToFrame(currentFrame);
        }
    }

    public static async Task SetValueForElementWithIdInAllFrames(this IWebDriver driver, string id, string value)
    {
        var currentFrame = driver.GetCurrentFrame();
        try
        {
            driver.ForEachFrame(currentFrame, frame => driver.SetValueForElementWithIdInCurrentFrame(id, value, frame));
        }
        catch (Exception)
        {
            
        }
        finally
        {
            driver.TryToSwitchToFrame(currentFrame);
        }
    }

    private static void SetValueForElementWithIdInCurrentFrame(this IWebDriver driver, string id, string value, ExtendedWebElement? frameWebElement)
    {
        try
        {
            var js = driver as IJavaScriptExecutor;
            var elements = driver.FindElements(By.Id(id));

            if (elements != null && elements.Any())
            {
                foreach (var element in elements)
                {
                    try
                    {
                        js!.ExecuteScript("arguments[0].setAttribute(arguments[1],arguments[2])", element, "value", value);
                        js!.ExecuteScript($"document.getElementById('{id}').value = '{value}'");
                    }
                    catch (Exception e)
                    {
                        // ignore
                    }
                }
            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
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
    public static List<string> FindManyValuesByXPathCurrentFrame(this IWebDriver driver, string attributeName, params string[] xPathPatterns)
    {
        var results = new List<string>();
        foreach (var xPathPattern in xPathPatterns)
        {
            try
            {
                results.AddRange(driver.FindElements(By.XPath(xPathPattern)).Select(x => x.GetAttribute(attributeName)).ToList());
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
        var result = GatherAllPageSourcesInFrames(driver, driver.GetCurrentFrame());
        driver.TryToSwitchToFrame(currentFrame);
        return result;
    }
    

    public static ExtendedWebElement? GetCurrentRootWebElement(this IWebDriver driver)
    {
        var inputWebElements = driver.FindElements(By.TagName("html"));
        if (inputWebElements.Count != 1)
        {
            throw new MultipleHtmlRootElementFoundWhileTraversingSiteException();
        }

        var attributes = driver.GetAllElementAttributes(inputWebElements.Single());
        return new ExtendedWebElement(inputWebElements.Single(), attributes);
    }
    
    
    public static Dictionary<string, object>  GetAllElementAttributes(this IWebDriver driver, IWebElement element)
    {
        try
        {
            var ex = driver as IJavaScriptExecutor;
            return (Dictionary<string, object>)
                ex.ExecuteScript("var items = { }; for (index = 0; index < arguments[0].attributes.length; ++index) { items[arguments[0].attributes[index].name] = arguments[0].attributes[index].value }; return items;",
                    element is ExtendedWebElement extendedWebElement ? extendedWebElement.WebElement : element);
        }
        catch (Exception e)
        {
            return new Dictionary<string, object>();
        }
    }


    //Must start from root.
    internal static TreeNode<ExtendedWebElement>? GetFramesTree(this IWebDriver driver)
    {
        var currentFrame = driver.GetCurrentFrame();

        if (currentFrame == null)
            return null;
        
        var result = new TreeNode<ExtendedWebElement>(currentFrame);
        var iframes = driver.FindIFramesInFrame(currentFrame);

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
            return driver.GetFramesTree() ?? throw new CouldNotBuildFramesTree();
        }
        catch (Exception exception)
        {
            throw new CouldNotBuildFramesTree(exception);
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
                throw new CouldNotTraverseToFrameException();
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
        var inputWebElement = driver.GetCurrentRootWebElement();
        var inputChildrenFrames = driver.FindIFramesInFrame(inputWebElement);
        
        if (inputWebElement == null)
        {
            return null;
        }

        driver.SwitchTo().ParentFrame();
        
        var childrenFrames = driver.FindIFramesInFrame(inputWebElement);

        if (childrenFrames.All(frame => inputChildrenFrames.Any(frame2 => frame2.Equals(frame))))
        {
            return inputWebElement;
        }
        
        foreach (var frame in childrenFrames)
        {
            if (driver.TryToSwitchToFrame(frame))
            {
                var currentRootWebElement = driver.GetCurrentRootWebElement();
                if (Equals(currentRootWebElement, inputWebElement))
                {
                    return new ExtendedWebElement(frame.WebElement, driver.GetAllElementAttributes(frame), frame);
                }
            }
        }

        return null;
    }
    

    private static string GatherAllPageSourcesInFrames(IWebDriver driver, ExtendedWebElement? extendedWebElement)
    {
        var builder = new StringBuilder();
        builder.Append(driver.PageSource);
        
        var childrenFrames = driver.FindIFramesInFrame(extendedWebElement);
        
        foreach (var iframe in childrenFrames)
        {
            if (driver.TryToSwitchToFrame(iframe))
            {
                builder.Append(GatherAllPageSourcesInFrames(driver, iframe));
                driver.SwitchTo().ParentFrame();
            }
        }
        
        return builder.ToString();
    }
    
    
    public static bool DoesAtLeastOneOfTheElementsExist(this IWebDriver driver, params string[] xPaths) =>
        xPaths.Any(xpath => driver.FindByXPathInCurrentFrame(xpath) != null);
    public static bool DoesAtLeastOneOfTheElementsExist(this IWebDriver driver, IEnumerable<string> xPaths) =>
        xPaths.Any(xpath => driver.FindByXPathInCurrentFrame(xpath) != null);

}