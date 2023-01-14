using AntiCaptchaApi.Net;
using AntiCaptchaApi.Net.Enums;
using AntiCaptchaApi.Net.Models.Solutions;
using AntiCaptchaApi.Net.Requests.Abstractions.Interfaces;
using AntiCaptchaApi.Net.Responses;
using Newtonsoft.Json.Linq;
using OpenQA.Selenium;
using Selenium.AntiCaptcha.Models;
using Selenium.FramesSearcher.Extensions;

namespace Selenium.AntiCaptcha.Solvers.Base;

public abstract class Solver<TRequest, TSolution> : ISolver <TSolution>
    where TRequest: ICaptchaRequest<TSolution>
    where TSolution: BaseSolution, new()
{
    protected IWebDriver Driver;
    private AnticaptchaClient _anticaptchaClient;
    public SolverConfig SolverConfig { get; protected set; }

    protected Solver(string clientKey, IWebDriver driver, SolverConfig solverConfig)
    {
        Configure(driver, clientKey, solverConfig);
    }

    protected virtual string GetSiteKey()
    {   
        var pageSource = Driver.GetAllPageSource();

        var patterns = new List<string>
        {
            @"sitekey=""?([\w\d-]+)""?",
            "gt=(.*?)&",
            "captcha_id=(.*?)&",
        };

        var result = pageSource.GetFirstRegexThatFits(true, patterns.ToArray());
        return result != null ? result.Groups[1].Value : string.Empty;
    }

    protected async Task<string> AcquireSiteKey()
    {
        var timePassedInMs = 0;
        while (true)
        {
            var result = GetSiteKey();

            if (!string.IsNullOrEmpty(result) || timePassedInMs >= SolverConfig.MaxPageLoadWaitingTimeInMilliseconds) 
                return result;

            await Task.Delay(SolverConfig.WaitingStepTimeInMilliseconds);
            timePassedInMs += SolverConfig.WaitingStepTimeInMilliseconds;
        }
    }


    protected abstract TRequest BuildRequest(SolverArguments arguments);

    protected virtual async Task FillResponseElement(TSolution solution, IWebElement? responseElement)
    {
        
    }

    protected virtual async Task<SolverArguments> FillMissingSolverArguments(SolverArguments solverArguments)
    {
        var websiteKey = string.IsNullOrEmpty(solverArguments.WebsiteKey) ? await AcquireSiteKey() : null;
        return solverArguments with
        {
            WebsiteUrl = solverArguments.WebsiteUrl ?? Driver.Url,
            WebsiteKey = websiteKey,
            WebsitePublicKey = solverArguments.WebsitePublicKey ?? websiteKey,
            UserAgent = solverArguments.UserAgent
        };
    }
    
    public async Task<TaskResultResponse<TSolution>> SolveAsync(SolverArguments arguments,
        ActionArguments actionArguments,
        CancellationToken cancellationToken = default)
    {
        arguments = await FillMissingSolverArguments(arguments);
        var request = BuildRequest(arguments);
        return await SolveCaptchaAsync(request, actionArguments, cancellationToken);
    }

    private async Task<TaskResultResponse<TSolution>> SolveCaptchaAsync(TRequest request, ActionArguments actionArguments, CancellationToken cancellationToken)
    {

        var result = await _anticaptchaClient
            .SolveCaptchaAsync(request, maxSeconds: SolverConfig.MaxTimeOutTimeInMilliseconds, cancellationToken: cancellationToken);
        if (result.Status == TaskStatusType.Ready && result.Solution.IsValid())
        {
            var cookies = (result.Solution as AntiGateSolution)?.Cookies;
            if (cookies != null)
                AddCookies(Driver, cookies, actionArguments.ShouldResetCookiesBeforeAdd);

            await FillResponseElement(result.Solution, actionArguments.ResponseElement);
            actionArguments.SubmitElement?.Click();
        }

        return result;
    }
    


    public async Task<TaskResultResponse<TSolution>> SolveAsync(TRequest request, ActionArguments actionArguments, CancellationToken cancellationToken)
    {
        return await SolveCaptchaAsync(request, actionArguments, cancellationToken);
    }

    public void Configure(IWebDriver driver, string clientKey, SolverConfig solverConfig)
    {
        Driver = driver;
        _anticaptchaClient = new AnticaptchaClient(clientKey);
        SolverConfig = solverConfig;
    }

    private static void AddCookies(IWebDriver driver, JObject? cookies, bool shouldClearCookies)
    {
        if (cookies is not { Count: > 0 }) 
            return;
        
        if (shouldClearCookies)
        {
            driver.Manage().Cookies.DeleteAllCookies();
        }
        foreach (var cookie in cookies)
        {
            if (!string.IsNullOrEmpty(cookie.Key) && !string.IsNullOrEmpty(cookie.Value?.ToString()))
                driver.Manage().Cookies.AddCookie(new Cookie(cookie.Key, cookie.Value.ToString()));
        }
    }
}