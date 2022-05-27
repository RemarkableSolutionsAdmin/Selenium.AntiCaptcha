using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using RemarkableSolutions.Selenium.AntiCaptcha.enums;

namespace RemarkableSolutions.Selenium.AntiCaptcha
{
    public static partial class Program
    {
        public static void Main(string[] args)
        {
            using (var driver = new ChromeDriver())
            {
                driver.Url = "http://http.myjino.ru/funcaptcha_test/";
                driver.SolveCaptcha(Environment.GetEnvironmentVariable("ClientKey"), captchaType: CaptchaType.FunCaptcha, submitElement: driver.FindElement(By.ClassName("btn")));
            }
        }

        public static void ReCaptchaV2()
        {
            using (var driver = new ChromeDriver())
            {
                driver.Url = "http://antigate.com/logintest.php";
                driver.SolveCaptcha(Environment.GetEnvironmentVariable("ClientKey"), captchaType: CaptchaType.ReCaptchaV2, submitElement: driver.FindElement(By.ClassName("btn")));
            }
        }

        public static void HCaptcha()
        {
            using (var driver = new ChromeDriver())
            {
                driver.Url = "https://democaptcha.com/demo-form-eng/hcaptcha.html";
                driver.SolveCaptcha(Environment.GetEnvironmentVariable("ClientKey"), captchaType: CaptchaType.HCaptcha, submitElement: driver.FindElement(By.ClassName("btn")));
            }
        }
    }
}

