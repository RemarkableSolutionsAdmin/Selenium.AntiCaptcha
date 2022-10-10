using System.Net;
using OpenQA.Selenium;
using AntiCaptchaApi.Models.Solutions;
using AntiCaptchaApi;
using AntiCaptchaApi.Requests;
using AntiCaptchaApi.Enums;

namespace Selenium.AntiCaptcha.solvers
{
    internal class ImageToTextSolver : Solver
    {
        protected override string GetSiteKey(IWebDriver driver)
        {
            return string.Empty;
        }

        protected override void FillResponseElement(IWebDriver driver, RawSolution solution, IWebElement? responseElement)
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
            var client = new AnticaptchaClient(clientKey);
            siteKey ??= GetSiteKey(driver);

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
            var anticaptchaRequest = new ImageToTextRequest
            {
                BodyBase64 = base64String
            };

            var creationTaskResult = client.CreateCaptchaTask(anticaptchaRequest);
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