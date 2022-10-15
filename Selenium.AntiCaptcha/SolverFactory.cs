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
        
        switch (captchaType)
        {
            case CaptchaType.ReCaptchaV2:
                return new ReCaptchaV2Solver() as ISolver<TSolution>;
            case CaptchaType.ReCaptchaV2Proxyless:
                return new ReCaptchaV2ProxylessSolver() as ISolver<TSolution>;
            case CaptchaType.ReCaptchaV2Enterprise:
                return new ReCaptchaV2EnterpriseSolver() as ISolver<TSolution>;
            case CaptchaType.ReCaptchaV2EnterpriseProxyless:
                return new ReCaptchaV2EnterpriseProxylessSolver() as ISolver<TSolution>;
            case CaptchaType.ReCaptchaV3Proxyless:
                return new ReCaptchaV3ProxylessSolver() as ISolver<TSolution>;
            case CaptchaType.ReCaptchaV3Enterprise:
                return new ReCaptchaV3EnterpriseSolver() as ISolver<TSolution>;
            case CaptchaType.HCaptcha:
                return new HCaptchaSolver() as ISolver<TSolution>;
            case CaptchaType.HCaptchaProxyless:
                return new HCaptchaProxylessSolver() as ISolver<TSolution>;
            case CaptchaType.FunCaptcha:
                return new FunCaptchaSolver() as ISolver<TSolution>;
            case CaptchaType.FunCaptchaProxyless:
                return new FunCaptchaProxylessSolver() as ISolver<TSolution>;
            case CaptchaType.ImageToText:
                return new ImageToTextSolver() as ISolver<TSolution>;
            case CaptchaType.GeeTestV3:
                return new GeeTestV3Solver() as ISolver<TSolution>;
            case CaptchaType.GeeTestV3Proxyless:
                return new GeeTestV3ProxylessSolver() as ISolver<TSolution>;
            case CaptchaType.GeeTestV4:
                return new GeeTestV4Solver() as ISolver<TSolution>;
            case CaptchaType.GeeTestV4Proxyless:
                return new GeeTestV4ProxylessSolver() as ISolver<TSolution>;
            case CaptchaType.AntiGate:
                return new AntiGateSolver() as ISolver<TSolution>;
            default:
                throw new ArgumentOutOfRangeException(nameof(captchaType), captchaType, null);
        }

        throw new ArgumentException();
    }
}