using AntiCaptchaApi.Net.Models.Solutions;
using AntiCaptchaApi.Net.Requests;
using OpenQA.Selenium;
using Selenium.AntiCaptcha.Models;
using Selenium.AntiCaptcha.Solvers.Base;

namespace Selenium.AntiCaptcha.Solvers;

internal class HCaptchaProxylessSolver  : Solver<HCaptchaProxylessRequest, HCaptchaSolution>
{
    protected override HCaptchaProxylessRequest BuildRequest(SolverAdditionalArguments additionalArguments) =>
        new()
        {
            WebsiteUrl = additionalArguments.Url,
            WebsiteKey = additionalArguments.SiteKey,
            UserAgent = additionalArguments.UserAgent
        };


    protected override void FillResponseElement(HCaptchaSolution solution, IWebElement? responseElement)
    {
        if (responseElement == null)
        {
            responseElement = Driver.FindElement(By.Name("h-captcha-response"));
        }

        responseElement.SendKeys(solution.GRecaptchaResponse);
    }

    public HCaptchaProxylessSolver(string clientKey, IWebDriver driver) : base(clientKey, driver)
    {
    }
}