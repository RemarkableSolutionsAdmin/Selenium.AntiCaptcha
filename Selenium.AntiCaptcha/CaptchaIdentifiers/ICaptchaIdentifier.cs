using OpenQA.Selenium;
using Selenium.AntiCaptcha.Enums;
using Selenium.AntiCaptcha.Models;

namespace Selenium.AntiCaptcha.CaptchaIdentifiers;

public interface ICaptchaIdentifier
{
    public bool CanIdentify(CaptchaType type);
    public Task<CaptchaType?> IdentifyInCurrentFrameAsync(IWebDriver driver, SolverArguments arguments,
        CancellationToken cancellationToken);
    public Task<CaptchaType?> IdentifyInAllFramesAsync(IWebDriver driver, SolverArguments arguments,
        CancellationToken cancellationToken);
    public Task<CaptchaType?> SpecifyCaptcha(CaptchaType originalType, IWebDriver driver, SolverArguments arguments, CancellationToken cancellationToken);
}