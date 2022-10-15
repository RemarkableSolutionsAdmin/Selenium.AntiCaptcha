using AntiCaptchaApi.Net.Models;
using AntiCaptchaApi.Net.Models.Solutions;
using Selenium.AntiCaptcha.Enums;
using Selenium.AntiCaptcha.Internal;
using Selenium.AntiCaptcha.Internal.Extensions;
using Selenium.AntiCaptcha.Internal.Helpers;
using Selenium.Anticaptcha.Tests.TestCore;

namespace Selenium.Anticaptcha.Tests.FunctionalityTests;


public class CaptchaIdentifierTests : AnticaptchaTestBase
{

    public class IdentificationWithoutSolutionTypeSpecified : CaptchaIdentifierTests
    {
        public IdentificationWithoutSolutionTypeSpecified(WebDriverFixture fixture) : base(fixture)
        {
        }
        
        [Theory]
        [MemberData(nameof(TestUris.TestableUris), MemberType = typeof(TestUris))]
        public void TestAllProxylessCaptchaUris(CaptchaUri captchaUri)
        {
            if (captchaUri.ExpectedType.IsProxylessType())
            {
                TestNonGenericIdentifier(captchaUri);
            }
        }
        
        
        [Theory]
        [MemberData(nameof(TestUris.TestableUris), MemberType = typeof(TestUris))]
        public void TestAllProxyCaptchaUris(CaptchaUri captchaUri)
        {
            var proxyType = captchaUri.ExpectedType.ToProxyType();
            
            if (proxyType.IsProxylessType())
            {
                TestNonGenericIdentifier(captchaUri.Uri, proxyType, TestEnvironment.GetCurrentTestProxyConfig());
            }
        }
    }
    


    public class IdentificationWithSolutionTypeSpecified : CaptchaIdentifierTests
    {
        
        [Theory]
        [InlineData(TestUris.GeeTest.V3.W1, CaptchaType.GeeTestV3Proxyless)]
        public void ShouldReturnProperGeeV3TestType_WithoutProxy(string websiteUrl, CaptchaType expectedType)
        {
            TestIdentifier<GeeTestV3Solution>(websiteUrl, expectedType, null);
        }
        
        
        [Theory]
        [InlineData(TestUris.GeeTest.V3.W1, CaptchaType.GeeTestV3)]
        public void ShouldReturnProperGeeV3TestType_WithProxy(string websiteUrl, CaptchaType expectedType)
        {
            TestIdentifier<GeeTestV3Solution>(websiteUrl, expectedType, TestEnvironment.GetCurrentTestProxyConfig());
        }
        
        
        [Theory]
        [InlineData(TestUris.GeeTest.V4.W1, CaptchaType.GeeTestV4Proxyless)]
        public void ShouldReturnProperGeeV4TestType_WithoutProxy(string websiteUrl, CaptchaType expectedType)
        {
            TestIdentifier<GeeTestV4Solution>(websiteUrl, expectedType, null);
        }
        
        
        [Theory]
        [InlineData(TestUris.GeeTest.V4.W1, CaptchaType.GeeTestV4)]
        public void ShouldReturnProperGeeV4TestType_WithProxy(string websiteUrl, CaptchaType expectedType)
        {
            TestIdentifier<GeeTestV4Solution>(websiteUrl, expectedType, TestEnvironment.GetCurrentTestProxyConfig());
        }

        [Theory]
        [InlineData(TestUris.Recaptcha.V3.Enterprise.W1, CaptchaType.ReCaptchaV3Enterprise)]
        [InlineData(TestUris.Recaptcha.V3.NonEnterprise.W1, CaptchaType.ReCaptchaV3Proxyless)]
        [InlineData(TestUris.Recaptcha.V2.NonEnterprise.W1, CaptchaType.ReCaptchaV2Proxyless)]
        public void ShouldReturnProperRecaptchaType_WithoutProxy(string websiteUrl, CaptchaType expectedType)
        {
            TestIdentifier<RecaptchaSolution>(websiteUrl, expectedType, null);
        }
        
        [Theory]
        [InlineData(TestUris.Recaptcha.V3.NonEnterprise.W1, CaptchaType.ReCaptchaV3Enterprise)]
        [InlineData(TestUris.Recaptcha.V2.NonEnterprise.W1, CaptchaType.ReCaptchaV2)]
        public void ShouldReturnProperRecaptchaType_WithProxy(string websiteUrl, CaptchaType expectedType)
        {
            TestIdentifier<RecaptchaSolution>(websiteUrl, expectedType, TestEnvironment.GetCurrentTestProxyConfig());
        }

        
        [Theory]
        [InlineData(TestUris.FunCaptcha.W1, CaptchaType.FunCaptchaProxyless)]
        public void ShouldReturnProperFunCaptchaType_WithoutProxy(string websiteUrl, CaptchaType expectedType)
        {
            TestIdentifier<FunCaptchaSolution>(websiteUrl, expectedType, null);
        }
        
        [Theory]
        [InlineData(TestUris.FunCaptcha.W1, CaptchaType.FunCaptcha)]
        public void ShouldReturnProperFunCaptchaType_WithProxy(string websiteUrl, CaptchaType expectedType)
        {
            TestIdentifier<FunCaptchaSolution>(websiteUrl, expectedType, TestEnvironment.GetCurrentTestProxyConfig());
        }

        
        [Theory]
        [InlineData(TestUris.ImageToText.W1, CaptchaType.ImageToText)]
        public void ShouldReturnProperImageToTextType_WithoutProxy(string websiteUrl, CaptchaType expectedType)
        {
            TestIdentifier<ImageToTextSolution>(websiteUrl, expectedType, null);
        }

        [Theory]
        [InlineData(TestUris.AntiGate.W1, CaptchaType.AntiGate)]
        public void ShouldReturnProperAntiGateType_WithoutProxy(string websiteUrl, CaptchaType expectedType)
        {
            TestIdentifier<AntiGateSolution>(websiteUrl, expectedType, null);
        }


        

        public IdentificationWithSolutionTypeSpecified(WebDriverFixture fixture) : base(fixture)
        {
            
        }
    }
    
    [Fact]
    public void ShouldBeAbleToIdentifyAllCaptchaTypes()
    {
        foreach (var enumValue in Enum.GetValues(typeof(CaptchaType)))
        {
            var canIdentify = AllCaptchaTypesIdentifier.CanIdentifyCaptcha((CaptchaType)enumValue);
            if (canIdentify)
                continue;
            Assert.Null(enumValue);
            Assert.True(canIdentify);
        }
    }
    
    protected void TestNonGenericIdentifier(CaptchaUri captchaUri, ProxyConfig? proxyConfig = null)
    {
        SetDriverUrl(captchaUri.Uri);
        var identifiedTypes = AllCaptchaTypesIdentifier.Identify(Driver, proxyConfig);
        var testFailed = identifiedTypes.Count != 1 || identifiedTypes[0] != captchaUri.ExpectedType;
        
        Assert.False(testFailed,
            $"Test non generic identifier failed for url: {captchaUri.Uri}. \n" +
            $"Expected type {captchaUri.ExpectedType}, but found {string.Join(", ", identifiedTypes.Select(x => x.ToString()))}\n" +
            $"ProxyConfig was {proxyConfig}");
    }

    protected void TestNonGenericIdentifier(string websiteUrl, CaptchaType expectedType, ProxyConfig? proxyConfig)
    {
        SetDriverUrl(websiteUrl);
        var identifiedTypes = AllCaptchaTypesIdentifier.Identify(Driver, proxyConfig);
        var testFailed = identifiedTypes.Count != 1 || identifiedTypes[0] != expectedType;
        
        Assert.False(testFailed,
            $"Test non generic identifier failed for url: {websiteUrl}. \n" +
            $"Expected type {expectedType}, but found {string.Join(", ", identifiedTypes.Select(x => x.ToString()))}\n" +
            $"ProxyConfig was {proxyConfig}");
    }
    
    protected void TestIdentifier<TSolution>(string websiteUrl, CaptchaType expectedType, ProxyConfig? proxyConfig = null)
        where TSolution : BaseSolution, new()
    {
        SetDriverUrl(websiteUrl);
        var type = AllCaptchaTypesIdentifier.IdentifyCaptcha<TSolution>(Driver, proxyConfig);
        var testFailed = type == null || type != expectedType;
        Assert.False(testFailed,
            $"Test non generic identifier failed for url: {websiteUrl}. \n" +
            $"Expected type {expectedType}, but found {type}\n" +
            $"ProxyConfig was {proxyConfig}");
    }
    public CaptchaIdentifierTests(WebDriverFixture fixture) : base(fixture) {}
}