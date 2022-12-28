using System.Text.RegularExpressions;
using AntiCaptchaApi.Net.Models.Solutions;
using AntiCaptchaApi.Net.Requests.Abstractions;
using AntiCaptchaApi.Net.Requests.Abstractions.Interfaces;
using OpenQA.Selenium;
using Selenium.AntiCaptcha.Internal.Extensions;
using Selenium.AntiCaptcha.Models;

namespace Selenium.AntiCaptcha.Solvers.Base;

internal abstract class RecaptchaSolverBase<TRequest> : Solver <TRequest, RecaptchaSolution>
    where TRequest : ICaptchaRequest<RecaptchaSolution>
{
    protected override string GetSiteKey()
    {
        try
        {
            return Driver.FindElement(By.ClassName("g-recaptcha")).GetAttribute("data-sitekey");
        }
        catch (Exception e)
        {
            // ignored
        }

        
        var recaptchaFrameSrc = Driver.FindByXPathAllFrames("//iframe[contains(@src, 'recaptcha')]")?.GetAttribute("src");


        if (!string.IsNullOrEmpty(recaptchaFrameSrc))
        {
            var regex = new Regex("k=(.*?)&");
            var siteKey = regex.Match(Driver.GetAllPageSource()).Groups[1].Value;

            if (!string.IsNullOrEmpty(siteKey))
                return siteKey;
        }

        return string.Empty;
    }
    
    
    protected override void FillResponseElement(RecaptchaSolution solution, IWebElement? responseElement)
    {
        if (responseElement != null)
        {
            responseElement.SendKeys(solution.GRecaptchaResponse);
        }
        else
        {
            var js = Driver as IJavaScriptExecutor;
            js.ExecuteScript($"window.localStorage.setItem('_grecaptcha','{solution.GRecaptchaResponse}');");
            js.ExecuteScript($"document.getElementById('g-recaptcha-response').innerText='{solution.GRecaptchaResponse}';");
        }
    }

    protected RecaptchaSolverBase(string clientKey, IWebDriver driver, SolverConfig solverConfig) : base(clientKey, driver, solverConfig)
    {
    }
}