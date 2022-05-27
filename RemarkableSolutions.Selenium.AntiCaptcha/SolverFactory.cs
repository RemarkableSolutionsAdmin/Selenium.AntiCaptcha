using RemarkableSolutions.Selenium.AntiCaptcha.enums;
using RemarkableSolutions.Selenium.AntiCaptcha.solutions;
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
                default:
                    throw new Exception("Not supported captchaType");
            }
        }
    }
}
