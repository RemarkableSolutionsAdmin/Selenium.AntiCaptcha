using System.Collections.ObjectModel;
using System.Drawing;
using OpenQA.Selenium;

namespace Selenium.FramesSearcher.Extensions;

public class ExtendedWebElement : IWebElement
{
    public IWebElement WebElement { get; private set; }
    public ExtendedWebElement? ParentFrameElement { get; private set; }
    public Dictionary<string, object> CachedAttributes { get; private set; }


    public ExtendedWebElement(IWebElement webElement, ExtendedWebElement? parentFrameElement, Dictionary<string, object> cachedAttributes)
    {
          WebElement = webElement;
          ParentFrameElement = parentFrameElement;
          CachedAttributes = cachedAttributes;
    }
    
    internal ExtendedWebElement(IWebElement webElement, Dictionary<string, object> cachedAttributes, ExtendedWebElement? parentFrameElement = null, bool isFrame = false) : this(webElement, parentFrameElement, cachedAttributes)
    {
          IsFrame = isFrame;
    }
      
    public bool IsRoot => ParentFrameElement == null;
    
    public bool IsFrame { get; private set; }
    public IWebElement FindElement(By by)
    {
         return WebElement.FindElement(by);
    }

    public ReadOnlyCollection<IWebElement> FindElements(By by)
    {
          return WebElement.FindElements(by);
    }

    public void Clear()
    {
          WebElement.Clear();
    }

    public void SendKeys(string text)
    { 
          WebElement.SendKeys(text);
    }

    public void Submit()
    { 
          WebElement.Submit();
    }

    public void Click()
    {
          WebElement.Click();
    }

    public string GetAttribute(string attributeName)
    {
          return WebElement.GetAttribute(attributeName);
    }

    public string GetDomAttribute(string attributeName)
    {
          return WebElement.GetDomAttribute(attributeName);
    }

    public string GetDomProperty(string propertyName)
    {
          return WebElement.GetDomProperty(propertyName);
    }

    public string GetCssValue(string propertyName)
    {
          return WebElement.GetCssValue(propertyName);
    }

    public ISearchContext GetShadowRoot()
    {
          return WebElement.GetShadowRoot();
    }

    public override bool Equals(object? obj)
    {
          if (obj is ExtendedWebElement extendedWebElement)
                return WebElement.Equals(extendedWebElement.WebElement);

          if (obj is IWebElement webElement)
                return WebElement.Equals(webElement);

          return false;
    }

    public override int GetHashCode()
    {
          return WebElement.GetHashCode();
    }

    public string TagName
    {
          get
          {
                try
                {
                      return WebElement.TagName;
                }
                catch (StaleElementReferenceException)
                {
                      return string.Empty;
                }
          }
    }

    public string Text
    {
          get
          {
                try
                {
                      return WebElement.Text;
                }
                catch (StaleElementReferenceException)
                {
                      return string.Empty;
                }
          }
    }
    public bool Enabled
    {
          get
          {
                try
                {
                      return WebElement.Enabled;
                }
                catch (StaleElementReferenceException)
                {
                      return false;
                }
          }
    }
    public bool Selected
    {
          get
          {
                try
                {
                      return WebElement.Selected;
                }
                catch (StaleElementReferenceException)
                {
                      return false;
                }
          }
    }
    public Point Location
    {
          get
          {
                try
                {
                      return WebElement.Location;
                }
                catch (StaleElementReferenceException)
                {
                      return default;
                }
          }
    }
    public Size Size
    {
          get
          {
                try
                {
                      return WebElement.Size;
                }
                catch (StaleElementReferenceException)
                {
                      return default;
                }
          }
    }
    public bool Displayed
    {
          get
          {
                try
                {
                      return WebElement.Displayed;
                }
                catch (Exception)
                {
                      return default;
                }
          }
    }
}