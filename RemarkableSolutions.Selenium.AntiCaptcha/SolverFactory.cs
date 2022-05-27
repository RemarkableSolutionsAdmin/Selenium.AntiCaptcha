using RemarkableSolutions.Selenium.AntiCaptcha.enums;
using RemarkableSolutions.Selenium.AntiCaptcha.solvers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RemarkableSolutions.Selenium.AntiCaptcha
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
                default:
                    throw new Exception("Not supported captchaType");
            }
        }
    }
}
