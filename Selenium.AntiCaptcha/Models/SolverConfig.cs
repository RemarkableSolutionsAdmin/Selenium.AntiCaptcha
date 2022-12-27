namespace Selenium.AntiCaptcha.Models;

public record SolverConfig(
    int MaxTimeOutTimeInMilliseconds, 
    int MaxPageLoadWaitingTimeInMilliseconds, 
    int WaitingStepTimeInMilliseconds);