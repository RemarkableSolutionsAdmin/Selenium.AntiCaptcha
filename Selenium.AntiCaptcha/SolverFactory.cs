using Selenium.AntiCaptcha.enums;
using Selenium.AntiCaptcha.solvers;
using AntiCaptchaApi.Net.Models.Solutions;
using AntiCaptchaApi.Net.Requests;
using AntiCaptchaApi.Net.Requests.Abstractions;

namespace Selenium.AntiCaptcha
{
    internal static class SolverFactory
    {
        internal static Solver<TRequest, TSolution>? GetSolver<TRequest, TSolution>(CaptchaType captchaType)
            where TRequest : CaptchaRequest
            where TSolution: BaseSolution, new()
        {
            Solver<TRequest, TSolution>? solver;
            
            switch (captchaType)
            {
                case CaptchaType.ReCaptchaV2:
                    solver = new ReCaptchaV2Solver() as Solver<TRequest,TSolution>;
                    break;
                case CaptchaType.HCaptchaProxyless:
                    solver = new HCaptchaProxylessSolver() as Solver<TRequest, TSolution>;
                    break;
                case CaptchaType.HCaptcha:
                    solver = new HCaptchaSolver() as Solver<TRequest, TSolution>;
                    break;
                case CaptchaType.FunCaptcha:
                    solver = new FunCaptchaSolver() as Solver<TRequest, TSolution>;
                    break;
                case CaptchaType.GeeTest:
                    solver = new GeeTestV3Solver() as Solver<TRequest, TSolution>;
                    break;
                case CaptchaType.ImageToText:
                    solver = new ImageToTextSolver() as Solver<TRequest, TSolution>;
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
