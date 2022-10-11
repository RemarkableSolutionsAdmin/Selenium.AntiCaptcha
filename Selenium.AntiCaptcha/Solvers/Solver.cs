using AntiCaptchaApi.Net.Models;
using OpenQA.Selenium;
using AntiCaptchaApi.Net.Models.Solutions;
using AntiCaptchaApi.Net.Responses;
using Newtonsoft.Json.Linq;

namespace Selenium.AntiCaptcha.solvers;



internal abstract class Solver<TSolution> where TSolution: BaseSolution, new()
{
    protected abstract string GetSiteKey(IWebDriver driver, int waitingTime = 1000);

    protected abstract void FillResponseElement(IWebDriver driver, TSolution solution, IWebElement? responseElement);

    internal abstract TaskResultResponse<TSolution> Solve(IWebDriver driver, string clientKey, string? url, string? siteKey,
        IWebElement? responseElement,
        IWebElement? submitElement, IWebElement? imageElement, string? userAgent, ProxyConfig proxyConfig);

    protected static void AddCookies(IWebDriver driver, JObject Cookies)
    {
        if (Cookies != null && Cookies.Count > 0)
        {
            driver.Manage().Cookies.DeleteAllCookies();
            foreach (var cookie in Cookies)
            {
                if (!string.IsNullOrEmpty(cookie.Key) && !string.IsNullOrEmpty(cookie.Value?.ToString()))
                    driver.Manage().Cookies.AddCookie(new Cookie(cookie.Key, cookie.Value.ToString()));
            }
        }
    }
}