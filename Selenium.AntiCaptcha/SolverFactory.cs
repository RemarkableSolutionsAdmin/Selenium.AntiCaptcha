using Selenium.AntiCaptcha.enums;
using Selenium.AntiCaptcha.solvers;
using System;

namespace Selenium.AntiCaptcha
{
    internal static class SolverFactory
    {
        internal static Solver GetSolver(CaptchaType captchaType)
        {
            switch (captchaType)
            {
                case CaptchaType.ReCaptchaV2:
                    return new ReCaptchaV2Solver();
                case CaptchaType.HCaptcha:
                    return new HCaptchaSolver();
                case CaptchaType.FunCaptcha:
                    return new FunCaptchaSolver();
                case CaptchaType.GeeTest:
                    return new GeeTestSolver();
                case CaptchaType.ImageToText:
                    return new ImageToTextSolver();
                default:
                    throw new Exception("Not supported captchaType");
            }
        }
    }
}
