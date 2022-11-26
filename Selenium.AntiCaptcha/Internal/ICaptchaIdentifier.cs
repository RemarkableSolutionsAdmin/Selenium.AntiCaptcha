using AntiCaptchaApi.Net.Models;
using AntiCaptchaApi.Net.Models.Solutions;
using OpenQA.Selenium;
using Selenium.AntiCaptcha.Enums;
using Selenium.AntiCaptcha.Models;

namespace Selenium.AntiCaptcha.Internal;

public interface ICaptchaIdentifier
{
    public  bool CanIdentifyAsync(CaptchaType type);
    public Task<CaptchaType?> IdentifyAsync(IWebDriver driver, SolverAdditionalArguments additionalArguments);
    public Task<CaptchaType?> SpecifyCaptcha(CaptchaType originalType, IWebDriver driver, SolverAdditionalArguments additionalArguments);
}