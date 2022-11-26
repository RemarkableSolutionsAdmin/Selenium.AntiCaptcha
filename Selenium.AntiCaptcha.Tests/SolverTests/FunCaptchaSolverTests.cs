using AntiCaptchaApi.Net.Models.Solutions;
using OpenQA.Selenium;
using Selenium.AntiCaptcha;
using Selenium.AntiCaptcha.Enums;
using Selenium.AntiCaptcha.Models;
using Selenium.Anticaptcha.Tests.TestCore;

namespace Selenium.Anticaptcha.Tests.SolverTests
{
    public class FunCaptchaSolverTests : SequentialAnticaptchaTestBase
    {
        private readonly SolverAdditionalArguments _solverAdditionalArguments;
        [Fact]
        public void Solve_CaptchaTypeSpecified()
        {
            SetDriverUrl(TestUris.FunCaptcha.W1);
            var result = Driver.SolveCaptcha<FunCaptchaSolution>(ClientKey, _solverAdditionalArguments);
            AssertSolveCaptchaResult(result);
        }
        
        [Fact]
        public void Solve_WithoutCaptchaTypeSpecified()
        {
            SetDriverUrl(TestUris.FunCaptcha.W1);
            var result = Driver.SolveCaptcha<FunCaptchaSolution>(ClientKey, _solverAdditionalArguments);
            AssertSolveCaptchaResult(result);
        }

        [Fact]
        public void SolveNonGeneric_CaptchaTypeSpecified()
        {
            SetDriverUrl(TestUris.FunCaptcha.W1);
            var result = Driver.SolveCaptcha(ClientKey, _solverAdditionalArguments);
            AssertSolveCaptchaResult(result);
        }
        
        [Fact]
        public void SolveNonGeneric_WithoutCaptchaTypeSpecified()
        {
            SetDriverUrl(TestUris.FunCaptcha.W1);
            var result = Driver.SolveCaptcha(ClientKey, _solverAdditionalArguments);
            AssertSolveCaptchaResult(result);
        }

        public FunCaptchaSolverTests(WebDriverFixture fixture) : base(fixture)
        {
            _solverAdditionalArguments = new SolverAdditionalArguments
            {
                ProxyConfig = TestEnvironment.GetCurrentTestProxyConfig()
            };
        }
    }
}

