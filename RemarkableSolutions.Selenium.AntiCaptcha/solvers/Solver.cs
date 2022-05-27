using OpenQA.Selenium;
using RemarkableSolutions.Anticaptcha.Api.Responses;

namespace RemarkableSolutions.Selenium.AntiCaptcha.solvers
{
    internal abstract class Solver
    {
        protected abstract string GetSiteKey(IWebDriver driver);
        protected abstract void FillResponseElement(IWebDriver driver, TaskResultResponse.SolutionData solution, IWebElement? responseElement);
        internal abstract void Solve(IWebDriver driver, string clientKey, string? url, string? siteKey, IWebElement? responseElement, IWebElement? submitElement);
        protected static void AddCookies(IWebDriver driver, TaskResultResponse.SolutionData solution)
        {
            if (solution?.Cookies != null && solution.Cookies.Count > 0)
            {
                driver.Manage().Cookies.DeleteAllCookies();
                foreach (var cookie in solution.Cookies)
                {
                    if (!string.IsNullOrEmpty(cookie.Key) && !string.IsNullOrEmpty(cookie.Value?.ToString()))
                        driver.Manage().Cookies.AddCookie(new Cookie(cookie.Key, cookie.Value.ToString()));
                }
            }
        }
    }
}
