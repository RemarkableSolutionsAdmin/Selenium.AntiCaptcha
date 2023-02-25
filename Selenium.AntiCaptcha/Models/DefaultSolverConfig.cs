namespace Selenium.AntiCaptcha.Models;

public class DefaultSolverConfig : SolverConfig
{
    public DefaultSolverConfig(
        int pageLoadTimeoutLimitInMilliseconds = 30000,
        int timeoutLimitInSeconds = 120, 
        int solveAsyncMaxRetries = 1,
        int stepDelayTimeInMilliseconds = 2000)
        : base(pageLoadTimeoutLimitInMilliseconds, timeoutLimitInSeconds, solveAsyncMaxRetries, stepDelayTimeInMilliseconds)
    {
        
    }
}