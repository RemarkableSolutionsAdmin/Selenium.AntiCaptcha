using AntiCaptchaApi.Net.Models.Solutions;
using AntiCaptchaApi.Net.Requests.Abstractions.Interfaces;
using OpenQA.Selenium;
using Selenium.AntiCaptcha.Models;
using Selenium.CaptchaIdentifier.Extensions;
using Selenium.FramesSearcher.Extensions;

namespace Selenium.AntiCaptcha.Solvers.Base;

public abstract class FunCaptchaSolverBase<TRequest> : Solver<TRequest, FunCaptchaSolution> 
    where TRequest : ICaptchaRequest<FunCaptchaSolution>
{
    protected override string GetSiteKey()
    {
        return Driver.FindFunCaptchaSiteKey();
    }

    protected override async Task FillResponseElement(FunCaptchaSolution solution, ActionArguments actionArguments)
    {
        if (actionArguments.ResponseElement != null)
        {
            actionArguments.ResponseElement.SendKeys(solution.Token);
        }
        else if(actionArguments.ShouldFindAndFillAccordingResponseElements)
        {
            try
            {           
                var recaptchaElementIds = Driver
                    .FindManyValuesByXPathAllFrames(
                        "id",
                        "//input[@id='fc-token' or @id='FunCaptcha-Token' or @id='verification-token']") // verification-token
                    .Distinct()
                    .ToList();


                foreach (var elementId in recaptchaElementIds)
                {
                    try
                    {
                        await Driver.SetValueForElementWithIdInAllFrames(elementId, solution.Token);
                    }
                    catch (Exception)
                    {
                        // ignore
                    }
                }
            }
            catch (Exception)
            {
                // ignore
            }
        }
    }

    protected FunCaptchaSolverBase(string clientKey, IWebDriver driver, SolverConfig solverConfig) : base(clientKey, driver, solverConfig)
    {
    }
}