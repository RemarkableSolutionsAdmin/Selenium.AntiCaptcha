using System.Text;

namespace Selenium.AntiCaptcha.Internal.Helpers;

public static class XPathBuilder
{
    public record XPath
    {
        private readonly StringBuilder _stringBuilder = new();

        public XPath SelectAllObjectsOfType(string typeName)
        {
            _stringBuilder.Append($"//{typeName}");
            return this;
        }

        public XPath FilterAttributeEqual(string attributeName, string value)
        {
            _stringBuilder.Append($"[@{attributeName}='{value}']");
            return this;
        }


        public XPath FilterAttributeContains(string attributeName, string value)
        {
            _stringBuilder.Append($"[contains(@{attributeName}, '{value}')]");
            return this;
        }

        public string Build()
        {
            return _stringBuilder.ToString();
        }
    }
}