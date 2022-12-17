using System.Text.RegularExpressions;
using OpenQA.Selenium;
using Selenium.AntiCaptcha.Enums;
using Selenium.AntiCaptcha.Internal.Extensions;
using Selenium.AntiCaptcha.Models;

namespace Selenium.AntiCaptcha.Internal;

public class ImageToTextIdentifier : ProxyCaptchaIdentifier
{
    public ImageToTextIdentifier()
    {
        IdentifiableTypes.Add(CaptchaType.ImageToText);
    }

    public override async Task<CaptchaType?> SpecifyCaptcha(CaptchaType originalType, IWebDriver driver,
        SolverAdditionalArguments additionalArguments, CancellationToken cancellationToken)
    {
        return originalType;
    }

    public override async Task<CaptchaType?> IdentifyInCurrentFrameAsync(IWebDriver driver, SolverAdditionalArguments additionalArguments,
        CancellationToken cancellationToken)
    {
        var base64 = additionalArguments.ImageElement?.DownloadSourceAsBase64String();
        
        if (!string.IsNullOrEmpty(base64))
        {
            return CaptchaType.ImageToText;
        }

        if (!string.IsNullOrEmpty(additionalArguments.ImageFilePath))
        {
            return CaptchaType.ImageToText;
        }

        return DoesCaptchaImageElementExists(driver) ? CaptchaType.ImageToText : null;
    }

    private static bool DoesCaptchaImageElementExists(IWebDriver driver)
    {
        var pageSource = driver.GetAllPageSource();

        var doesContainIFrames = pageSource.DoesContainRegex(@"<\s*iframe.*<\/iframe>");

        if (doesContainIFrames)
            return false;
        
        
        var imageSources = pageSource.GetFirstRegexThatFits(RegexOptions.Multiline | RegexOptions.IgnoreCase, "^.*?<\\s*?img.*src\\s*=\\s*\"(.+?)\".*?$");

        if (!imageSources.Any(x => x.Success))
        {
            return false;
        }


        var possibleCaptchaImagesCounter = 0;
            
        foreach (Match match in imageSources)
        {
            if(match.Groups.Count != 2 || !match.Groups[1].Success) 
                continue;
                
            var sourceValue = match.Groups[1].Value.ToLower();
            var idMatch = sourceValue.GetFirstRegexThatFits(true, @".*?=(\d{1,20})\D*?");

            if (sourceValue.Contains("captcha") && idMatch is not null && idMatch.Success)
            {
                possibleCaptchaImagesCounter++;
            }

        }


        return possibleCaptchaImagesCounter == 1;

    }
    
    
}