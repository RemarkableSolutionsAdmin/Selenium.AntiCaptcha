using OpenQA.Selenium;
using AntiCaptchaApi;
using AntiCaptchaApi.Models;
using AntiCaptchaApi.Requests;
using AntiCaptchaApi.Requests.Abstractions;

namespace Selenium.AntiCaptcha.solvers
{
    internal class HCaptchaSolver : Solver
    {
        protected override void FillResponseElement(IWebDriver driver, SolutionData solution, IWebElement? responseElement)
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
            siteKey ??= GetSiteKey(driver);

            var captchaRequest = new HWebsiteCaptchaProxylessRequest
            {
                ClientKey = clientKey,
                WebsiteUrl = new Uri(url ?? driver.Url),
                WebsiteKey = siteKey
            };


            var creationTaskResult = AnticaptchaManager.CreateCaptchaTask(captchaRequest);
            var result = AnticaptchaManager.WaitForTaskResult(creationTaskResult.TaskId.Value);

            if (result.Success)
            {
                FillResponseElement(driver, anticaptchaTask.GetTaskSolution(), responseElement);

            }

            if (submitElement != null)
            {
                submitElement.Click();
            }
        }
    }
}
