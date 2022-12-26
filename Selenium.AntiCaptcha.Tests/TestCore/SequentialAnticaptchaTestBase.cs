using Selenium.AntiCaptcha.Models;

namespace Selenium.Anticaptcha.Tests.TestCore;

[Collection("Sequential")]
public class SequentialAnticaptchaTestBase : AnticaptchaTestBase
{
    
    protected readonly SolverAdditionalArguments _solverProxyAdditionalArguments;
    public SequentialAnticaptchaTestBase(WebDriverFixture fixture) : base(fixture)
    {
        _solverProxyAdditionalArguments = new SolverAdditionalArguments
        {
            ProxyConfig = TestEnvironment.GetCurrentTestProxyConfig()
        };
    }
}