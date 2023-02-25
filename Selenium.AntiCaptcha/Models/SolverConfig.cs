using AntiCaptchaApi.Net.Models;

namespace Selenium.AntiCaptcha.Models;

public class SolverConfig : ClientConfig
{
    public SolverConfig(int pageLoadTimeoutLimitInMilliseconds, int timeoutLimitInSeconds, int solveAsyncMaxRetries, int stepDelayTimeInMilliseconds)
    {
        PageLoadTimeoutLimitInMilliseconds = pageLoadTimeoutLimitInMilliseconds;
        MaxWaitingTimeInSeconds = timeoutLimitInSeconds;
        SolveAsyncMaxRetries = solveAsyncMaxRetries;
        StepWaitingTimeInMilliseconds = stepDelayTimeInMilliseconds;
    }
    public int PageLoadTimeoutLimitInMilliseconds { get; set; }
}