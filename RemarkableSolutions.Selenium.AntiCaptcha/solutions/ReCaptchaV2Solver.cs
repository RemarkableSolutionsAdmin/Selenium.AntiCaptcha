using OpenQA.Selenium;
using RemarkableSolutions.Anticaptcha.Api.Anticaptchas;
using RemarkableSolutions.Anticaptcha.Api.Responses;

namespace RemarkableSolutions.Selenium.AntiCaptcha.solutions
{
    internal class ReCaptchaV2Solver : Solver
    {
        protected override string GetSiteKey(IWebDriver driver) => driver.FindElement(By.ClassName("g-recaptcha")).GetAttribute("data-sitekey");

        internal override void Solve(IWebDriver driver, string clientKey, string? url, string? siteKey, IWebElement? submitElement)
        {

            if (string.IsNullOrWhiteSpace(siteKey))
            {
                siteKey = GetSiteKey(driver);
            }
            var anticaptchaTask = new RecaptchaV2ProxylessTask
            {
                ClientKey = clientKey,
                WebsiteUrl = new Uri(url ?? driver.Url),
                WebsiteKey = siteKey
            };

            anticaptchaTask.CreateTask();
            var result = anticaptchaTask.WaitForTaskResult();

            if (result.Success)
            {
                var solution = anticaptchaTask.GetTaskSolution();
                AddCookies(driver, solution);

                var js = driver as IJavaScriptExecutor;
                js.ExecuteScript($"window.localStorage.setItem('_grecaptcha','{solution.GRecaptchaResponse}');");
                js.ExecuteScript($"document.getElementById('g-recaptcha-response').innerText='{solution.GRecaptchaResponse}';");
            }

            if (submitElement != null)
            {
                submitElement.Click();
            }
        }
    }
}
