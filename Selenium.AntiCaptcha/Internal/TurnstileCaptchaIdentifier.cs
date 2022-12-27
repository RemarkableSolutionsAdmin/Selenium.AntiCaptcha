using System.Text.RegularExpressions;
using OpenQA.Selenium;
using Selenium.AntiCaptcha.Enums;
using Selenium.AntiCaptcha.Internal.Extensions;
using Selenium.AntiCaptcha.Models;

namespace Selenium.AntiCaptcha.Internal;

internal class TurnstileCaptchaIdentifier  : ProxyCaptchaIdentifier
{
    private readonly List<CaptchaType> _turnstileTypes = new()
    {
        CaptchaType.Turnstile,
        CaptchaType.TurnstileProxyless,
    };
    
    public TurnstileCaptchaIdentifier()
    {
        IdentifiableTypes.AddRange(_turnstileTypes);
    }

    public override async Task<CaptchaType?> IdentifyInCurrentFrameAsync(
        IWebDriver driver,
        SolverArguments arguments,
        CancellationToken cancellationToken)
    {
        try
        {
            var turnstileFrame = GetTurnstileIFrame(driver);

            if (turnstileFrame == null)
            {
                return null;
            }

            return await base.SpecifyCaptcha(CaptchaType.TurnstileProxyless, driver, arguments, cancellationToken);
        }
        catch (Exception)
        {
            return null;
        }
    }
    

    public override Task<CaptchaType?> SpecifyCaptcha(CaptchaType originalType, IWebDriver driver,
        SolverArguments arguments, CancellationToken cancellationToken)
    {
        return IdentifyInCurrentFrameAsync(driver, arguments, cancellationToken);
    }

    private static IWebElement? GetTurnstileIFrame(IWebDriver driver)
    {
        return driver.FindByXPathInCurrentFrame("//iframe[contains(@src, 'turnstile')]");
    }

}