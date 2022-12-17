using OpenQA.Selenium;
using Selenium.AntiCaptcha.Enums;
using Selenium.AntiCaptcha.Internal.Extensions;
using Selenium.AntiCaptcha.Models;

namespace Selenium.AntiCaptcha.Internal;

public abstract class ProxyCaptchaIdentifier : ICaptchaIdentifier
{
    protected readonly List<CaptchaType> IdentifiableTypes = new();
    
    public bool CanIdentify(CaptchaType type)
    {
        return IdentifiableTypes.Contains(type);
    }

    public abstract Task<CaptchaType?> IdentifyInCurrentFrameAsync(IWebDriver driver, SolverAdditionalArguments additionalArguments,
        CancellationToken cancellationToken);

    public async Task<CaptchaType?> IdentifyInAllFramesAsync(IWebDriver driver, SolverAdditionalArguments additionalArguments, CancellationToken cancellationToken)
    {        
        var currentFrame = driver.GetCurrentFrame();
        try
        {
            return await IdentifyInCurrentFrameAsync(driver, additionalArguments, cancellationToken);
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
        SolverAdditionalArguments additionalArguments, CancellationToken cancellationToken)
    {
        if (additionalArguments.ProxyConfig == null || string.IsNullOrEmpty(additionalArguments.ProxyConfig.ProxyAddress))
        {
            return originalType;
        }

        return originalType.ToProxyType();
    }

}