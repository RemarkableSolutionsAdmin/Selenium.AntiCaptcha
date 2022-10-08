using AntiCaptchaApi.Requests;
using OpenQA.Selenium;
using System.Text.RegularExpressions;

namespace Selenium.AntiCaptcha.solvers
{
    internal class GeeTestSolver : Solver
    {
        protected override void FillResponseElement(IWebDriver driver, SolutionData solution, IWebElement? responseElement)
        {
            throw new NotImplementedException();
        }

        protected override string GetSiteKey(IWebDriver driver)
        {
            var regex = new Regex("gt=(.*?)&");
            return regex.Match(driver.PageSource).Groups[1].Value;
        }

        private string GetChallenge(IWebDriver driver)
        {
            var regex = new Regex("challenge=(.*?)&");
            return regex.Match(driver.PageSource).Groups[1].Value;
        }

        internal override void Solve(IWebDriver driver, string clientKey, string? url, string? siteKey, IWebElement? responseElement,
            IWebElement? submitElement, IWebElement? imageElement)
        {
            siteKey ??= GetSiteKey(driver);
            var challenge = GetChallenge(driver);

            var captchaRequest = new GeeTestV3ProxylessRequest
            {
                ClientKey = clientKey,
                WebsiteUrl = new Uri(url ?? driver.Url),
                WebsiteKey = siteKey,
                WebsiteChallenge = challenge,
                GeetestApiServerSubdomain = "api.geetest.com"
            };

            var creationTaskResult = AnticaptchaManager.CreateCaptchaTask(captchaRequest);
            var result = AnticaptchaManager.WaitForTaskResult(creationTaskResult.TaskId.Value);

            if (result.Status == Anticaptcha.Api.Responses.TaskStatusType.Ready)
            {
                var solution = anticaptchaTask.GetTaskSolution();
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
