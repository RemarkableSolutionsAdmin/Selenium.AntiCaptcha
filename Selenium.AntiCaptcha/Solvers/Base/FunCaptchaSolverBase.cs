using AntiCaptchaApi.Net.Models.Solutions;
using AntiCaptchaApi.Net.Requests.Abstractions;
using OpenQA.Selenium;
using Selenium.AntiCaptcha.Internal.Helpers;
using Selenium.AntiCaptcha.Models;

namespace Selenium.AntiCaptcha.Solvers.Base;

public abstract class FunCaptchaSolverBase<TRequest> : Solver<TRequest, FunCaptchaSolution> where TRequest : CaptchaRequest<FunCaptchaSolution>
{
    protected override string GetSiteKey()
    {
        try
        {
            return Driver.FindElement(By.Id("funcaptcha")).GetAttribute("data-pkey");
        }
        catch (Exception e)
        {
            // ignore
        }
 
        return PageSourceSearcher.FindFunCaptchaSiteKey(Driver);
    }

    protected FunCaptchaSolverBase(string clientKey, IWebDriver driver, SolverConfig solverConfig) : base(clientKey, driver, solverConfig)
    {
    }
}