using AntiCaptchaApi.Net.Models.Solutions;
using OpenQA.Selenium;
using Selenium.AntiCaptcha;
using Selenium.AntiCaptcha.Enums;
using Selenium.AntiCaptcha.Models;
using Selenium.Anticaptcha.Tests.TestCore;

namespace Selenium.Anticaptcha.Tests.SolverTests
{
    public class AntiGateSolverTests : SequentialAnticaptchaTestBase
    {
        private readonly SolverAdditionalArguments _solverAdditionalArguments;
        
        [Fact]
        public async Task Solve_CaptchaTypeSpecified()
        {
            SetDriverUrl(TestUris.AntiGate.W1);
            var result = await Driver.SolveCaptchaAsync<AntiGateSolution>(ClientKey, _solverAdditionalArguments);
            AssertSolveCaptchaResult(result);
        }
        
        [Fact]
        public async Task Solve_WithoutCaptchaTypeSpecified()
        {
            SetDriverUrl(TestUris.AntiGate.W1);
            var result = await Driver.SolveCaptchaAsync<AntiGateSolution>(ClientKey, _solverAdditionalArguments);
            AssertSolveCaptchaResult(result);
        }

        [Fact]
        public async Task SolveNonGeneric_CaptchaTypeSpecified()
        {
            SetDriverUrl(TestUris.AntiGate.W1);
            var result = await Driver.SolveCaptchaAsync(ClientKey, _solverAdditionalArguments);
            AssertSolveCaptchaResult(result);
        }
        
        [Fact]
        public async Task SolveNonGeneric_WithoutCaptchaTypeSpecified()
        {
            SetDriverUrl(TestUris.AntiGate.W1);
            var result = await Driver.SolveCaptchaAsync(ClientKey, _solverAdditionalArguments);
            AssertSolveCaptchaResult(result);
        }

        public AntiGateSolverTests(WebDriverFixture fixture) : base(fixture)
        {
            _solverAdditionalArguments = new SolverAdditionalArguments
            {
                TemplateName = "CloudFlare cookies for a proxy",
                ProxyConfig = TestEnvironment.GetCurrentTestProxyConfig()
            };
        }
    }
}

