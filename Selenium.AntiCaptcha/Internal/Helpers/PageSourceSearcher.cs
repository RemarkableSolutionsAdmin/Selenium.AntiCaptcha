using System.Text.RegularExpressions;
using OpenQA.Selenium;
using Selenium.AntiCaptcha.Internal.Extensions;

namespace Selenium.AntiCaptcha.Internal.Helpers;

public static class PageSourceSearcher
{
    private const string SiteKeyRegexPattern = @"(\w{8}-\w{4}-\w{4}-\w{4}-\w{12})";
    private const string FunCaptchaRegexSiteKey1 = $"funcaptcha+.{{0,100}}{SiteKeyRegexPattern}";
    private const string FunCaptchaRegexSiteKey2 = $"{SiteKeyRegexPattern}.{{0,100}}funcaptcha";
    public static string FindFunCaptchaSiteKey(IWebDriver driver)
    {       
        try
        {
            return driver.FindElement(By.Id("funcaptcha")).GetAttribute("data-pkey");
        }
        catch (Exception)
        {
            // ignore
        }

        var pageSource = driver.GetAllPageSource();
        var match = pageSource.GetFirstRegexThatFits(false, FunCaptchaRegexSiteKey1, FunCaptchaRegexSiteKey2);

        if (match == null || match.Groups.Count == 0)
            return string.Empty;

        return match.Groups[1].Value;
    }

    public static string FindSiteKey(IWebDriver driver)
    {
        var pageSource = driver.GetAllPageSource();
        var match = pageSource.GetFirstRegexThatFits(false, SiteKeyRegexPattern);
        return match != null ? match.Groups[1].Value : string.Empty;
    }

    public static string FindSingleImageSourceForImageToText(IWebDriver driver)
    {
        var pageSource = driver.GetAllPageSource();

        var doesContainIFrames = pageSource.DoesContainRegex(@"<\s*iframe.*<\/iframe>");

        if (doesContainIFrames)
            return string.Empty;
        
        
        var imageSources = pageSource.GetFirstRegexThatFits(RegexOptions.Multiline | RegexOptions.IgnoreCase, "^.*?<\\s*?img.*src\\s*=\\s*\"(.+?)\".*?$");

        if (!imageSources.Any(x => x.Success))
        {
            return string.Empty;
        }


        var possibleCaptchaImageSources = new List<string>();
            
        foreach (Match match in imageSources)
        {
            if(match.Groups.Count != 2 || !match.Groups[1].Success) 
                continue;
            var sourceValue = Regex.Unescape((System.Net.WebUtility.HtmlDecode(match.Groups[1].Value)));
            
            var idMatch = sourceValue.GetFirstRegexThatFits(true, @".*?=(\d{1,20})\D*?");

            if (sourceValue.ToLower().Contains("captcha") && idMatch is not null && idMatch.Success)
            {
                possibleCaptchaImageSources.Add(sourceValue);
            }

        }
        
        return possibleCaptchaImageSources.Count == 1 ? possibleCaptchaImageSources[0] : string.Empty;
    }
}