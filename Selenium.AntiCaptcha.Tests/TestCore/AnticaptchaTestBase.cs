using AntiCaptchaApi.Net.Enums;
using AntiCaptchaApi.Net.Models;
using AntiCaptchaApi.Net.Models.Solutions;
using AntiCaptchaApi.Net.Responses;
using AntiCaptchaApi.Net.Responses.Abstractions;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using Selenium.AntiCaptcha.Internal.Extensions;

namespace Selenium.Anticaptcha.Tests.TestCore;
public abstract class AnticaptchaTestBase : IClassFixture<WebDriverFixture>
{
    private readonly WebDriverFixture _fixture;
    protected readonly IWebDriver Driver;
    private const int MaxWaitingTimeInMilliseconds = 5000;
    private const int StepDelayTimeInMilliseconds = 500;


    protected AnticaptchaTestBase(WebDriverFixture fixture)
    {
        this._fixture = fixture;
        Driver = _fixture.Driver;
    }


    protected readonly string ClientKey = TestEnvironment.ClientKey;

    private async Task WaitForLoad()
    {
        var timeElapsedInMilliseconds = 0;
        while (true)
        {
            if (timeElapsedInMilliseconds > MaxWaitingTimeInMilliseconds ||
                Driver.FindByXPathAllFrames("//*[contains(@class, 'captcha')]") != null)
            {
                break;
            }

            await Task.Delay(StepDelayTimeInMilliseconds);
            timeElapsedInMilliseconds += StepDelayTimeInMilliseconds;
        }
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

    protected static void AssertSolveCaptchaResult(BaseResponse result)
    {
        if (result is TaskResultResponse<RecaptchaSolution>)
            AssertSolveCaptchaResult((TaskResultResponse<RecaptchaSolution>?)result);
        if (result is TaskResultResponse<HCaptchaSolution>)
            AssertSolveCaptchaResult((TaskResultResponse<HCaptchaSolution>?)result);
        if (result is TaskResultResponse<FunCaptchaSolution>)
            AssertSolveCaptchaResult((TaskResultResponse<FunCaptchaSolution>?)result);
        if (result is TaskResultResponse<GeeTestV3Solution>)
            AssertSolveCaptchaResult((TaskResultResponse<GeeTestV3Solution>?)result);
        if (result is TaskResultResponse<GeeTestV4Solution>)
            AssertSolveCaptchaResult((TaskResultResponse<GeeTestV4Solution>?)result);
        if (result is TaskResultResponse<AntiGateSolution>)
            AssertSolveCaptchaResult((TaskResultResponse<AntiGateSolution>?)result);
        if (result is TaskResultResponse<ImageToTextSolution>)
            AssertSolveCaptchaResult((TaskResultResponse<ImageToTextSolution>?)result);
    }
}