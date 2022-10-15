using AntiCaptchaApi.Net.Models.Solutions;
using OpenQA.Selenium;
using Selenium.AntiCaptcha;
using Selenium.AntiCaptcha.Enums;
using Selenium.Anticaptcha.Tests.TestCore;

namespace Selenium.Anticaptcha.Tests.SolverTests;

public record CaptchaSolverTest(string Uri, CaptchaType ExpectedType);

public abstract class BaseSolveBase <TSolution> : AnticaptchaTestBase
    where TSolution: BaseSolution, new()
{
    public static List<CaptchaSolverTest> CaptchaSolverTests = new List<CaptchaSolverTest>();
    protected BaseSolveBase(WebDriverFixture fixture) : base(fixture)
    {
        
    }
    //
    //
    // [Fact]
    // public void Solve_WithCaptchaTypeSpecified(CaptchaSolverTest)
    // {
    //     SetDriverUrl(websiteUrl); = TestUris.Recaptcha.V2.NonEnterprise.W2;
    //     var result = Driver.SolveCaptcha<TSolution>(ClientKey, captchaType: CaptchaType.ReCaptchaV2Proxyless);
    //     AssertSolveCaptchaResult(result);
    // }
    //
    
}