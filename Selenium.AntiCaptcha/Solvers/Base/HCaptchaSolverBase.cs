using AntiCaptchaApi.Net.Models.Solutions;
using AntiCaptchaApi.Net.Requests.Abstractions.Interfaces;
using OpenQA.Selenium;
using Selenium.AntiCaptcha.Models;

namespace Selenium.AntiCaptcha.Solvers.Base;

public abstract class HCaptchaSolverBase<TRequest> : Solver <TRequest, HCaptchaSolution>
    where TRequest : ICaptchaRequest<HCaptchaSolution>
{
    protected HCaptchaSolverBase(string clientKey, IWebDriver driver, SolverConfig solverConfig) : base(clientKey, driver, solverConfig)
    {
    }
    
    protected override async Task FillResponseElement(HCaptchaSolution solution, ActionArguments actionArguments)
    {
        try
        {
            var responseElement = actionArguments.ResponseElement;
            if (actionArguments.ShouldFindAndFillAccordingResponseElements)
            {
                responseElement ??= Driver.FindElement(By.Name("h-captcha-response"));
            }

            if (responseElement != null)
            {
                actionArguments.ResponseElement?.SendKeys(solution.GRecaptchaResponse);   
            }
        }
        catch (Exception e)
        {
            // ignore
        }
    }
}