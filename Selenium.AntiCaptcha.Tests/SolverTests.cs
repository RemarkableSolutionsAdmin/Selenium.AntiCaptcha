using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using Selenium.AntiCaptcha.enums;

namespace Selenium.AntiCaptcha
{
    //TODO: Change into real tests
    public static partial class SolverTests
    {
        private static void BinanceLogin()
        {
            using (var driver = new ChromeDriver())
            {
                driver.Url = "https://accounts.binance.com/pl/register-person";
                var nameEl = driver.FindElement(By.Name("email"));
                nameEl.SendKeys($"{RandText(10)}@{RandText(3)}.com");
                var passEl = driver.FindElement(By.Name("password"));
                passEl.SendKeys($"{RandText(10)}1!");
                var aggrToTerms = driver.FindElement(By.XPath("//label"));
                aggrToTerms.Click();
                driver.SolveCaptcha(Environment.GetEnvironmentVariable("ClientKey"), captchaType: CaptchaType.GeeTest);
            }
        }

        private static Random random = new Random();
        private static string RandText(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            return new string(Enumerable.Repeat(chars, length)
                .Select(s => s[random.Next(s.Length)]).ToArray());
        }

        public static void GeeTest()
        {
            
            using (var driver = new ChromeDriver(Environment.CurrentDirectory))
            {
                driver.Url = "https://www.geetest.com/en/adaptive-captcha-demo";

                var allButtonParents = driver.FindElements(By.XPath("//button/parent::*"));
                foreach (var buttonParent in allButtonParents)
                {
                    var buttonText = buttonParent.Text;

                    if (buttonText.Contains("Slide"))
                    {
                        buttonParent.Click();
                    }
                }
                
                
                
                driver.SolveCaptcha(Environment.GetEnvironmentVariable("ClientKey"), captchaType: CaptchaType.GeeTest);
            }
        }
        
        public static void ImageToTextTest()
        {
            using (var driver = new ChromeDriver())
            {
                driver.Url = "https://en.wikipedia.org/w/index.php?title=Special:CreateAccount&returnto=Main+Page";
                driver.SolveCaptcha(Environment.GetEnvironmentVariable("ClientKey"),
                    imageElement: driver.FindElement(By.ClassName("fancycaptcha-image")),
                    responseElement: driver.FindElement(By.Id("mw-input-captchaWord")),
                    captchaType: CaptchaType.ImageToText);
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

