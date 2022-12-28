using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace Selenium.Anticaptcha.Tests.Core;


public class WebDriverFixture : IDisposable
{
    public IWebDriver Driver { get; private set; }

    public WebDriverFixture()
    {
        RecreateWebDriver();
    }

    public void RecreateWebDriver()
    {
        if (Driver != null)
        {
            Driver.Close();
            Driver.Dispose();
        }
        Driver = new ChromeDriver(Environment.CurrentDirectory);
    }

    public void Dispose()
    {
        Driver?.Dispose();
    }
}