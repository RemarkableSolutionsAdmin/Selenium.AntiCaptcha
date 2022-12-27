using System.Text.RegularExpressions;
using OpenQA.Selenium;
using Selenium.AntiCaptcha.Enums;
using Selenium.AntiCaptcha.Internal.Extensions;
using Selenium.AntiCaptcha.Internal.Helpers;
using Selenium.AntiCaptcha.Models;

namespace Selenium.AntiCaptcha.Internal;

public class ImageToTextIdentifier : ProxyCaptchaIdentifier
{
    public ImageToTextIdentifier()
    {
        IdentifiableTypes.Add(CaptchaType.ImageToText);
    }

    public override async Task<CaptchaType?> SpecifyCaptcha(CaptchaType originalType, IWebDriver driver,
        SolverArguments arguments, CancellationToken cancellationToken)
    {
        return originalType;
    }

    public override async Task<CaptchaType?> IdentifyInCurrentFrameAsync(IWebDriver driver, SolverArguments arguments,
        CancellationToken cancellationToken)
    {
        var base64 = arguments.ImageElement?.DownloadSourceAsBase64String();
        
        if (!string.IsNullOrEmpty(base64))
        {
            return CaptchaType.ImageToText;
        }

        if (!string.IsNullOrEmpty(arguments.ImageFilePath))
        {
            return CaptchaType.ImageToText;
        }

        return DoesCaptchaImageElementExists(driver) ? CaptchaType.ImageToText : null;
    }

    private static bool DoesCaptchaImageElementExists(IWebDriver driver)
    {
        var possibleCaptchaImageSources = PageSourceSearcher.FindSingleImageSourceForImageToText(driver);
        return !string.IsNullOrEmpty(possibleCaptchaImageSources);

    }
    
    
}