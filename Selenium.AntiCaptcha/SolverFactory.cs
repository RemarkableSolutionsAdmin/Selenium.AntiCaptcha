using AntiCaptchaApi.Net.Models.Solutions;
using OpenQA.Selenium;
using Selenium.AntiCaptcha.Enums;
using Selenium.AntiCaptcha.Internal.Extensions;
using Selenium.AntiCaptcha.Solvers;
using Selenium.AntiCaptcha.Solvers.Base;

namespace Selenium.AntiCaptcha;

internal static class SolverFactory
{
    internal static ISolver<TSolution> GetSolver<TSolution>(IWebDriver webDriver, string clientKey, CaptchaType captchaType)
        where TSolution : BaseSolution, new()
    {
        var solutionType = captchaType.GetSolutionType();

        if (typeof(TSolution) != solutionType)
        {
            throw new ArgumentException("Wrong solution type chosen to captcha type.");
        }

        return GetSolver(webDriver, clientKey, captchaType) as ISolver<TSolution>;
    }
    internal static ISolver GetSolver(IWebDriver webDriver, string clientKey, CaptchaType captchaType)
    {
        switch (captchaType)
        {
            case CaptchaType.ReCaptchaV2:
                return new ReCaptchaV2Solver(clientKey, webDriver);
            case CaptchaType.ReCaptchaV2Proxyless:
                return new ReCaptchaV2ProxylessSolver(clientKey, webDriver);
            case CaptchaType.ReCaptchaV2Enterprise:
                return new ReCaptchaV2EnterpriseSolver(clientKey, webDriver);
            case CaptchaType.ReCaptchaV2EnterpriseProxyless:
                return new ReCaptchaV2EnterpriseProxylessSolver(clientKey, webDriver);
            case CaptchaType.ReCaptchaV3:
                return new RecaptchaV3Solver(clientKey, webDriver);
            case CaptchaType.ReCaptchaV3Enterprise:
                return new ReCaptchaV3EnterpriseSolver(clientKey, webDriver);
            case CaptchaType.HCaptcha:
                return new HCaptchaSolver(clientKey, webDriver);
            case CaptchaType.HCaptchaProxyless:
                return new HCaptchaProxylessSolver(clientKey, webDriver);
            case CaptchaType.FunCaptcha:
                return new FunCaptchaSolver(clientKey, webDriver);
            case CaptchaType.FunCaptchaProxyless:
                return new FunCaptchaProxylessSolver(clientKey, webDriver);
            case CaptchaType.ImageToText:
                return new ImageToTextSolver(clientKey, webDriver);
            case CaptchaType.GeeTestV3:
                return new GeeTestV3Solver(clientKey, webDriver);
            case CaptchaType.GeeTestV3Proxyless:
                return new GeeTestV3ProxylessSolver(clientKey, webDriver);
            case CaptchaType.GeeTestV4:
                return new GeeTestV4Solver(clientKey, webDriver);
            case CaptchaType.GeeTestV4Proxyless:
                return new GeeTestV4ProxylessSolver(clientKey, webDriver);
            case CaptchaType.AntiGate:
                return new AntiGateSolver(clientKey, webDriver);
            case CaptchaType.Turnstile:
                return new TurnstileCaptchaSolver(clientKey, webDriver);
            case CaptchaType.TurnstileProxyless:
                return new TurnstileProxylessCaptchaSolver(clientKey, webDriver);
            default:
                throw new ArgumentOutOfRangeException(nameof(captchaType), captchaType, null);
        }
    }
}