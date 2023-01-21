﻿using AntiCaptchaApi.Net.Models.Solutions;
using AntiCaptchaApi.Net.Requests;
using OpenQA.Selenium;
using Selenium.AntiCaptcha.Exceptions;
using Selenium.AntiCaptcha.Models;
using Selenium.AntiCaptcha.Solvers.Base;
using Selenium.FramesSearcher.Extensions;

namespace Selenium.AntiCaptcha.Solvers
{
    internal class ImageToTextSolver : Solver<ImageToTextRequest, ImageToTextSolution>
    {
        protected override string GetSiteKey()
        {
            return string.Empty;
        }

        protected override ImageToTextRequest BuildRequest(SolverArguments arguments)
        {
            if (!string.IsNullOrEmpty(arguments.ImageFilePath))
            {
                return new ImageToTextRequest(arguments);
            }

            var bodyBase64 = string.Empty;
            if (arguments.ImageElement == null)
            {
                var possibleImageSource = PageSourceSearcher.FindSingleImageSourceForImageToText(Driver);

                if (string.IsNullOrEmpty(possibleImageSource))
                {
                    throw new InsufficientSolverArgumentsException("No image found in the arguments. Please provide one.");    
                }

                var imageWebElement =  Driver.FindByXPathAllFrames($"//img[@src='{possibleImageSource}']");
                bodyBase64 = imageWebElement?.DownloadSourceAsBase64String();
                
                if (string.IsNullOrEmpty(bodyBase64))
                {
                    throw new InsufficientSolverArgumentsException("No image found in the arguments. Please provide one.");    
                }
            }
            
            return new ImageToTextRequest
            {
                BodyBase64 = arguments.ImageElement?.DownloadSourceAsBase64String() ?? bodyBase64,           
                Phrase = arguments.Phrase,
                Case = arguments.Case,
                Numeric = arguments.Numeric,
                Math = arguments.Math,
                MinLength = arguments.MinLength,
                MaxLength = arguments.MaxLength,
                Comment = arguments.Comment,
            };
        }


        protected override async Task FillResponseElement(ImageToTextSolution solution, ActionArguments actionArguments)
        {
            try
            {
                var responseElement = actionArguments.ResponseElement;
                if (actionArguments.ShouldFindAndFillAccordingResponseElements)
                {
                    responseElement ??= Driver.FindElement(By.Name("captchaWord"));   
                }

                responseElement?.SendKeys(solution.Text);
            }
            catch (Exception)
            {
                // ignored
            }
        }

        public ImageToTextSolver(string clientKey, IWebDriver driver, SolverConfig solverConfig) : base(clientKey, driver, solverConfig)
        {
        }
    }
}