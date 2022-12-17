using AntiCaptchaApi.Net.Models.Solutions;
using AntiCaptchaApi.Net.Responses;
using OpenQA.Selenium;
using Selenium.AntiCaptcha.Models;

namespace Selenium.AntiCaptcha.Solvers.Base;


public interface ISolver<TSolution> : ISolver
    where TSolution : BaseSolution, new()
{
    public Task<TaskResultResponse<TSolution>> SolveAsync(SolverAdditionalArguments additionalArguments, CancellationToken cancellationToken);
    public void Reconfigure(IWebDriver driver, string clientKey);
}

public interface ISolver { }
