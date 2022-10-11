using Selenium.AntiCaptcha.enums;
using Selenium.AntiCaptcha.solvers;
using AntiCaptchaApi.Net.Models.Solutions;

namespace Selenium.AntiCaptcha
{
    internal static class SolverFactory
    {
        internal static Solver<TSolution>? GetSolver<TSolution>(CaptchaType captchaType)
            where TSolution : BaseSolution, new()
        {
            Solver<TSolution>? solver;
            
            switch (captchaType)
            {
                case CaptchaType.ReCaptchaV2:
                    solver = new ReCaptchaV2Solver() as Solver<TSolution>;
                    break;
                case CaptchaType.HCaptchaProxyless:
                    solver = new HCaptchaProxylessSolver() as Solver<TSolution>;
                    break;
                case CaptchaType.HCaptcha:
                    solver = new HCaptchaSolver() as Solver<TSolution>;
                    break;
                case CaptchaType.FunCaptcha:
                    solver = new FunCaptchaSolver() as Solver<TSolution>;
                    break;
                case CaptchaType.GeeTest:
                    solver = new GeeTestV3Solver() as Solver<TSolution>;
                    break;
                case CaptchaType.ImageToText:
                    solver = new ImageToTextSolver() as Solver<TSolution>;
                    break;
                default:
                    throw new Exception("Not supported captchaType");
            }

            if (solver == null)
            {
                throw new Exception("Not supported solver.");
            }
            
            return solver;
        }
    }
}
