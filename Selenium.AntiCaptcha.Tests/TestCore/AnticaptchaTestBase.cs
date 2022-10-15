using AntiCaptchaApi.Net.Enums;
using AntiCaptchaApi.Net.Models;
using AntiCaptchaApi.Net.Models.Solutions;
using AntiCaptchaApi.Net.Responses;
using OpenQA.Selenium;

namespace Selenium.Anticaptcha.Tests.TestCore;

[Collection(TestEnvironment.DriverBasedTestCollection)]
public abstract class AnticaptchaTestBase
{
    private readonly WebDriverFixture _fixture;
    private static int _testInstanceCount;
    private readonly int _instanceNumber;

    protected readonly IWebDriver Driver; 
        

    protected AnticaptchaTestBase(WebDriverFixture fixture)
    {
        this._fixture = fixture;
        Driver = _fixture.Drivers[_testInstanceCount++ % WebDriverFixture.DriversCount];
    }


    protected readonly string ClientKey = TestEnvironment.ClientKey;

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