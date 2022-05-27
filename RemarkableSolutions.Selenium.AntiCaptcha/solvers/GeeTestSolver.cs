using OpenQA.Selenium;
using RemarkableSolutions.Anticaptcha.Api.Anticaptchas;
using RemarkableSolutions.Anticaptcha.Api.Responses;
using RemarkableSolutions.Selenium.AntiCaptcha.solvers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace RemarkableSolutions.Selenium.AntiCaptcha.solvers
{
    internal class GeeTestSolver : Solver
    {
        protected override void FillResponseElement(IWebDriver driver, TaskResultResponse.SolutionData solution, IWebElement? responseElement)
        {
            throw new NotImplementedException();
        }

        protected override string GetSiteKey(IWebDriver driver)
        {
            var regex = new Regex("captcha_id=(.*?)&");
            return regex.Match(driver.PageSource).Groups[1].Value;
        }

        private string GetChallenge(IWebDriver driver)
        {
            var regex = new Regex("challenge=(.*?)&");
            return regex.Match(driver.PageSource).Groups[1].Value;
        }

        internal override void Solve(IWebDriver driver, string clientKey, string? url, string? siteKey, IWebElement? responseElement, IWebElement? submitElement)
        {
            siteKey ??= GetSiteKey(driver);
            var challenge = GetChallenge(driver);

            var anticaptchaTask = new GeeTestProxylessTask
            {
                ClientKey = clientKey,
                WebsiteUrl = new Uri(url ?? driver.Url),
                WebsiteKey = siteKey,
                WebsiteChallenge = challenge
            };

            anticaptchaTask.CreateTask();
            var result = anticaptchaTask.WaitForTaskResult();

            if (result.Success)
            {
                var solution = anticaptchaTask.GetTaskSolution();
                //AddCookies(driver, solution);
                //FillResponseElement(driver, solution, responseElement);
            }

            if (submitElement != null)
            {
                submitElement.Click();
            }
        }
    }
}
