using System.Text.RegularExpressions;
using AntiCaptchaApi.Net.Models.Solutions;
using AntiCaptchaApi.Net.Requests.Abstractions;
using OpenQA.Selenium;
using Selenium.AntiCaptcha.Internal.Extensions;
using Selenium.AntiCaptcha.Internal.Helpers;

namespace Selenium.AntiCaptcha.Solvers.Base;

internal abstract class FunCaptchaSolverBase<TRequest, TSolution> : Solver<TRequest, TSolution>
    where TRequest : CaptchaRequest<TSolution>
    where TSolution : FunCaptchaSolution, new()
{
    protected override string GetSiteKey(IWebDriver driver)
    {
        try
        {
            return driver.FindElement(By.Id("funcaptcha")).GetAttribute("data-pkey");
        }
        catch (Exception e)
        {
            // ignore
        }
 
        return PageSourceSearcher.FindFunCaptchaSiteKey(driver);
    }
}