using AntiCaptchaApi.Net.Enums;
using AntiCaptchaApi.Net.Models;
using AntiCaptchaApi.Net.Models.Solutions;
using AntiCaptchaApi.Net.Responses;
using OpenQA.Selenium;

namespace Selenium.Anticaptcha.Tests.TestCore;

[Collection(TestEnvironment.DriverBasedTestCollection)]
public abstract class AnticaptchaTestBase
{
    protected WebDriverFixture fixture;

    protected IWebDriver Driver => fixture.Driver;

    public AnticaptchaTestBase(WebDriverFixture fixture)
    {
        this.fixture = fixture;
    }
    
    protected string ClientKey = TestEnvironment.ClientKey;

    protected void AssertSolveCaptchaResult<TSolution>(TaskResultResponse<TSolution>? result)
        where TSolution : BaseSolution, new()
    {
        Assert.NotNull(result);
        Assert.False(result.IsErrorResponse);
        Assert.NotNull(result.Solution);
        Assert.True(result.Solution.IsValid());
    }
}