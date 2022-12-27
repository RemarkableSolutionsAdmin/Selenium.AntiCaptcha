using AntiCaptchaApi.Net.Models.Solutions;
using AntiCaptchaApi.Net.Requests;
using OpenQA.Selenium;
using Selenium.AntiCaptcha;
using Selenium.AntiCaptcha.Enums;
using Selenium.AntiCaptcha.Internal.Extensions;
using Selenium.AntiCaptcha.Internal.Models;
using Selenium.AntiCaptcha.Models;
using Selenium.Anticaptcha.Tests.TestCore;

namespace Selenium.Anticaptcha.Tests.FunctionalityTests;

public class WebDriverExtensionsTests : AnticaptchaTestBase
{
    public WebDriverExtensionsTests(WebDriverFixture fixture) : base(fixture) {}
    [Fact]
    public async Task ShouldThrowException_WhenSolutionTypeAndCaptchaTypeDoNotMatch()
    {
        await Assert.ThrowsAsync<ArgumentException>(() => Driver.SolveCaptchaAsync<GeeTestV3Solution>(ClientKey, new SolverArguments(CaptchaType: CaptchaType.ReCaptchaV2)));
    }

    public class FramesTreeTraversingTests : WebDriverExtensionsTests
    {
        private const string TestUri = TestUris.Recaptcha.V2.NonEnterprise.AntigateDemo; 
        // TestUris.Recaptcha.V2.NonEnterprise.W2: Has structure
        //   1
        // 1 1 1
        // 1
        
        public FramesTreeTraversingTests(WebDriverFixture fixture) : base(fixture)
        {
            SetDriverUrl(TestUri).Wait();
        }

        public override void Dispose()
        {
            base.Dispose();
            ResetDriverUri().Wait();
        }

        
        //TODO. Find site with more iframes.
        [Fact] 
        public async Task ShouldBuildFramesTree_AndReturnToOriginalFrame_FromBottom()
        {
            var rootFrame = Driver.GetCurrentFrame();
            var bottomFrame = GetDeepestFrame(null, 0);
            var rootElement = Driver.GetCurrentRootWebElement();
            Driver.SwitchTo().Frame(Driver.FindIFramesInCurrentFrame().First().WebElement);
            var currentFrame = Driver.GetCurrentFrame();
            Driver.GetFullFramesTree();
            var afterOperationFrame = Driver.GetCurrentFrame();
            
            
            Assert.True(rootFrame.IsRoot);
            Assert.False(rootFrame.IsFrame);
            Assert.NotNull(rootElement);
            Assert.NotNull(currentFrame);
            Assert.NotNull(afterOperationFrame);
            Assert.Equal(currentFrame.WebElement, afterOperationFrame.WebElement);
        }

        private (IWebElement? frame, int level) GetDeepestFrame(IWebElement? frame, int level)
        {
            if (frame != null)
            {
                if (!Driver.TryToSwitchToFrame(new ExtendedWebElement(frame)))
                {
                    return (null, 0);
                }
            }

            var children = Driver.FindIFramesInCurrentFrame();

            if (children.Any())
            {
                var childrenFrames = children.Select(x => GetDeepestFrame(x, level + 1)).ToList();
                var maxLevel = childrenFrames.Max(x => x.level);
                return childrenFrames.First(x => x.level == maxLevel);
            }

            return (frame, level);
        }


        [Fact]
        public async Task ShouldBuildFramesTree_AndReturnToOriginalFrame()
        {
            await SetDriverUrl(TestUri);
            var rootFrame = Driver.GetCurrentFrame();
            var rootElement = Driver.GetCurrentRootWebElement();
            Driver.SwitchTo().Frame(Driver.FindIFramesInCurrentFrame().First().WebElement);
            var currentFrame = Driver.GetCurrentFrame();
            Driver.GetFullFramesTree();
            var afterOperationFrame = Driver.GetCurrentFrame();
            
            
            Assert.True(rootFrame.IsRoot);
            Assert.False(rootFrame.IsFrame);
            Assert.NotNull(rootElement);
            Assert.NotNull(currentFrame);
            Assert.NotNull(afterOperationFrame);
            Assert.True(currentFrame.Equals(afterOperationFrame));
            // Assert.Equal(currentFrame.WebElement, afterOperationFrame.WebElement);
        }

        [Fact]
        public async Task ShouldBuildProperFramesTree_AndReturnToOriginalFrame()
        {
            await SetDriverUrl(TestUri);
            var frameBeforeTraversingThrough = Driver.GetCurrentFrame();
            Driver.GetFullFramesTree();
            Assert.Equal(frameBeforeTraversingThrough, Driver.GetCurrentFrame());
        }

        [Fact]
        public async Task Test()
        {
        }
    }

}