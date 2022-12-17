using OpenQA.Selenium;
using Selenium.AntiCaptcha.Enums;
using Selenium.AntiCaptcha.Models;

namespace Selenium.AntiCaptcha.Internal;

public interface ICaptchaIdentifier
{
    public bool CanIdentify(CaptchaType type);
    public Task<CaptchaType?> IdentifyInCurrentFrameAsync(IWebDriver driver, SolverAdditionalArguments additionalArguments,
        CancellationToken cancellationToken);
    public Task<CaptchaType?> IdentifyInAllFramesAsync(IWebDriver driver, SolverAdditionalArguments additionalArguments,
        CancellationToken cancellationToken);
    public Task<CaptchaType?> SpecifyCaptcha(CaptchaType originalType, IWebDriver driver, SolverAdditionalArguments additionalArguments, CancellationToken cancellationToken);
}