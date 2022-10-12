using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace Selenium.AntiCaptcha.Tests;

public class BinanceLoginTests
{
    [Fact]
    public static void BinanceLogin()
    {
        //Does not work yet.
        return;
        using (var driver = new ChromeDriver())
        {
            driver.Url = "https://accounts.binance.com/pl/register-person";
            var nameEl = driver.FindElement(By.Name("email"));
            nameEl.SendKeys($"{RandText(10)}@{RandText(3)}.com");
            var passEl = driver.FindElement(By.Name("password"));
            passEl.SendKeys($"{RandText(10)}1!");
            var aggrToTerms = driver.FindElement(By.XPath("//label"));
            aggrToTerms.Click();
            //driver.SolveCaptcha<RawSolution>(Environment.GetEnvironmentVariable("ClientKey"), captchaType: CaptchaType.GeeTestV3);
        }
    }
    
    private static Random random = new Random();
    private static string RandText(int length)
    {
        const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
        return new string(Enumerable.Repeat(chars, length)
            .Select(s => s[random.Next(s.Length)]).ToArray());
    }
}