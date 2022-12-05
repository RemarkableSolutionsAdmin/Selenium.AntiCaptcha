using AntiCaptchaApi.Net.Models;
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
        //var imageElements = driver.FindManyByXPathAllFrames("//img");
        return false; // TODO!
    }
    
    
}