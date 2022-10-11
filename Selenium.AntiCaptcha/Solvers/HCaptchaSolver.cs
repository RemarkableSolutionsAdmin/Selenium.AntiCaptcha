using OpenQA.Selenium;
using AntiCaptchaApi.Net;
using AntiCaptchaApi.Net.Enums;
using AntiCaptchaApi.Net.Models.Solutions;
using AntiCaptchaApi.Net.Requests;
using AntiCaptchaApi.Net.Responses;

namespace Selenium.AntiCaptcha.solvers
{
    internal class HCaptchaSolver : Solver<HCaptchaSolution>
    {
        protected override void FillResponseElement(IWebDriver driver, HCaptchaSolution solution, IWebElement? responseElement)
        {
            if (responseElement == null)
            {
                responseElement = driver.FindElement(By.Name("h-captcha-response"));
            }

            responseElement.SendKeys(solution.GRecaptchaResponse);
        }

        protected override string GetSiteKey(IWebDriver driver) => driver.FindElement(By.ClassName("h-captcha")).GetAttribute("data-sitekey");

        internal override TaskResultResponse<HCaptchaSolution> Solve(IWebDriver driver, string clientKey, string? url, string? siteKey,
            IWebElement? responseElement,
            IWebElement? submitElement, IWebElement? imageElement)
        {
            var client = new AnticaptchaClient(clientKey);
            siteKey ??= GetSiteKey(driver);

            var captchaRequest = new HCaptchaProxylessRequest
            {
                WebsiteUrl = url ?? driver.Url,
                WebsiteKey = siteKey
            };


            var creationTaskResult = client.CreateCaptchaTask(captchaRequest);
            var result = client.WaitForTaskResult<HCaptchaSolution>(creationTaskResult.TaskId.Value);

            if (result.Status == TaskStatusType.Ready)
            {
                FillResponseElement(driver, result.Solution, responseElement);

            }

            if (submitElement != null)
            {
                submitElement.Click();
            }
            return result;
        }
    }
}
