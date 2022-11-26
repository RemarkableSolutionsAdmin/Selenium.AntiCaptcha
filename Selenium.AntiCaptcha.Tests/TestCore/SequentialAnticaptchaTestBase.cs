namespace Selenium.Anticaptcha.Tests.TestCore;

[Collection("Sequential")]
public class SequentialAnticaptchaTestBase : AnticaptchaTestBase
{
    public SequentialAnticaptchaTestBase(WebDriverFixture fixture) : base(fixture)
    {
    }
}