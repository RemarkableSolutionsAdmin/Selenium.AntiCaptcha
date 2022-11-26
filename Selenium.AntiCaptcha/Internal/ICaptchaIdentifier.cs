using AntiCaptchaApi.Net.Models;
using AntiCaptchaApi.Net.Models.Solutions;
using OpenQA.Selenium;
using Selenium.AntiCaptcha.Enums;
using Selenium.AntiCaptcha.Models;

namespace Selenium.AntiCaptcha.Internal;

public interface ICaptchaIdentifier
{
    public  bool CanIdentify(CaptchaType type);
    public CaptchaType? Identify(IWebDriver driver, SolverAdditionalArguments additionalArguments);
    public CaptchaType? SpecifyCaptcha(CaptchaType originalType, IWebDriver driver, SolverAdditionalArguments additionalArguments);
}