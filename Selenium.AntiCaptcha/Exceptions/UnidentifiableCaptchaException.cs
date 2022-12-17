namespace Selenium.AntiCaptcha.Exceptions;

public class UnidentifiableCaptchaException : ArgumentException
{
    public UnidentifiableCaptchaException(string details) : base(details)
    {
        
    }
}