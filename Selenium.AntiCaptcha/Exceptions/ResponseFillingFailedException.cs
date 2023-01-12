namespace Selenium.AntiCaptcha.Exceptions;

public class ResponseFillingFailedException : Exception
{
    public ResponseFillingFailedException() : base("Unfortunately could not fill the response elements with response from anti-captcha.")
    {
        
    }
}