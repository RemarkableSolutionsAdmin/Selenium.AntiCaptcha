using AntiCaptchaApi.Net.Enums;
using AntiCaptchaApi.Net.Models.Solutions;
using AntiCaptchaApi.Net.Requests;
using OpenQA.Selenium;
using Selenium.AntiCaptcha.Internal.Extensions;
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
                    Math = additionalArguments.Math ?? false,
                    MinLength = additionalArguments.MinLength ?? 0,
                    MaxLength = additionalArguments.MaxLength ?? 0,
                    Comment = additionalArguments.Comment,
                    FilePath = additionalArguments.ImageFilePath
                };
            }

            if (additionalArguments.ImageElement == null)
            {
                throw new ArgumentException("No image found in the arguments. Please provide one.");
            }
            
            return new ImageToTextRequest
            {
                BodyBase64 = additionalArguments.ImageElement.DownloadSourceAsBase64String(),           
                Phrase = additionalArguments.Phrase ?? false,
                Case = additionalArguments.Case ?? false,
                Numeric = additionalArguments.Numeric ?? NumericOption.NoRequirements,
                Math = additionalArguments.Math ?? false,
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