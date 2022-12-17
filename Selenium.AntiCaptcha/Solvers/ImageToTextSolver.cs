using AntiCaptchaApi.Net.Enums;
using AntiCaptchaApi.Net.Models.Solutions;
using AntiCaptchaApi.Net.Requests;
using OpenQA.Selenium;
using Selenium.AntiCaptcha.Exceptions;
using Selenium.AntiCaptcha.Internal.Extensions;
using Selenium.AntiCaptcha.Internal.Helpers;
using Selenium.AntiCaptcha.Models;
using Selenium.AntiCaptcha.Solvers.Base;

namespace Selenium.AntiCaptcha.Solvers
{
    internal class ImageToTextSolver : Solver<ImageToTextRequest, ImageToTextSolution>
    {
        protected override string GetSiteKey()
        {
            return string.Empty;
        }

        protected override ImageToTextRequest BuildRequest(SolverAdditionalArguments additionalArguments)
        {
            if (!string.IsNullOrEmpty(additionalArguments.ImageFilePath))
            {
                return new ImageToTextRequest
                {
                    Phrase = additionalArguments.Phrase ?? false,
                    Case = additionalArguments.Case ?? false,
                    Numeric = additionalArguments.Numeric ?? NumericOption.NoRequirements,
                    Math = additionalArguments.Math ?? 0,
                    MinLength = additionalArguments.MinLength ?? 0,
                    MaxLength = additionalArguments.MaxLength ?? 0,
                    Comment = additionalArguments.Comment,
                    FilePath = additionalArguments.ImageFilePath
                };
            }

            var bodyBase64 = string.Empty;
            if (additionalArguments.ImageElement == null)
            {
                
                var possibleImageSource = PageSourceSearcher.FindSingleImageSourceForImageToText(Driver);

                if (string.IsNullOrEmpty(possibleImageSource))
                {
                    throw new UnidentifiableCaptchaException("No image found in the arguments. Please provide one.");    
                }

                var imageWebElement =  Driver.FindByXPathAllFrames($"//img[@src='{possibleImageSource}']");
                bodyBase64 = imageWebElement?.DownloadSourceAsBase64String();
                
                if (string.IsNullOrEmpty(bodyBase64))
                {
                    throw new UnidentifiableCaptchaException("No image found in the arguments. Please provide one.");    
                }
            }
            
            return new ImageToTextRequest
            {
                BodyBase64 = additionalArguments.ImageElement?.DownloadSourceAsBase64String() ?? bodyBase64,           
                Phrase = additionalArguments.Phrase ?? false,
                Case = additionalArguments.Case ?? false,
                Numeric = additionalArguments.Numeric ?? NumericOption.NoRequirements,
                Math = additionalArguments.Math ?? 0,
                MinLength = additionalArguments.MinLength ?? 0,
                MaxLength = additionalArguments.MaxLength ?? 0,
                Comment = additionalArguments.Comment,
            };
        }


        protected override void FillResponseElement(ImageToTextSolution solution, IWebElement? responseElement)
        {
            responseElement ??= Driver.FindElement(By.Name("captchaWord"));
            responseElement?.SendKeys(solution.Text);
        }

        public ImageToTextSolver(string clientKey, IWebDriver driver) : base(clientKey, driver)
        {
        }
    }
}