﻿using OpenQA.Selenium;
using RemarkableSolutions.Anticaptcha.Api.Anticaptchas;
using RemarkableSolutions.Anticaptcha.Api.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RemarkableSolutions.Selenium.AntiCaptcha.solutions
{
    internal class HCaptchaSolver : Solver
    {
        protected override void FillResponseElement(IWebDriver driver, TaskResultResponse.SolutionData solution, IWebElement? responseElement)
        {
            if (responseElement == null)
            {
                responseElement = driver.FindElement(By.Name("h-captcha-response"));
            }

            responseElement.SendKeys(solution.GRecaptchaResponse);
        }

        protected override string GetSiteKey(IWebDriver driver) => driver.FindElement(By.ClassName("h-captcha")).GetAttribute("data-sitekey");

        internal override void Solve(IWebDriver driver, string clientKey, string? url, string? siteKey, IWebElement? responseElement, IWebElement? submitElement)
        {
            siteKey ??= GetSiteKey(driver);

            var anticaptchaTask = new HCaptchaProxylessTask
            {
                ClientKey = clientKey,
                WebsiteUrl = new Uri(url ?? driver.Url),
                WebsiteKey = siteKey
            };

            anticaptchaTask.CreateTask();
            var result = anticaptchaTask.WaitForTaskResult();

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