using AntiCaptchaApi.Net.Models.Solutions;
using AntiCaptchaApi.Net.Requests.Abstractions;
using AntiCaptchaApi.Net.Responses;
using AntiCaptchaApi.Net.Responses.Abstractions;
using OpenQA.Selenium;
using Selenium.AntiCaptcha.Enums;
using Selenium.AntiCaptcha.Exceptions;
using Selenium.AntiCaptcha.Internal;
using Selenium.AntiCaptcha.Internal.Extensions;
using Selenium.AntiCaptcha.Models;
using Selenium.AntiCaptcha.Solvers.Base;

namespace Selenium.AntiCaptcha
{
    public static class IWebDriverExtensions
    {
        public static async Task<BaseResponse> SolveCaptchaAsync(this IWebDriver driver,
            string clientKey,
            SolverArguments? additionalArguments = null,
            ActionArguments? actionArguments = null,
            SolverConfig? solverConfig = null,
            CancellationToken cancellationToken = default)
        {
            additionalArguments ??= new SolverArguments();
            var captchaType = additionalArguments.CaptchaType ?? await IdentifyCaptcha(driver, additionalArguments, cancellationToken);
            dynamic solver = SolverFactory.GetSolver(driver, clientKey, captchaType, solverConfig ?? new DefaultSolverConfig());
            return await solver.SolveAsync(additionalArguments, actionArguments, cancellationToken);
        }

        private static async Task<CaptchaType> IdentifyCaptcha(IWebDriver driver, SolverArguments arguments, CancellationToken cancellationToken = default)
        {          
            var identifiedCaptchaTypes = await AllCaptchaTypesIdentifier.IdentifyAsync(driver, arguments, cancellationToken);
            return identifiedCaptchaTypes.Count switch
            {
                > 1 => throw new UnidentifiableCaptchaException($"Unable to identify captcha. Found multiple matching captcha types: " +
                                                                $"{string.Join(',', identifiedCaptchaTypes.Select(x => x.ToString()))}"),
                0 => throw new UnidentifiableCaptchaException($"Unable to identify captcha. Did not find any captcha on current page."),
                _ => identifiedCaptchaTypes.First()
            };
        }

        public static async Task<TaskResultResponse<TSolution>> SolveCaptchaAsync<TSolution>(
            this IWebDriver driver, 
            string clientKey,
            CaptchaRequest<TSolution> captchaRequest,
            ActionArguments? actionArguments = null,
            SolverConfig? solverConfig = null,
            CancellationToken cancellationToken = default)
            where TSolution : BaseSolution, new()
        {
            var captchaType = captchaRequest.GetCaptchaType();
            var solver = SolverFactory.GetSolver<TSolution>(driver, clientKey, captchaType, solverConfig ?? new DefaultSolverConfig());
            return await solver.SolveAsync(captchaRequest, actionArguments ?? new ActionArguments(), cancellationToken);
        }

        public static async Task<TaskResultResponse<TSolution>> SolveCaptchaAsync<TSolution>(
            this IWebDriver driver,
            string clientKey,
            SolverArguments? additionalArguments = null,
            ActionArguments? actionArguments = null,
            SolverConfig? solverConfig = null,
            CancellationToken cancellationToken = default) where TSolution : BaseSolution, new()
        {
            additionalArguments ??= new SolverArguments();
            var captchaType = additionalArguments.CaptchaType ?? await AllCaptchaTypesIdentifier.IdentifyCaptchaAsync<TSolution>(driver, additionalArguments, cancellationToken);

            if (!captchaType.HasValue)
            {
                throw new UnidentifiableCaptchaException("Could not identify the captcha type from arguments. Please provide captchaType or better additional arguments.");
            }

            ValidateSolutionOutputToCaptchaType<TSolution>(captchaType.Value);
            var solver = SolverFactory.GetSolver<TSolution>(driver,  clientKey, captchaType.Value, solverConfig ?? new DefaultSolverConfig());
            return await solver.SolveAsync(additionalArguments, actionArguments: actionArguments ?? new ActionArguments() , cancellationToken);

        }

        private static void ValidateSolutionOutputToCaptchaType<TSolution>(CaptchaType captchaType)
            where TSolution : BaseSolution, new()
        {
            var wrongSolutionTypeMessage = $"Wrong solution type. Was trying to create solution of type: {typeof(TSolution)} with captcha type: {captchaType}";
            var captchaSolutionType = captchaType.GetSolutionType();

            if (typeof(TSolution) != captchaSolutionType)
            {
                throw new ArgumentException(wrongSolutionTypeMessage);
            }
        }
    }
}