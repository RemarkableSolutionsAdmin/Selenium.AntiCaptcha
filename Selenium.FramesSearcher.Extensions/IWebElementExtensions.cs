using System.Net;
using System.Text.RegularExpressions;
using OpenQA.Selenium;

namespace Selenium.FramesSearcher.Extensions;

public static class IWebElementExtensions
{
    public static string DownloadSourceAsBase64String(this IWebElement element)
    {
        try
        {
            var elementSrc = element.GetAttribute("src");
            var isUrl = elementSrc.Contains("http");

            if (isUrl)
            {
                byte[]? file;
                using (WebClient webClient = new())
                {
                    file = webClient.DownloadData(elementSrc);
                }

                return Convert.ToBase64String(file);
            }

            var imageBase64Pattern = "(?:data:image/[^;]+;base64,)([^\"\n]+)";
            var match = new Regex(imageBase64Pattern).Match(elementSrc);
            return match.Success ? match.Groups[1].Value : string.Empty;
        }
        catch (Exception)
        {
            return string.Empty;
        }
    }
}