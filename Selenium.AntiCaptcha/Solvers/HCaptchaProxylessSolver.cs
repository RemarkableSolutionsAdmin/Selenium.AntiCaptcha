using AntiCaptchaApi.Net.Requests;
using OpenQA.Selenium;
using Selenium.AntiCaptcha.Models;
using Selenium.AntiCaptcha.Solvers.Base;

namespace Selenium.AntiCaptcha.Solvers;

internal class HCaptchaProxylessSolver  : HCaptchaSolverBase<HCaptchaProxylessRequest>
{
    protected override HCaptchaProxylessRequest BuildRequest(SolverArguments arguments) =>
        new()
        {
            WebsiteUrl = arguments.WebsiteUrl,
            WebsiteKey = arguments.WebsiteKey,
            UserAgent = arguments.UserAgent
        };
    

    public HCaptchaProxylessSolver(string clientKey, IWebDriver driver, SolverConfig solverConfig) : base(clientKey, driver, solverConfig)
    {
    }
}