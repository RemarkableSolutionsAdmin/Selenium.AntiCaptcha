using System.Text;
using AntiCaptchaApi.Net.Models.Solutions;
using AntiCaptchaApi.Net.Responses;
using AntiCaptchaApi.Net.Responses.Abstractions;
using Selenium.AntiCaptcha;
using Selenium.AntiCaptcha.Models;
using Selenium.CaptchaIdentifier.Enums;
using Selenium.CaptchaIdentifier.Extensions;
using Tests.Common;
using Tests.Common.Config;
using Tests.Common.Core;
using Tests.Common.Core.Models;
using Xunit;

namespace Selenium.Anticaptcha.Tests.Core.SolverTestBases;

[Collection("Sequential")]
public abstract class SolverTestBase<TSolution> : WebDriverBasedTestBase
    where TSolution : BaseSolution, new()
{
    protected abstract string TestedUri { get; set; }
    protected abstract CaptchaType CaptchaType { get; set; }
    protected SolverArguments SolverArgumentsWithoutCaptchaType { get; set; }
    protected SolverArguments SolverArgumentsWithCaptchaType { get; set; }

    protected SolverTestBase(WebDriverFixture fixture) : base(fixture)
    {
        ValidateCaptchaUri();
        
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
    protected static void AssertSolveCaptchaResult<TSolution>(TaskResultResponse<TSolution>? result, CaptchaType expectedCaptchaType)
        where TSolution : BaseSolution, new()
    {
        Assert.NotNull(result);
        if (!string.IsNullOrEmpty(result!.ErrorDescription))
        {
            Fail(BuildErrorMessage(result));
        }
        
        Assert.True(result.Solution.IsValid());
        var expectedCaptchaTypeText = expectedCaptchaType.ToString();
        //TODO! There's task in the name. so it does not find it.
        // Assert.Contains($"\"{expectedCaptchaType.ToString()}\"", result.CreateTaskResponse.RawRequestPayload);
    }

    private static string BuildErrorMessage<TSolution>(TaskResultResponse<TSolution> result) //TODO move somewhere else.
        where TSolution : BaseSolution, new()
    {
        var stringBuilder = new StringBuilder();

        AppendTitleWithValue(stringBuilder, nameof(TaskResultResponse<TSolution>.ErrorDescription), result.ErrorDescription);
        AppendTitleWithValue(stringBuilder, nameof(TaskResultResponse<TSolution>.RawPayload), result.RawPayload);
        AppendTitleWithValue(stringBuilder, nameof(TaskResultResponse<TSolution>.RawResponse), result.RawResponse);
        AppendTitleWithValue(stringBuilder, nameof(TaskResultResponse<TSolution>.CreateTaskResponse.RawPayload), result.CreateTaskResponse?.RawPayload);
        AppendTitleWithValue(stringBuilder, nameof(TaskResultResponse<TSolution>.CreateTaskResponse.RawResponse), result.CreateTaskResponse?.RawResponse);
        
        return stringBuilder.ToString();
    }

    private static StringBuilder AppendTitleWithValue(StringBuilder stringBuilder, string? title, string? value)
    {
        if (!string.IsNullOrEmpty(title) && !string.IsNullOrEmpty(value))
        {
            AppendTitle(stringBuilder, title);
            AppendWithNewLine(stringBuilder, value);   
        }
        return stringBuilder;
    }
    
    private static StringBuilder AppendTitle(StringBuilder stringBuilder, string title)
    {
        AppendWithNewLine(stringBuilder, $"----------{title}----------");
        return stringBuilder;
    }
    
    private static StringBuilder AppendWithNewLine(StringBuilder stringBuilder, string value)
    {
        stringBuilder.Append($"{Environment.NewLine}{value}{Environment.NewLine}");
        return stringBuilder;
    }

    protected static void AssertSolveCaptchaResult(BaseResponse result, CaptchaType expectedCaptchaType)
    {
        if (result is TaskResultResponse<RecaptchaSolution>)
            AssertSolveCaptchaResult((TaskResultResponse<RecaptchaSolution>?)result, expectedCaptchaType);
        if (result is TaskResultResponse<HCaptchaSolution>)
            AssertSolveCaptchaResult((TaskResultResponse<HCaptchaSolution>?)result, expectedCaptchaType);
        if (result is TaskResultResponse<FunCaptchaSolution>)
            AssertSolveCaptchaResult((TaskResultResponse<FunCaptchaSolution>?)result, expectedCaptchaType);
        if (result is TaskResultResponse<GeeTestV3Solution>)
            AssertSolveCaptchaResult((TaskResultResponse<GeeTestV3Solution>?)result, expectedCaptchaType);
        if (result is TaskResultResponse<GeeTestV4Solution>)
            AssertSolveCaptchaResult((TaskResultResponse<GeeTestV4Solution>?)result, expectedCaptchaType);
        if (result is TaskResultResponse<AntiGateSolution>)
            AssertSolveCaptchaResult((TaskResultResponse<AntiGateSolution>?)result, expectedCaptchaType);
        if (result is TaskResultResponse<ImageToTextSolution>)
            AssertSolveCaptchaResult((TaskResultResponse<ImageToTextSolution>?)result, expectedCaptchaType);
        if (result is TaskResultResponse<TurnstileSolution>)
            AssertSolveCaptchaResult((TaskResultResponse<TurnstileSolution>?)result, expectedCaptchaType);
    }
}