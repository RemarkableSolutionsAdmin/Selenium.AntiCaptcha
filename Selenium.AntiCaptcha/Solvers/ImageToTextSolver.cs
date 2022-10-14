﻿using System.Net;
using AntiCaptchaApi.Net.Models;
using AntiCaptchaApi.Net.Models.Solutions;
using AntiCaptchaApi.Net.Requests;
using OpenQA.Selenium;
using Selenium.AntiCaptcha.Solvers.Base;

namespace Selenium.AntiCaptcha.Solvers
{
    internal class ImageToTextSolver : Solver<ImageToTextRequest, ImageToTextSolution>
    {
        protected override string GetSiteKey(IWebDriver driver, int waitingTime = 1000, int tries = 3)
        {
            return string.Empty;
        }

        protected override ImageToTextRequest BuildRequest(IWebDriver driver, string? url, string? siteKey, IWebElement? imageElement, string? userAgent, ProxyConfig proxyConfig)
        {
            if (imageElement == null)
            {
                throw new ArgumentException("No image found in the arguments. Please provide one.,");
            }

            var elementSrc = imageElement.GetAttribute("src");


            byte[]? file = null;
            using (WebClient webClient = new())
            {
                file = webClient.DownloadData(elementSrc);
            }

            var base64String = Convert.ToBase64String(file);
            var anticaptchaRequest = new ImageToTextRequest
            {
                BodyBase64 = base64String
            };
            return anticaptchaRequest;
        }

        protected override void FillResponseElement(IWebDriver driver, ImageToTextSolution solution, IWebElement? responseElement)
        {
            if (responseElement == null)
            {
                responseElement = driver.FindElement(By.Name("captchaWord"));
            }

            //todo if not response element, throw an exception
            responseElement.SendKeys(solution.Text);
        }
    }
}