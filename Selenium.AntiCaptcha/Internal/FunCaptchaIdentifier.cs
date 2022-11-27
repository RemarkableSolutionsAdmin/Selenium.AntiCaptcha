using System.Text.RegularExpressions;
using AntiCaptchaApi.Net.Models;
using AntiCaptchaApi.Net.Models.Solutions;
using OpenQA.Selenium;
using Selenium.AntiCaptcha.Enums;
using Selenium.AntiCaptcha.Internal.Extensions;
using Selenium.AntiCaptcha.Internal.Helpers;
using Selenium.AntiCaptcha.Models;

namespace Selenium.AntiCaptcha.Internal;

internal class FunCaptchaIdentifier : ProxyCaptchaIdentifier
{

    private readonly List<CaptchaType> _funcaptchaTypes = new()
    {
        CaptchaType.FunCaptcha, CaptchaType.FunCaptchaProxyless
    };

    public override async Task<CaptchaType?> IdentifyAsync(IWebDriver driver, SolverAdditionalArguments additionalArguments,
        CancellationToken cancellationToken)
    {
        try
        {
            if(IsFunCaptcha(driver))
                return await base.SpecifyCaptcha(CaptchaType.FunCaptchaProxyless, driver, additionalArguments, cancellationToken);
            
            return null;
        }
        catch
        {
            return null;
        }
        finally
        {
            driver.SwitchTo().DefaultContent();
        }
    }

    private bool IsFunCaptcha(IWebDriver driver)
        => IsThereFunCaptchaFunCaptchaScriptInAnyIFrames(driver) || IsThereAnElementWithPkey(driver);


    private bool IsThereAnElementWithPkey(IWebDriver driver)
    {        
        try
        {
            if (!string.IsNullOrEmpty(PageSourceSearcher.FindFunCaptchaSiteKey(driver)))
            {
                return true;
            }
            
            var frames = driver.FindManyByXPathCurrentFrame("//iframe");

            foreach (var frame in frames)
            {
                driver.SwitchTo().Frame(frame);
                if (IsThereFunCaptchaFunCaptchaScriptInAnyIFrames(driver))
                {
                    return true;
                }
            }

            return driver.FindByXPath("//script[contains(@src, 'funcaptcha'") != null;
        }
        catch (Exception)
        {
            return false;
        }
        finally
        {
            driver.SwitchTo().DefaultContent();
        }
    }

    private static bool IsThereFunCaptchaFunCaptchaScriptInAnyIFrames(IWebDriver driver)
    {
        try
        {
            var frames = driver.FindManyByXPathCurrentFrame("//iframe");

            foreach (var frame in frames)
            {
                driver.SwitchTo().Frame(frame);
                if (IsThereFunCaptchaFunCaptchaScriptInAnyIFrames(driver))
                {
                    return true;
                }
            }

            return driver.FindByXPath("//script[contains(@src, 'funcaptcha'") != null;
        }
        catch (Exception)
        {
            return false;
        }
        finally
        {
            driver.SwitchTo().DefaultContent();
        }
    }
    
    

    public FunCaptchaIdentifier()
    {
        IdentifiableTypes.AddRange(_funcaptchaTypes);
    }
}