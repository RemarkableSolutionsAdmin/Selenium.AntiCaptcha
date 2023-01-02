using AntiCaptchaApi.Net.Models.Solutions;
using AntiCaptchaApi.Net.Requests.Abstractions;
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
    
    protected override void FillResponseElement(HCaptchaSolution solution, IWebElement? responseElement)
    {
        try
        {
            responseElement ??= Driver.FindElement(By.Name("h-captcha-response"));
            responseElement?.SendKeys(solution.GRecaptchaResponse);
        }
        catch (Exception e)
        {
            // ignore
        }
    }
}