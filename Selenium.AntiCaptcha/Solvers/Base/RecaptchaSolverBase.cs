using System.Text.RegularExpressions;
using AntiCaptchaApi.Net.Models.Solutions;
using AntiCaptchaApi.Net.Requests.Abstractions;
using OpenQA.Selenium;
using Selenium.AntiCaptcha.Internal.Extensions;

namespace Selenium.AntiCaptcha.Solvers.Base;

internal abstract class RecaptchaSolverBase<TRequest, TSolution> : Solver<TRequest, TSolution>
    where TRequest : CaptchaRequest<TSolution>
    where TSolution : RecaptchaSolution, new()
{
    protected override string GetSiteKey(IWebDriver driver, int waitingTime = 1000, int tries = 3)
    {
        Thread.Sleep(waitingTime);

        try
        {
            return driver.FindElement(By.ClassName("g-recaptcha")).GetAttribute("data-sitekey");
        }
        catch (Exception e)
        {
            
        }

        var recaptchaFrameSrc = driver.FindByXPath("//iframe[contains(@src, 'recaptcha')]")?.GetAttribute("src");


        if (!string.IsNullOrEmpty(recaptchaFrameSrc))
        {
            var regex = new Regex("k=(.*?)&");
            var siteKey = regex.Match(driver.GetAllPageSource()).Groups[1].Value;

            if (!string.IsNullOrEmpty(siteKey))
                return siteKey;
        }

        return string.Empty;
    }
    
    
    protected override void FillResponseElement(IWebDriver driver, TSolution solution, IWebElement? responseElement)
    {
        if (responseElement != null)
        {
            responseElement.SendKeys(solution.GRecaptchaResponse);
        }
        else
        {
            var js = driver as IJavaScriptExecutor;
            js.ExecuteScript($"window.localStorage.setItem('_grecaptcha','{solution.GRecaptchaResponse}');");
            js.ExecuteScript($"document.getElementById('g-recaptcha-response').innerText='{solution.GRecaptchaResponse}';");
        }
    }
}