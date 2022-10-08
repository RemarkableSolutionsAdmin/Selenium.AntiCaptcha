using System;
using OpenQA.Selenium;
using Selenium.AntiCaptcha.enums;

namespace Selenium.AntiCaptcha
{
    public static class IWebDriverExtensions
    {
        /// <summary>
        /// Solves captcha found on website. To get clientKey sign-up on anti-captcha.com
        /// </summary>
        /// <param name="driver"></param>
        /// <param name="clientKey"></param>
        /// <param name="captchaType"></param>
        /// <param name="url"></param>
        /// <param name="siteKey"></param>
        /// <param name="submitElement"></param>
        /// <param name="imageElement"></param>
        public static void SolveCaptcha(
            this IWebDriver driver, 
            string clientKey, 
            CaptchaType? captchaType = null, 
            string? url = null, 
            string? siteKey = null, 
            IWebElement? responseElement = null, 
            IWebElement? submitElement = null,
            IWebElement? imageElement = null)
        {
            if (captchaType == null)
            {
                captchaType = IdentifyCaptcha(driver);
            }

            var solver = SolverFactory.GetSolver(captchaType.Value);
            solver.Solve(driver, clientKey, url, siteKey, responseElement, submitElement, imageElement);
        }

        private static CaptchaType? IdentifyCaptcha(IWebDriver driver)
        {
            //TODO: identify captcha type
            throw new NotImplementedException();
        }
    }
}