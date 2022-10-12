using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace Selenium.Anticaptcha.Tests.TestCore;


public class WebDriverFixture : IDisposable
{
    public IWebDriver Driver { get; }

    public WebDriverFixture()
    {
        Driver = new ChromeDriver(Environment.CurrentDirectory);
    }

    public void Dispose()
    {
        Driver.Dispose();
    }
}