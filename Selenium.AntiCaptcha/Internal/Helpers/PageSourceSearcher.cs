using System.Text.RegularExpressions;
using OpenQA.Selenium;
using Selenium.AntiCaptcha.Internal.Extensions;

namespace Selenium.AntiCaptcha.Internal.Helpers;

public static class PageSourceSearcher
{
    private const string SiteKeyRegexPattern = @"(\w{8}-\w{4}-\w{4}-\w{4}-\w{12})";
    private const string FunCaptchaRegexSiteKey1 = $"(funcaptcha)+.{{0,100}}{SiteKeyRegexPattern}";
    private const string FunCaptchaRegexSiteKey2 = $"{SiteKeyRegexPattern}.{{0,100}}(funcaptcha)+";
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

        if (match == null || match.Groups.Count < 2)
            return string.Empty;

        return match.Groups[2].Value;
    }

    public static string FindSiteKey(IWebDriver driver)
    {
        var pageSource = driver.GetAllPageSource();
        var match = pageSource.GetFirstRegexThatFits(false, SiteKeyRegexPattern);
        return match != null ? match.Groups[1].Value : string.Empty;
    }
}