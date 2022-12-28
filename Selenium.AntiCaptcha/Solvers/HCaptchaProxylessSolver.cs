using AntiCaptchaApi.Net.Requests;
using AntiCaptchaApi.Net.Requests.Abstractions.Interfaces;
using OpenQA.Selenium;
using Selenium.AntiCaptcha.Models;
using Selenium.AntiCaptcha.Solvers.Base;

namespace Selenium.AntiCaptcha.Solvers;

internal class HCaptchaProxylessSolver  : HCaptchaSolverBase<IHCaptchaProxylessRequest>
{
    protected override IHCaptchaProxylessRequest BuildRequest(SolverArguments arguments) =>
        new HCaptchaProxylessRequest(arguments);
    

    public HCaptchaProxylessSolver(string clientKey, IWebDriver driver, SolverConfig solverConfig) : base(clientKey, driver, solverConfig)
    {
    }
}