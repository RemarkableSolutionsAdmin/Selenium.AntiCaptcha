using AntiCaptchaApi.Net.Models.Solutions;
using Selenium.AntiCaptcha.Enums;
using Selenium.AntiCaptcha.Internal.Extensions;
using Selenium.AntiCaptcha.Internal.Helpers;
using Selenium.AntiCaptcha.Solvers;
using Selenium.AntiCaptcha.Solvers.Base;

namespace Selenium.AntiCaptcha;

internal static class SolverFactory
{
    internal static ISolver<TSolution> GetSolver<TSolution>(CaptchaType captchaType)
        where TSolution : BaseSolution, new()
    {
        var solutionType = captchaType.GetSolutionType();

        if (typeof(TSolution) != solutionType)
        {
            throw new ArgumentException("Wrong solution type chosen to captcha type.");
        }

        return GetSolver(captchaType) as ISolver<TSolution>;

        throw new ArgumentException();
    }
    internal static ISolver GetSolver(CaptchaType captchaType)
    {
        switch (captchaType)
        {
            case CaptchaType.ReCaptchaV2:
                return new ReCaptchaV2Solver();
            case CaptchaType.ReCaptchaV2Proxyless:
                return new ReCaptchaV2ProxylessSolver();
            case CaptchaType.ReCaptchaV2Enterprise:
                return new ReCaptchaV2EnterpriseSolver();
            case CaptchaType.ReCaptchaV2EnterpriseProxyless:
                return new ReCaptchaV2EnterpriseProxylessSolver();
            case CaptchaType.ReCaptchaV3Proxyless:
                return new ReCaptchaV3ProxylessSolver();
            case CaptchaType.ReCaptchaV3Enterprise:
                return new ReCaptchaV3EnterpriseSolver();
            case CaptchaType.HCaptcha:
                return new HCaptchaSolver();
            case CaptchaType.HCaptchaProxyless:
                return new HCaptchaProxylessSolver();
            case CaptchaType.FunCaptcha:
                return new FunCaptchaSolver();
            case CaptchaType.FunCaptchaProxyless:
                return new FunCaptchaProxylessSolver();
            case CaptchaType.ImageToText:
                return new ImageToTextSolver();
            case CaptchaType.GeeTestV3:
                return new GeeTestV3Solver();
            case CaptchaType.GeeTestV3Proxyless:
                return new GeeTestV3ProxylessSolver();
            case CaptchaType.GeeTestV4:
                return new GeeTestV4Solver();
            case CaptchaType.GeeTestV4Proxyless:
                return new GeeTestV4ProxylessSolver();
            case CaptchaType.AntiGate:
                return new AntiGateSolver();
            default:
                throw new ArgumentOutOfRangeException(nameof(captchaType), captchaType, null);
        }
    }
}