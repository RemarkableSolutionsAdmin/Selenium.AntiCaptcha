using AntiCaptchaApi.Net.Models.Solutions;
using Selenium.AntiCaptcha;
using Selenium.AntiCaptcha.Enums;
using Selenium.AntiCaptcha.Internal.Extensions;
using Selenium.AntiCaptcha.Models;
using Selenium.Anticaptcha.Tests.Core.Config;
using Selenium.Anticaptcha.Tests.Core.Models;

namespace Selenium.Anticaptcha.Tests.Core.SolverTestBases;

[Collection("Sequential")]
public abstract class SolverTestBase<TSolution> : AnticaptchaTestBase
    where TSolution : BaseSolution, new()
{
    protected abstract string TestedUri { get; set; }
    protected abstract CaptchaType CaptchaType { get; set; }
    protected SolverArguments SolverArgumentsWithoutCaptchaType { get; set; }
    protected SolverArguments SolverArgumentsWithCaptchaType { get; set; }

    protected SolverTestBase(WebDriverFixture fixture) : base(fixture)
    {
        ValidateCaptchaUri();
        Initialize();
    }

    private void Initialize()
    {
        SolverArgumentsWithoutCaptchaType = new SolverArguments();
        SolverArgumentsWithCaptchaType = new SolverArguments(CaptchaType);
        
        if (CaptchaType.IsProxyType())
        {
            SolverArgumentsWithoutCaptchaType.ProxyConfig = TestEnvironment.GetCurrentTestProxyConfig();
            SolverArgumentsWithCaptchaType.ProxyConfig = TestEnvironment.GetCurrentTestProxyConfig();
        }
    }

    protected virtual async Task BeforeTestAction()
    {
        
    }

    protected virtual async Task AfterTestAction()
    {
        
    }
    

    [Fact]
    public virtual async Task Solve_Generic_WithoutCaptchaTypeSpecified()
    {
        await Solve_Generic(SolverArgumentsWithoutCaptchaType);
    }

    [Fact]
    public virtual async Task Solve_Generic_WithCaptchaTypeSpecified()
    {
        await Solve_Generic(SolverArgumentsWithCaptchaType);
    }

    [Fact]
    public virtual async Task Solve_NonGeneric_WithoutCaptchaTypeSpecified()
    {
        await Solve_NonGeneric(SolverArgumentsWithoutCaptchaType);
    }
    [Fact]
    public virtual async Task Solve_NonGeneric_WithCaptchaTypeSpecified()
    {
        await Solve_NonGeneric(SolverArgumentsWithCaptchaType);
    }

    private async Task Solve_Generic(SolverArguments solverArguments)
    {
        await SetDriverUrl(TestedUri);
        await BeforeTestAction();
        var result = await Driver.SolveCaptchaAsync<TSolution>(ClientKey, solverArguments);
        AssertSolveCaptchaResult(result, expectedCaptchaType: CaptchaType);
        await AfterTestAction();
    }

    private async Task Solve_NonGeneric(SolverArguments solverArguments)
    {
        await SetDriverUrl(TestedUri);
        await BeforeTestAction();
        var result = await Driver.SolveCaptchaAsync(ClientKey, solverArguments);
        AssertSolveCaptchaResult(result, expectedCaptchaType: CaptchaType);
        await AfterTestAction();
    }

    private void ValidateCaptchaUri()
    {
        var testUri = TestUris.Uris().FirstOrDefault(x => x.FirstOrDefault(y =>((CaptchaUri)y).Uri == TestedUri) != null)?.FirstOrDefault();

        if (testUri == null)
        {
            Fail("Tested Uri not found in the Testable Uris.");
        }

        var captchaUri = (CaptchaUri)testUri!;
        var expectedType = CaptchaType.IsProxyType() ? captchaUri.ExpectedType.ToProxyType() : captchaUri.ExpectedType;
        
        if (expectedType != CaptchaType)
        {
            Fail("Tested captcha uri expected captcha type do not match expected tested captcha type.");
        }
    }
}