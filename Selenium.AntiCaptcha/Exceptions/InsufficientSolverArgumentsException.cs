namespace Selenium.AntiCaptcha.Exceptions;

public class InsufficientSolverArgumentsException : ArgumentException
{
    public InsufficientSolverArgumentsException(string details) : base(details)
    {
        
    }
}