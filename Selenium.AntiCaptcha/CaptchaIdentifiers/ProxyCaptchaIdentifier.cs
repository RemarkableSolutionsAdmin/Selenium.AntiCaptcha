using AntiCaptchaApi.Net.Models;
using OpenQA.Selenium;
using Selenium.AntiCaptcha.Enums;
using Selenium.AntiCaptcha.Internal.Extensions;
using Selenium.AntiCaptcha.Models;

namespace Selenium.AntiCaptcha.CaptchaIdentifiers;

public abstract class ProxyCaptchaIdentifier : ICaptchaIdentifier
{
    protected readonly List<CaptchaType> IdentifiableTypes = new();
    
    public bool CanIdentify(CaptchaType type)
    {
        return IdentifiableTypes.Contains(type);
    }

    public abstract Task<CaptchaType?> IdentifyInCurrentFrameAsync(IWebDriver driver, SolverArguments arguments,
        CancellationToken cancellationToken);

    public async Task<CaptchaType?> IdentifyInAllFramesAsync(IWebDriver driver, SolverArguments arguments,
        CancellationToken cancellationToken)
    {        
        var currentFrame = driver.GetCurrentFrame();
        try
        {
            return await IdentifyInCurrentFrameAsync(driver, arguments, cancellationToken);
        }
        catch
        {
            return null;
        }
        finally
        {
            driver.TryToSwitchToFrame(currentFrame);
        }
    }


    public virtual async Task<CaptchaType?> SpecifyCaptcha(CaptchaType originalType, IWebDriver driver,
        SolverArguments arguments, CancellationToken cancellationToken)
    {
        if (arguments.ProxyConfig == null || string.IsNullOrEmpty(arguments.ProxyConfig.ProxyAddress))
        {
            return originalType;
        }

        return originalType.ToProxyType();
    }

}