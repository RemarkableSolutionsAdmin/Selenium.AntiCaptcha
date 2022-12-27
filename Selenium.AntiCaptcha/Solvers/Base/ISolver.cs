using AntiCaptchaApi.Net.Models.Solutions;
using AntiCaptchaApi.Net.Requests.Abstractions;
using AntiCaptchaApi.Net.Responses;
using OpenQA.Selenium;
using Selenium.AntiCaptcha.Models;

namespace Selenium.AntiCaptcha.Solvers.Base;


public interface ISolver<TRequest, TSolution> : ISolver
    where TRequest : CaptchaRequest<TSolution>
    where TSolution : BaseSolution, new()
{
    public Task<TaskResultResponse<TSolution>> SolveAsync(SolverArguments arguments, ActionArguments actionArguments,
        CancellationToken cancellationToken);
    public Task<TaskResultResponse<TSolution>> SolveAsync(TRequest request, ActionArguments actionArguments,
        CancellationToken cancellationToken);
    public void Configure(IWebDriver driver, string clientKey, SolverConfig solverConfig);
}

public interface ISolver { }
