using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace Selenium.Anticaptcha.Tests.TestCore;


public class WebDriverFixture : IDisposable
{
    public const int DriversCount = 3;
    public IWebDriver Driver { get; private set; }
    public TimeSpan DefaultWait { get; set; } = TimeSpan.FromMilliseconds(1000);
    public TimeSpan PollingInterval { get; set; } = TimeSpan.FromMilliseconds(100);

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