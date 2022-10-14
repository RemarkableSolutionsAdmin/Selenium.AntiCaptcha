using AntiCaptchaApi.Net.Enums;
using AntiCaptchaApi.Net.Models;
using AntiCaptchaApi.Net.Models.Solutions;
using AntiCaptchaApi.Net.Responses;
using OpenQA.Selenium;

namespace Selenium.Anticaptcha.Tests.TestCore;

[Collection(TestEnvironment.DriverBasedTestCollection)]
public abstract class AnticaptchaTestBase
{
    protected readonly WebDriverFixture Fixture;

    protected IWebDriver Driver => Fixture.Driver;

    protected AnticaptchaTestBase(WebDriverFixture fixture)
    {
        Fixture = fixture;
    }
    
    protected readonly string ClientKey = TestEnvironment.ClientKey;

    protected static void AssertSolveCaptchaResult<TSolution>(TaskResultResponse<TSolution>? result)
        where TSolution : BaseSolution, new()
    {
        Assert.NotNull(result);
        Assert.False(result.IsErrorResponse);
        Assert.NotNull(result.Solution);
        Assert.True(result.Solution.IsValid());
    }
}