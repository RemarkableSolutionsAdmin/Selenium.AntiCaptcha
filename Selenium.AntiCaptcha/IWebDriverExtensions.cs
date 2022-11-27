using AntiCaptchaApi.Net.Models;
using AntiCaptchaApi.Net.Models.Solutions;
using AntiCaptchaApi.Net.Responses;
using AntiCaptchaApi.Net.Responses.Abstractions;
using OpenQA.Selenium;
using Selenium.AntiCaptcha.Enums;
using Selenium.AntiCaptcha.Internal;
using Selenium.AntiCaptcha.Internal.Extensions;
using Selenium.AntiCaptcha.Models;

namespace Selenium.AntiCaptcha
{
    public static class IWebDriverExtensions
    {
        public static async Task<BaseResponse> SolveCaptchaAsync(this IWebDriver driver,
            string clientKey,
            SolverAdditionalArguments? additionalArguments = null)
        {
            additionalArguments ??= new SolverAdditionalArguments();
            driver.SwitchTo().DefaultContent();
            var captchaType = additionalArguments.CaptchaType;
            
            if (captchaType == null)
            {
                var identifiedCaptchaTypes = await AllCaptchaTypesIdentifier.IdentifyAsync(driver, additionalArguments);
                if (identifiedCaptchaTypes.Count != 1)
                {
                    throw new Exception($"Unable to identify captcha. Found multiple matching captcha types: " +
                                        $"{string.Join(',', identifiedCaptchaTypes.Select(x => x.ToString()))}");
                }
                captchaType = identifiedCaptchaTypes.First();
            }

            dynamic solver = SolverFactory.GetSolver(captchaType.Value);
            var solution = await solver.SolveAsync(driver, clientKey, additionalArguments);
            driver.SwitchTo().DefaultContent();
            return solution;

        }

        public static async Task<TaskResultResponse<TSolution>> SolveCaptchaAsync<TSolution>(this IWebDriver driver, string clientKey,
            SolverAdditionalArguments? additionalArguments = null)
                where TSolution : BaseSolution, new()
        {
            driver.SwitchTo().DefaultContent();
            additionalArguments ??= new SolverAdditionalArguments();
            var captchaType = additionalArguments.CaptchaType ?? await AllCaptchaTypesIdentifier.IdentifyCaptcha<TSolution>(driver, additionalArguments);

            if (!captchaType.HasValue)
            {
                throw new ArgumentNullException(nameof(captchaType), "Could not identify the captcha type from arguments. Please provide captchaType.");
            }

            ValidateSolutionOutputToCaptchaType<TSolution>(captchaType.Value);
            var solver = SolverFactory.GetSolver<TSolution>(captchaType.Value);
            var solution = await solver.SolveAsync(driver, clientKey, additionalArguments);
            
            driver.SwitchTo().DefaultContent();
            
            return solution;

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