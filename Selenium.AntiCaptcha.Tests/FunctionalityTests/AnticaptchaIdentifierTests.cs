using AntiCaptchaApi.Net.Models;
using AntiCaptchaApi.Net.Models.Solutions;
using Selenium.AntiCaptcha.Enums;
using Selenium.AntiCaptcha.Internal;
using Selenium.Anticaptcha.Tests.TestCore;

namespace Selenium.Anticaptcha.Tests.FunctionalityTests;


public class AnticaptchaIdentifierTests : AnticaptchaTestBase
{

        public class IdentificationWithoutSolutionTypeSpecified : AnticaptchaIdentifierTests
    {
        
        [Theory]
        [InlineData(TestUris.GeeTest.V3.W1, CaptchaType.GeeTestV3Proxyless)]
        public void ShouldReturnProperGeeV3TestType_WithoutProxy(string websiteUrl, CaptchaType expectedType)
        {
            TestNonGenericIdentifier(websiteUrl, expectedType, null);
        }
        
        
        [Theory]
        [InlineData(TestUris.GeeTest.V3.W1, CaptchaType.GeeTestV3)]
        public void ShouldReturnProperGeeV3TestType_WithProxy(string websiteUrl, CaptchaType expectedType)
        {
            TestNonGenericIdentifier(websiteUrl, expectedType, TestEnvironment.GetCurrentTestProxyConfig());
        }
        
        
        [Theory]
        [InlineData(TestUris.GeeTest.V4.W1, CaptchaType.GeeTestV4Proxyless)]
        public void ShouldReturnProperGeeV4TestType_WithoutProxy(string websiteUrl, CaptchaType expectedType)
        {
            TestNonGenericIdentifier(websiteUrl, expectedType, null);
        }
        
        
        [Theory]
        [InlineData(TestUris.GeeTest.V4.W1, CaptchaType.GeeTestV4)]
        public void ShouldReturnProperGeeV4TestType_WithProxy(string websiteUrl, CaptchaType expectedType)
        {
            TestNonGenericIdentifier(websiteUrl, expectedType, TestEnvironment.GetCurrentTestProxyConfig());
        }
        
        [Theory]
        [InlineData(TestUris.Recaptcha.V3.EnterpriseW1, CaptchaType.ReCaptchaV3Enterprise)]
        [InlineData(TestUris.Recaptcha.V3.NoEnterpriseW1, CaptchaType.ReCaptchaV3Proxyless)]
        [InlineData(TestUris.Recaptcha.V2.W1, CaptchaType.ReCaptchaV2Proxyless)]
        public void ShouldReturnProperRecaptchaType_WithoutProxy(string websiteUrl, CaptchaType expectedType)
        {
            TestNonGenericIdentifier(websiteUrl, expectedType, null);
        }
        
        [Theory]
        [InlineData(TestUris.Recaptcha.V3.EnterpriseW1, CaptchaType.ReCaptchaV3Enterprise)]
        [InlineData(TestUris.Recaptcha.V2.W1, CaptchaType.ReCaptchaV2)]
        public void ShouldReturnProperRecaptchaType_WithProxy(string websiteUrl, CaptchaType expectedType)
        {
            TestNonGenericIdentifier(websiteUrl, expectedType, TestEnvironment.GetCurrentTestProxyConfig());
        }

        [Theory]
        [InlineData(TestUris.FunCaptcha.W1, CaptchaType.FunCaptchaProxyless)]
        [InlineData(TestUris.FunCaptcha.W2, CaptchaType.FunCaptchaProxyless)]
        [InlineData(TestUris.FunCaptcha.W3, CaptchaType.FunCaptchaProxyless)]
        public void ShouldReturnProperFunCaptchaType_WithoutProxy(string websiteUrl, CaptchaType expectedType)
        {
            TestNonGenericIdentifier(websiteUrl, expectedType, null);
        }
        
        [Theory]
        [InlineData(TestUris.FunCaptcha.W1, CaptchaType.FunCaptcha)]
        [InlineData(TestUris.FunCaptcha.W2, CaptchaType.FunCaptcha)]
        [InlineData(TestUris.FunCaptcha.W3, CaptchaType.FunCaptcha)]
        public void ShouldReturnProperFunCaptchaType_WithProxy(string websiteUrl, CaptchaType expectedType)
        {
            TestNonGenericIdentifier(websiteUrl, expectedType, TestEnvironment.GetCurrentTestProxyConfig());
        }
        
        [Theory]
        [InlineData(TestUris.ImageToText.W1, CaptchaType.ImageToText)]
        public void ShouldReturnProperImageToTextType_WithoutProxy(string websiteUrl, CaptchaType expectedType)
        {
            TestNonGenericIdentifier(websiteUrl, expectedType, null);
        }

        [Theory]
        [InlineData(TestUris.AntiGate.W1, CaptchaType.AntiGate)]
        public void ShouldReturnProperAntiGateType_WithoutProxy(string websiteUrl, CaptchaType expectedType)
        {
            TestNonGenericIdentifier(websiteUrl, expectedType, null);
        }

        public IdentificationWithoutSolutionTypeSpecified(WebDriverFixture fixture) : base(fixture)
        {
            
        }
    }
    


    public class IdentificationWithSolutionTypeSpecified : AnticaptchaIdentifierTests
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
        [InlineData(TestUris.Recaptcha.V3.EnterpriseW1, CaptchaType.ReCaptchaV3Enterprise)]
        [InlineData(TestUris.Recaptcha.V3.NoEnterpriseW1, CaptchaType.ReCaptchaV3Proxyless)]
        [InlineData(TestUris.Recaptcha.V2.W1, CaptchaType.ReCaptchaV2Proxyless)]
        public void ShouldReturnProperRecaptchaType_WithoutProxy(string websiteUrl, CaptchaType expectedType)
        {
            TestIdentifier<RecaptchaSolution>(websiteUrl, expectedType, null);
        }
        
        [Theory]
        [InlineData(TestUris.Recaptcha.V3.EnterpriseW1, CaptchaType.ReCaptchaV3Enterprise)]
        [InlineData(TestUris.Recaptcha.V2.W1, CaptchaType.ReCaptchaV2)]
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
            if (!canIdentify)
            {
                Assert.Null(enumValue);
                Assert.True(canIdentify);
            }
        }
    }

    protected void TestNonGenericIdentifier(string websiteUrl, CaptchaType expectedType, ProxyConfig? proxyConfig)
    {
        Fixture.Driver.Url = websiteUrl;
        var identifiedTypes = AllCaptchaTypesIdentifier.Identify(Driver, proxyConfig);
        Assert.NotNull(identifiedTypes);
        var resultType = Assert.Single(identifiedTypes); 
        Assert.Equal(expectedType, resultType);
    }
    
    protected void TestIdentifier<TSolution>(string websiteUrl, CaptchaType expectedType, ProxyConfig? proxyConfig)
        where TSolution : BaseSolution, new()
    {
        Fixture.Driver.Url = websiteUrl;
        var type = AllCaptchaTypesIdentifier.IdentifyCaptcha<TSolution>(Driver, proxyConfig);
        Assert.NotNull(type);
        Assert.Equal(expectedType, type.Value);
    }

    public AnticaptchaIdentifierTests(WebDriverFixture fixture) : base(fixture)
    {
    }
}