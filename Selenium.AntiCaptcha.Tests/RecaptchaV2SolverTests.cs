using AntiCaptchaApi.Net.Models.Solutions;
using AntiCaptchaApi.Net.Requests;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using Selenium.AntiCaptcha.enums;
using Xunit;

namespace Selenium.AntiCaptcha.Tests
{
    public class RecaptchaV2SolverTests : SolverTestsBase
    {
        private const string Uri = "http://antigate.com/logintest.php";
        [Fact]
        public void ReCaptchaV2WithCaptchaTypeSpecified()
        {
            using (var driver = new ChromeDriver())
            {
                driver.Url = "http://antigate.com/logintest.php";
                var result = driver.SolveCaptcha<RecaptchaSolution>(ClientKey, submitElement: driver.FindElement(By.ClassName("btn")));
                AssertSolveCaptchaResult(result);
            }
        }
        
        [Fact]
        public void ReCaptchaV2WithoutCaptchaTypeSpecified()
        {
            using (var driver = new ChromeDriver())
            {
                driver.Url = "http://antigate.com/logintest.php";
                var result = driver.SolveCaptcha<RecaptchaSolution>(ClientKey, submitElement: driver.FindElement(By.ClassName("btn")));
                AssertSolveCaptchaResult(result);
            }
        }
    }
}

