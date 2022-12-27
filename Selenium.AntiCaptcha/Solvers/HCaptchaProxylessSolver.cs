using AntiCaptchaApi.Net.Models.Solutions;
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
            WebsiteUrl = arguments.Url,
            WebsiteKey = arguments.SiteKey,
            UserAgent = arguments.UserAgent
        };
    

    public HCaptchaProxylessSolver(string clientKey, IWebDriver driver, SolverConfig solverConfig) : base(clientKey, driver, solverConfig)
    {
    }
}