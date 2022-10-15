using AntiCaptchaApi.Net.Enums;
using AntiCaptchaApi.Net.Models;
using AntiCaptchaApi.Net.Models.Solutions;
using AntiCaptchaApi.Net.Responses;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace Selenium.Anticaptcha.Tests.TestCore;
public abstract class AnticaptchaTestBase : IClassFixture<WebDriverFixture>
{
    private readonly WebDriverFixture _fixture;
    private static int _testInstanceCount;
    private readonly int _instanceNumber;

    protected readonly IWebDriver Driver; 
        

    protected AnticaptchaTestBase(WebDriverFixture fixture)
    {
        this._fixture = fixture;
        Driver = _fixture.Driver;
    }


    protected readonly string ClientKey = TestEnvironment.ClientKey;

    protected void SetDriverUrl(string url)
    {
        try
        {
            Driver.Navigate().GoToUrl(url);
        }
        catch (WebDriverException e)
        {
            _fixture.RecreateWebDriver();
            Driver.Url = url;
        };
    }
    
    protected static void AssertSolveCaptchaResult<TSolution>(TaskResultResponse<TSolution>? result)
        where TSolution : BaseSolution, new()
    {
        Assert.NotNull(result);
        if (!string.IsNullOrEmpty(result.ErrorDescription))
        {
            Assert.Empty(result.ErrorDescription);
            Assert.NotNull(result.ErrorDescription);   
        }

        if (!string.IsNullOrEmpty(result.CreateTaskResponse.ErrorDescription))
        {
            Assert.Empty(result.CreateTaskResponse.ErrorDescription);
            Assert.NotNull(result.CreateTaskResponse.ErrorDescription);
        }
        
        Assert.False(result.IsErrorResponse);
        Assert.NotNull(result.Solution);
        Assert.True(result.Solution.IsValid());
    }
}