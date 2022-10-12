using OpenQA.Selenium;
using AntiCaptchaApi.Net;
using AntiCaptchaApi.Net.Enums;
using AntiCaptchaApi.Net.Models;
using AntiCaptchaApi.Net.Models.Solutions;
using AntiCaptchaApi.Net.Requests;
using AntiCaptchaApi.Net.Responses;

namespace Selenium.AntiCaptcha.solvers
{
    internal class ReCaptchaV2Solver : Solver <RecaptchaV2ProxylessRequest, RecaptchaSolution>
    {
        protected override string GetSiteKey(IWebDriver driver, int waitingTime = 1000) => driver
            .FindElement(By.ClassName("g-recaptcha")).GetAttribute("data-sitekey");

        protected override RecaptchaV2ProxylessRequest BuildRequest(IWebDriver driver, string? url, string? siteKey, IWebElement? imageElement, string? userAgent, ProxyConfig proxyConfig)
        {
            return new RecaptchaV2ProxylessRequest
            {
                WebsiteUrl = url ?? driver.Url,
                WebsiteKey = siteKey
            };
        }

        protected override void FillResponseElement(IWebDriver driver, RecaptchaSolution solution, IWebElement? responseElement)
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

        internal override TaskResultResponse<RecaptchaSolution> Solve(IWebDriver driver, string clientKey, string? url, string? siteKey,
            IWebElement? responseElement,
            IWebElement? submitElement, IWebElement? imageElement, string? userAgent, ProxyConfig proxyConfig)
        {
            var client = new AnticaptchaClient(clientKey);
            siteKey ??= GetSiteKey(driver);

            var anticaptchaRequest = new RecaptchaV2ProxylessRequest
            {
                WebsiteUrl = url ?? driver.Url,
                WebsiteKey = siteKey
            };

            var creationTaskResult = client.CreateCaptchaTask(anticaptchaRequest);
            var result = client.WaitForTaskResult<RecaptchaSolution>(creationTaskResult.TaskId.Value);

            if (result.Status == TaskStatusType.Ready)
            {
                var solution = result.Solution;
                //AddCookies(driver, solution.Cookies);
                FillResponseElement(driver, solution, responseElement);
            }

            if (submitElement != null)
            {
                submitElement.Click();
            }
            return result;
        }
    }
}
