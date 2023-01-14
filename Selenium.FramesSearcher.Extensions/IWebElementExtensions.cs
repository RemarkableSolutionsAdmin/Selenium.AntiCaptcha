﻿using System.Net;
using OpenQA.Selenium;

namespace Selenium.FramesSearcher.Extensions;

public static class IWebElementExtensions
{
    public static string DownloadSourceAsBase64String(this IWebElement element)
    {
        try
        {
            var elementSrc = element.GetAttribute("src");
            byte[]? file;
            using (WebClient webClient = new())
            {
                file = webClient.DownloadData(elementSrc);
            }

            return Convert.ToBase64String(file);
        }
        catch (Exception)
        {
            return string.Empty;
        }
    }
}