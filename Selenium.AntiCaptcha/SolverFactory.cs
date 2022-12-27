using AntiCaptchaApi.Net.Models.Solutions;
using AntiCaptchaApi.Net.Requests.Abstractions;
using OpenQA.Selenium;
using Selenium.AntiCaptcha.Enums;
using Selenium.AntiCaptcha.Internal.Extensions;
using Selenium.AntiCaptcha.Models;
using Selenium.AntiCaptcha.Solvers;
using Selenium.AntiCaptcha.Solvers.Base;

namespace Selenium.AntiCaptcha;

internal static class SolverFactory
{
    public static ISolver<CaptchaRequest<TSolution>, TSolution> GetSolver<TSolution>(IWebDriver webDriver, string clientKey, CaptchaType captchaType, SolverConfig solverConfig)
        where TSolution : BaseSolution, new()
    {
        var solutionType = captchaType.GetSolutionType();

        if (typeof(TSolution) != solutionType)
        {
            throw new ArgumentException("Wrong solution type chosen to captcha type.");
        }

        return GetSolver(webDriver, clientKey, captchaType, solverConfig) as ISolver<CaptchaRequest<TSolution>, TSolution>;
    }
    internal static ISolver GetSolver(IWebDriver webDriver, string clientKey, CaptchaType captchaType, SolverConfig solverConfig)
    {
        switch (captchaType)
        {
            case CaptchaType.ReCaptchaV2:
                return new ReCaptchaV2Solver(clientKey, webDriver, solverConfig);
            case CaptchaType.ReCaptchaV2Proxyless:
                return new ReCaptchaV2ProxylessSolver(clientKey, webDriver, solverConfig);
            case CaptchaType.ReCaptchaV2Enterprise:
                return new ReCaptchaV2EnterpriseSolver(clientKey, webDriver, solverConfig);
            case CaptchaType.ReCaptchaV2EnterpriseProxyless:
                return new ReCaptchaV2EnterpriseProxylessSolver(clientKey, webDriver, solverConfig);
            case CaptchaType.ReCaptchaV3:
                return new RecaptchaV3Solver(clientKey, webDriver, solverConfig);
            case CaptchaType.ReCaptchaV3Enterprise:
                return new ReCaptchaV3EnterpriseSolver(clientKey, webDriver, solverConfig);
            case CaptchaType.HCaptcha:
                return new HCaptchaSolver(clientKey, webDriver, solverConfig);
            case CaptchaType.HCaptchaProxyless:
                return new HCaptchaProxylessSolver(clientKey, webDriver, solverConfig);
            case CaptchaType.FunCaptcha:
                return new FunCaptchaSolver(clientKey, webDriver, solverConfig);
            case CaptchaType.FunCaptchaProxyless:
                return new FunCaptchaProxylessSolver(clientKey, webDriver, solverConfig);
            case CaptchaType.ImageToText:
                return new ImageToTextSolver(clientKey, webDriver, solverConfig);
            case CaptchaType.GeeTestV3:
                return new GeeTestV3Solver(clientKey, webDriver, solverConfig);
            case CaptchaType.GeeTestV3Proxyless:
                return new GeeTestV3ProxylessSolver(clientKey, webDriver, solverConfig);
            case CaptchaType.GeeTestV4:
                return new GeeTestV4Solver(clientKey, webDriver, solverConfig);
            case CaptchaType.GeeTestV4Proxyless:
                return new GeeTestV4ProxylessSolver(clientKey, webDriver, solverConfig);
            case CaptchaType.AntiGate:
                return new AntiGateSolver(clientKey, webDriver, solverConfig);
            case CaptchaType.Turnstile:
                return new TurnstileCaptchaSolver(clientKey, webDriver, solverConfig);
            case CaptchaType.TurnstileProxyless:
                return new TurnstileProxylessCaptchaSolver(clientKey, webDriver, solverConfig);
            default:
                throw new ArgumentOutOfRangeException(nameof(captchaType), captchaType, null);
        }
    }
}