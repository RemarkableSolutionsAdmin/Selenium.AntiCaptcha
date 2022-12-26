using AntiCaptchaApi.Net.Models.Solutions;
using AntiCaptchaApi.Net.Requests.Abstractions;
using OpenQA.Selenium;

namespace Selenium.AntiCaptcha.Solvers.Base;

public abstract class HCaptchaSolverBase<TRequest> : Solver <TRequest, HCaptchaSolution>
    where TRequest : CaptchaRequest<HCaptchaSolution>
{
    protected HCaptchaSolverBase(string clientKey, IWebDriver driver) : base(clientKey, driver)
    {
    }
    
    protected override void FillResponseElement(HCaptchaSolution solution, IWebElement? responseElement)
    {
        responseElement ??= Driver.FindElement(By.Name("h-captcha-response"));
        responseElement?.SendKeys(solution.GRecaptchaResponse);
    }
}