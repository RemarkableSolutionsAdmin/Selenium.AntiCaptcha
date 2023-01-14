using OpenQA.Selenium;
using Selenium.FramesSearcher.Extensions;
using Tests.Common.Config;
using Tests.Common.Core;
using Xunit.Sdk;

namespace Tests.Common;
public abstract class WebDriverBasedTestBase : IClassFixture<WebDriverFixture>, IDisposable
{
    private readonly WebDriverFixture _fixture;
    protected IWebDriver Driver => _fixture.Driver;
    private const int MaxWaitingTimeInMilliseconds = 5000;
    private const int StepDelayTimeInMilliseconds = 500;
    private const string ResetWebsiteUri = "https://www.google.com/";
    
    protected WebDriverBasedTestBase(WebDriverFixture fixture)
    {
        _fixture = fixture;
    }
    
    protected readonly string ClientKey = TestEnvironment.ClientKey;
    
    public virtual void Dispose()
    {
        //_fixture?.Dispose();
    }
    
    private async Task WaitForLoad()
    {
        var timeElapsedInMilliseconds = 0;
        while (true)
        {
            if (IsLoaded(timeElapsedInMilliseconds))
            {
                break;
            }

            await Task.Delay(StepDelayTimeInMilliseconds);
            timeElapsedInMilliseconds += StepDelayTimeInMilliseconds;
        }
    }

    private bool IsLoaded(int timeElapsedInMilliseconds)
    {
        var isResetPage = Driver.Url == ResetWebsiteUri;
        return timeElapsedInMilliseconds > MaxWaitingTimeInMilliseconds ||
               (isResetPage ?
                   Driver.FindByXPathAllFrames("//img[contains(@alt, 'oogle')]") != null : //TODO: better xpaths.
                   Driver.FindByXPathAllFrames("//*[contains(@class, 'captcha')]") != null
               );
    }

    protected async Task ResetDriverUri()
    {
        await SetDriverUrl(ResetWebsiteUri);
    }

    protected async Task SetDriverUrl(string url)
    {
        if (url == Driver.Url)
        {
            Driver.Manage().Cookies.DeleteAllCookies();
            Driver.Navigate().Refresh();
        }
        else
        {
            Driver.Navigate().GoToUrl(url);   
        }
        await WaitForLoad();
    }

    protected static void Fail(string message)
    {
        throw new XunitException(message);
    }
}