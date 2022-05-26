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
                driver.Url = "http://antigate.com/logintest.php";
                driver.SolveCaptcha(Environment.GetEnvironmentVariable("ClientKey"), captchaType: CaptchaType.ReCaptchaV2, submitElement: driver.FindElement(By.ClassName("btn")));
                Console.Read();
            }
        }
    }
}

