using AntiCaptchaApi.Net.Models.Solutions;
using AntiCaptchaApi.Net.Requests;
using OpenQA.Selenium;
using Selenium.AntiCaptcha.Models;
using Selenium.AntiCaptcha.Solvers.Base;

namespace Selenium.AntiCaptcha.Solvers;

internal class HCaptchaProxylessSolver  : HCaptchaSolverBase<HCaptchaProxylessRequest>
{
    protected override HCaptchaProxylessRequest BuildRequest(SolverAdditionalArguments additionalArguments) =>
        new()
        {
            WebsiteUrl = additionalArguments.Url,
            WebsiteKey = additionalArguments.SiteKey,
            UserAgent = additionalArguments.UserAgent
        };
    

    public HCaptchaProxylessSolver(string clientKey, IWebDriver driver) : base(clientKey, driver)
    {
    }
}