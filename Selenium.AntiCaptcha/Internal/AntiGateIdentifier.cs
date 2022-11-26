using AntiCaptchaApi.Net.Models;
using AntiCaptchaApi.Net.Models.Solutions;
using OpenQA.Selenium;
using Selenium.AntiCaptcha.Enums;
using Selenium.AntiCaptcha.Models;

namespace Selenium.AntiCaptcha.Internal;

public class AntiGateIdentifier : ProxyCaptchaIdentifier
{    
    public AntiGateIdentifier()
    {
        IdentifiableTypes.Add(CaptchaType.AntiGate);
    }


    public override CaptchaType? Identify(IWebDriver driver, SolverAdditionalArguments? additionalArguments)
    {
        return null; //todo!
    }

    //TODO!
    public override CaptchaType? SpecifyCaptcha(CaptchaType originalType, IWebDriver driver, SolverAdditionalArguments additionalArguments)
    {
        return base.SpecifyCaptcha(originalType, driver, additionalArguments);
    }
}