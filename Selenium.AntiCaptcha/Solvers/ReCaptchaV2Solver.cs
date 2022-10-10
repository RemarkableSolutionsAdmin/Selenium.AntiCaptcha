using OpenQA.Selenium;
using AntiCaptchaApi.Models.Solutions;
using AntiCaptchaApi;
using AntiCaptchaApi.Requests;
using AntiCaptchaApi.Enums;

namespace Selenium.AntiCaptcha.solvers
{
    internal class ReCaptchaV2Solver : Solver
    {

        protected override string GetSiteKey(IWebDriver driver) => driver.FindElement(By.ClassName("g-recaptcha")).GetAttribute("data-sitekey");

        protected override void FillResponseElement(IWebDriver driver, RawSolution solution, IWebElement? responseElement)
        {
            if (responseElement != null)
            {
                responseElement.SendKeys(solution.GRecaptchaResponse);
            }
            else
            {
                var js = driver as IJavaScriptExecutor;
                js.ExecuteScript($"window.localStorage.setItem('_grecaptcha','{solution.GRecaptchaResponse}');");
                js.ExecuteScript($"document.getElementById('g-recaptcha-response').innerText='{solution.GRecaptchaResponse}';");
            }
        }

        internal override void Solve(IWebDriver driver, string clientKey, string? url, string? siteKey, IWebElement? responseElement,
            IWebElement? submitElement, IWebElement? imageElement)
        {
            var client = new AnticaptchaClient(clientKey);
            siteKey ??= GetSiteKey(driver);

            var anticaptchaRequest = new RecaptchaV2ProxylessRequest
            {
                WebsiteUrl = url ?? driver.Url,
                WebsiteKey = siteKey
            };

            var creationTaskResult = client.CreateCaptchaTask(anticaptchaRequest);
            var result = client.WaitForRawTaskResult<RawSolution>(creationTaskResult.TaskId.Value);

            if (result.Status == TaskStatusType.Ready)
            {
                var solution = result.Solution;
                AddCookies(driver, solution);
                FillResponseElement(driver, solution, responseElement);
            }

            if (submitElement != null)
            {
                submitElement.Click();
            }
        }
    }
}
