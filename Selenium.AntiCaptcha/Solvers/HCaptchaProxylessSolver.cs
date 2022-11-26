using AntiCaptchaApi.Net.Models;
using AntiCaptchaApi.Net.Models.Solutions;
using AntiCaptchaApi.Net.Requests;
using OpenQA.Selenium;
using Selenium.AntiCaptcha.Constants;
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


    protected override void FillResponseElement(IWebDriver driver, HCaptchaSolution solution, IWebElement? responseElement)
    {
        if (responseElement == null)
        {
            responseElement = driver.FindElement(By.Name("h-captcha-response"));
        }

        responseElement.SendKeys(solution.GRecaptchaResponse);
    }
}