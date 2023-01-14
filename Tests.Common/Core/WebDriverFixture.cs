using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace Tests.Common.Core;


public class WebDriverFixture : IDisposable
{
    public IWebDriver Driver { get; private set; }

    public WebDriverFixture()
    {
        RecreateWebDriver();
    }

    public void RecreateWebDriver()
    {
        ChromeDriverKiller.KillAllPreviousChromeDriversIfTheyExist();
        if (Driver != null)
        {
            Driver.Close();
            Driver.Dispose();
        }
        var options = new ChromeOptions();

        Driver = new ChromeDriver(Environment.CurrentDirectory, options);
    }

    public void Dispose()
    {
        Driver?.Dispose();
    }
}