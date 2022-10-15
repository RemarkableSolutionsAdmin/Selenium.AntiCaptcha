using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace Selenium.Anticaptcha.Tests.TestCore;


public class WebDriverFixture : IDisposable
{
    public const int DriversCount = 3;
    public List<IWebDriver> Drivers { get; } = new();

    public WebDriverFixture()
    {
        Drivers.AddRange(Enumerable.Repeat(new ChromeDriver(Environment.CurrentDirectory), DriversCount));
    }

    public void Dispose()
    {
        foreach (var driver in Drivers)
        {
            driver?.Dispose();
        }
    }
}