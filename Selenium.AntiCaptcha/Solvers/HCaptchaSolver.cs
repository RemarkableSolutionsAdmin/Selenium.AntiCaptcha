using OpenQA.Selenium;
using AntiCaptchaApi.Net;
using AntiCaptchaApi.Net.Enums;
using AntiCaptchaApi.Net.Models;
using AntiCaptchaApi.Net.Models.Solutions;
using AntiCaptchaApi.Net.Requests;
using AntiCaptchaApi.Net.Responses;
using Selenium.AntiCaptcha.Constants;

namespace Selenium.AntiCaptcha.solvers
{
    internal class HCaptchaSolver : Solver<HCaptchaSolution>
    {
        protected override void FillResponseElement(IWebDriver driver, HCaptchaSolution solution, IWebElement? responseElement)
        {
            if (responseElement == null)
            {
                responseElement = driver.FindElement(By.Name("h-captcha-response"));
            }

            responseElement.SendKeys(solution.GRecaptchaResponse);
        }

        protected override string GetSiteKey(IWebDriver driver, int waitingTime = 1000) => driver.FindElement(By.ClassName("h-captcha")).GetAttribute("data-sitekey");

        internal override TaskResultResponse<HCaptchaSolution> Solve(IWebDriver driver, string clientKey, string? url, string? siteKey,
            IWebElement? responseElement, IWebElement? submitElement, IWebElement? imageElement, string? userAgent, ProxyConfig proxyConfig)
        {
            var client = new AnticaptchaClient(clientKey);
            siteKey ??= GetSiteKey(driver);

            var captchaRequest = new HCaptchaRequest()
            {
                WebsiteUrl = url ?? driver.Url,
                WebsiteKey = siteKey,
                UserAgent = userAgent ?? AnticaptchaDefaultValues.UserAgent,
                ProxyConfig = proxyConfig
            };

            var result = client.SolveCaptcha<HCaptchaProxylessRequest, HCaptchaSolution>(captchaRequest);

            if (result.Status == TaskStatusType.Ready)
            {
                FillResponseElement(driver, result.Solution, responseElement);
            }

            submitElement?.Click();
            return result;
        }
    }
}
