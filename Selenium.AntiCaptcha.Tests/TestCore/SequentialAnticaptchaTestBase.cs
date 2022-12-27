using Selenium.AntiCaptcha.Models;

namespace Selenium.Anticaptcha.Tests.TestCore;

[Collection("Sequential")]
public class SequentialAnticaptchaTestBase : AnticaptchaTestBase
{
    
    protected readonly SolverArguments SolverProxyArguments;
    public SequentialAnticaptchaTestBase(WebDriverFixture fixture) : base(fixture)
    {
        SolverProxyArguments = new SolverArguments
        {
            ProxyConfig = TestEnvironment.GetCurrentTestProxyConfig()
        };
    }
}