using OpenQA.Selenium;
using AntiCaptchaApi;
using AntiCaptchaApi.Models;
using AntiCaptchaApi.Requests;
using AntiCaptchaApi.Requests.Abstractions;
using AntiCaptchaApi.Models.Solutions;
using AntiCaptchaApi.Enums;

namespace Selenium.AntiCaptcha.solvers
{
    internal class HCaptchaSolver : Solver
    {
        protected override void FillResponseElement(IWebDriver driver, RawSolution solution, IWebElement? responseElement)
        {
            if (responseElement == null)
            {
                responseElement = driver.FindElement(By.Name("h-captcha-response"));
            }

            responseElement.SendKeys(solution.GRecaptchaResponse);
        }

        protected override string GetSiteKey(IWebDriver driver) => driver.FindElement(By.ClassName("h-captcha")).GetAttribute("data-sitekey");

        internal override void Solve(IWebDriver driver, string clientKey, string? url, string? siteKey, IWebElement? responseElement,
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
            var result = client.WaitForRawTaskResult<RawSolution>(creationTaskResult.TaskId.Value);

            if (result.Status == TaskStatusType.Ready)
            {
                FillResponseElement(driver, result.Solution, responseElement);

            }

            if (submitElement != null)
            {
                submitElement.Click();
            }
        }
    }
}
