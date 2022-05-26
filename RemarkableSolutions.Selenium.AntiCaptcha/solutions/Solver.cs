using OpenQA.Selenium;
using RemarkableSolutions.Anticaptcha.Api.Responses;

namespace RemarkableSolutions.Selenium.AntiCaptcha.solutions
{
    internal abstract class Solver
    {
        protected abstract string GetSiteKey(IWebDriver driver);
        internal abstract void Solve(IWebDriver driver, string clientKey, string? url, string? siteKey, IWebElement? submitElement);
        protected static void AddCookies(IWebDriver driver, TaskResultResponse.SolutionData solution)
        {
            driver.Manage().Cookies.DeleteAllCookies();
            if (solution.Cookies.Count > 0)
            {
                foreach (var cookie in solution.Cookies)
                {
                    if (!string.IsNullOrEmpty(cookie.Key) && !string.IsNullOrEmpty(cookie.Value?.ToString()))
                        driver.Manage().Cookies.AddCookie(new Cookie(cookie.Key, cookie.Value.ToString()));
                }
            }
        }
    }
}
