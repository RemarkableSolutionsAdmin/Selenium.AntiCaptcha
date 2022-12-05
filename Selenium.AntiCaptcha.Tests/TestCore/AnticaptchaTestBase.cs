﻿using AntiCaptchaApi.Net.Models.Solutions;
using AntiCaptchaApi.Net.Responses;
using AntiCaptchaApi.Net.Responses.Abstractions;
using OpenQA.Selenium;
using Selenium.AntiCaptcha.Internal.Extensions;

namespace Selenium.Anticaptcha.Tests.TestCore;
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
    
    protected static void AssertSolveCaptchaResult<TSolution>(TaskResultResponse<TSolution>? result)
        where TSolution : BaseSolution, new()
    {
        Assert.NotNull(result);
        if (!string.IsNullOrEmpty(result.ErrorDescription))
        {
            Assert.False(true, result?.ErrorDescription);
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