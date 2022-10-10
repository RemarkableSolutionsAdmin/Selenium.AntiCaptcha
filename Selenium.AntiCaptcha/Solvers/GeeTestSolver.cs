using AntiCaptchaApi;
using AntiCaptchaApi.Enums;
using AntiCaptchaApi.Models.Solutions;
using AntiCaptchaApi.Requests;
using OpenQA.Selenium;
using System.Text.RegularExpressions;

namespace Selenium.AntiCaptcha.solvers
{
    internal class GeeTestSolver : Solver
    {
        protected override void FillResponseElement(IWebDriver driver, RawSolution solution, IWebElement? responseElement)
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

        internal override void Solve(IWebDriver driver, 
            string clientKey, 
            string? url,
            string? gt,
            IWebElement? responseElement,
            IWebElement? submitElement,
            IWebElement? imageElement)
        {
            var client = new AnticaptchaClient(clientKey);
            gt ??= GetSiteKey(driver);
            var challenge = GetChallenge(driver);

            var captchaRequest = new GeeTestV3ProxylessRequest
            {
                WebsiteUrl = url ?? driver.Url,
                Challenge = challenge,
                GeetestApiServerSubdomain = "api.geetest.com",
                Gt = gt
            };

            var creationTaskResult = client.CreateCaptchaTask(captchaRequest);
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
