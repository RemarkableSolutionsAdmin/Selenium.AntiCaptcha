using AntiCaptchaApi.Net.Models.Solutions;
using AntiCaptchaApi.Net.Requests.Abstractions;
using OpenQA.Selenium;

namespace Selenium.AntiCaptcha.Solvers.Base;

public abstract class GeeTestV3SolverBase<TRequest> : GeeSolverBase <TRequest, GeeTestV3Solution>
    where TRequest : CaptchaRequest<GeeTestV3Solution>
{
    protected GeeTestV3SolverBase(string clientKey, IWebDriver driver) : base(clientKey, driver)
    {
    }
}