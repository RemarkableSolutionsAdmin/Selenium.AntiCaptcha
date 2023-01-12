using AntiCaptchaApi.Net.Models.Solutions;
using AntiCaptchaApi.Net.Responses;
using AntiCaptchaApi.Net.Responses.Abstractions;
using OpenQA.Selenium;
using Selenium.AntiCaptcha.Enums;
using Selenium.AntiCaptcha.Internal.Extensions;
using Selenium.Anticaptcha.Tests.Core.Config;
using Xunit.Sdk;

namespace Selenium.Anticaptcha.Tests.Core.SolverTestBases;
public abstract class AnticaptchaTestBase : IClassFixture<WebDriverFixture>, IDisposable
{
    private readonly WebDriverFixture _fixture;
    protected IWebDriver Driver => _fixture.Driver;
    private const int MaxWaitingTimeInMilliseconds = 5000;
    private const int StepDelayTimeInMilliseconds = 500;
    private const string ResetWebsiteUri = "https://www.google.com/";
    
    protected AnticaptchaTestBase(WebDriverFixture fixture)
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
    
    protected static void AssertSolveCaptchaResult<TSolution>(TaskResultResponse<TSolution>? result, CaptchaType expectedCaptchaType)
        where TSolution : BaseSolution, new()
    {
        Assert.NotNull(result);
        if (!string.IsNullOrEmpty(result!.ErrorDescription))
        {
            Fail($"{result.ErrorDescription}{Environment.NewLine}------------Request Payload----------{Environment.NewLine}" +
                 $"{result.RawRequestPayload}{Environment.NewLine}------------Response Payload----------{Environment.NewLine}" +
                 $"{result.RawResponse}{Environment.NewLine}{Environment.NewLine}");
        }

        if (!string.IsNullOrEmpty(result!.CreateTaskResponse.ErrorDescription))
        {
            Assert.Empty(result.CreateTaskResponse.ErrorDescription);
            Assert.NotNull(result.CreateTaskResponse.ErrorDescription);
        }
        
        Assert.False(result.IsErrorResponse);
        Assert.NotNull(result.Solution);
        Assert.True(result.Solution.IsValid());
        var expectedCaptchaTypeText = expectedCaptchaType.ToString();
        //TODO! There's task in the name. so it does not find it.
        // Assert.Contains($"\"{expectedCaptchaType.ToString()}\"", result.CreateTaskResponse.RawRequestPayload);
        
    }

    protected static void AssertSolveCaptchaResult(BaseResponse result, CaptchaType expectedCaptchaType)
    {
        if (result is TaskResultResponse<RecaptchaSolution>)
            AssertSolveCaptchaResult((TaskResultResponse<RecaptchaSolution>?)result, expectedCaptchaType);
        if (result is TaskResultResponse<HCaptchaSolution>)
            AssertSolveCaptchaResult((TaskResultResponse<HCaptchaSolution>?)result, expectedCaptchaType);
        if (result is TaskResultResponse<FunCaptchaSolution>)
            AssertSolveCaptchaResult((TaskResultResponse<FunCaptchaSolution>?)result, expectedCaptchaType);
        if (result is TaskResultResponse<GeeTestV3Solution>)
            AssertSolveCaptchaResult((TaskResultResponse<GeeTestV3Solution>?)result, expectedCaptchaType);
        if (result is TaskResultResponse<GeeTestV4Solution>)
            AssertSolveCaptchaResult((TaskResultResponse<GeeTestV4Solution>?)result, expectedCaptchaType);
        if (result is TaskResultResponse<AntiGateSolution>)
            AssertSolveCaptchaResult((TaskResultResponse<AntiGateSolution>?)result, expectedCaptchaType);
        if (result is TaskResultResponse<ImageToTextSolution>)
            AssertSolveCaptchaResult((TaskResultResponse<ImageToTextSolution>?)result, expectedCaptchaType);
        if (result is TaskResultResponse<TurnstileSolution>)
            AssertSolveCaptchaResult((TaskResultResponse<TurnstileSolution>?)result, expectedCaptchaType);
    }
}