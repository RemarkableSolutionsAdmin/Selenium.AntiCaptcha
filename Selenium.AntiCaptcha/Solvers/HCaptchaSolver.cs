using AntiCaptchaApi.Net.Models;
using AntiCaptchaApi.Net.Models.Solutions;
using AntiCaptchaApi.Net.Requests;
using OpenQA.Selenium;
using Selenium.AntiCaptcha.Constants;
using Selenium.AntiCaptcha.Internal.Extensions;
using Selenium.AntiCaptcha.Models;
using Selenium.AntiCaptcha.Solvers.Base;

namespace Selenium.AntiCaptcha.Solvers
{
    internal class HCaptchaSolver : Solver<HCaptchaRequest, HCaptchaSolution>
    {
        protected override HCaptchaRequest BuildRequest(SolverAdditionalArguments additionalArguments)
        {
            return new HCaptchaRequest
            {
                WebsiteUrl = additionalArguments.Url,
                WebsiteKey = additionalArguments.SiteKey,
                UserAgent = additionalArguments.UserAgent,
                ProxyConfig = additionalArguments.ProxyConfig,
                IsInvisible = additionalArguments.IsInvisible!.Value,
                EnterprisePayload = additionalArguments.EnterprisePayload
            };
        }

        protected override async Task<SolverAdditionalArguments> FillMissingAdditionalArguments(IWebDriver driver, SolverAdditionalArguments solverAdditionalArguments)
        {
            return await base.FillMissingAdditionalArguments(driver, solverAdditionalArguments) with
            {
                IsInvisible = solverAdditionalArguments.IsInvisible ?? false,
                EnterprisePayload = solverAdditionalArguments.EnterprisePayload ?? new Dictionary<string, string>()
            };
        }

        protected override void FillResponseElement(IWebDriver driver, HCaptchaSolution solution, IWebElement? responseElement)
        {
            responseElement ??= driver.FindElement(By.Name("h-captcha-response"));
            responseElement.SendKeys(solution.GRecaptchaResponse);
        }
    }
}
