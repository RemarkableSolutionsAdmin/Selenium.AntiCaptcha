namespace Selenium.AntiCaptcha.Models;

public class DefaultSolverConfig : SolverConfig
{
    public DefaultSolverConfig(
        int pageLoadTimeoutMs = 30000,
        int delayTimeBetweenElementValueRetrievalMs = 2000,
        int maxWaitForTaskResultTimeMs = 120000,
        int maxHttpRequestTimeMs = 60000, 
        int solveAsyncRetries = 1,
        int delayTimeBetweenCheckingTaskResultMs = 2000)
        : base(pageLoadTimeoutMs,
            delayTimeBetweenElementValueRetrievalMs,
            maxWaitForTaskResultTimeMs,
            maxHttpRequestTimeMs,
            solveAsyncRetries,
            delayTimeBetweenCheckingTaskResultMs)
    {
        
    }
}