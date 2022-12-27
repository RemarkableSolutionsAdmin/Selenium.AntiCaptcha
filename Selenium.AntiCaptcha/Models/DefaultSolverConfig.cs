namespace Selenium.AntiCaptcha.Models;

public record DefaultSolverConfig(
    int MaxTimeOutTimeInMilliseconds = 5000, 
    int MaxPageLoadWaitingTimeInMilliseconds = 30000, 
    int WaitingStepTimeInMilliseconds = 500) 
    : SolverConfig(MaxTimeOutTimeInMilliseconds, MaxPageLoadWaitingTimeInMilliseconds, WaitingStepTimeInMilliseconds);