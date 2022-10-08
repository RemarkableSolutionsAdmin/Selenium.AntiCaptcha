using System.Net;
using OpenQA.Selenium;
using AntiCaptcha.Api.Models;

namespace Selenium.AntiCaptcha.solvers
{
    internal class ImageToTextSolver : Solver
    {
        protected override string GetSiteKey(IWebDriver driver)
        {
            return string.Empty;
        }

        protected override void FillResponseElement(IWebDriver driver, SolutionData solution, IWebElement? responseElement)
        {
            if (responseElement == null)
            {
                responseElement = driver.FindElement(By.Name("captchaWord"));
            }

            //todo if not response element, throw an exception
            responseElement.SendKeys(solution.Text);
        }

        internal override void Solve(IWebDriver driver, string clientKey, string? url, string? siteKey, IWebElement? responseElement,
            IWebElement? submitElement, IWebElement? imageElement)
        {
            siteKey ??= GetSiteKey(driver);

            var anticaptchaTask = new ImageToTextTask()
            {
                ClientKey = clientKey,
            };
            if (imageElement == null)
            {
                throw new ArgumentException("No image found in the arguments. Please provide one.,");
            }

            var elementSrc = imageElement.GetAttribute("src");

            byte[] file = null;

            using (WebClient webClient = new())
            {
                file = webClient.DownloadData(elementSrc);
            }

            var base64String = Convert.ToBase64String(file);

            anticaptchaTask.BodyBase64 = base64String;

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