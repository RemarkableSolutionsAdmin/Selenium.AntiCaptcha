using AntiCaptchaApi.Net.Models;
using AntiCaptchaApi.Net.Models.Solutions;
using Selenium.AntiCaptcha.Enums;
using Selenium.AntiCaptcha.Internal;
using Selenium.AntiCaptcha.Internal.Extensions;
using Selenium.AntiCaptcha.Internal.Helpers;
using Selenium.AntiCaptcha.Models;
using Selenium.Anticaptcha.Tests.TestCore;

namespace Selenium.Anticaptcha.Tests.FunctionalityTests;


public class CaptchaIdentifierTests : AnticaptchaTestBase
{
    private async Task TestProxyCaptchaIdentification(CaptchaUri captchaUri)
    {                
        var proxyType = captchaUri.ExpectedType.ToProxyType();
        if (proxyType.IsProxyType())
        {
            await TestNonGenericIdentifier(captchaUri with { ExpectedType = proxyType }, new SolverAdditionalArguments
            {
                ProxyConfig = TestEnvironment.GetCurrentTestProxyConfig()
            });
        }
    }

    private async Task TestProxylessCaptchaIdentification(CaptchaUri captchaUri)
    {
        if (captchaUri.ExpectedType.IsProxylessType())
        {
            await TestNonGenericIdentifier(captchaUri);
        }
    }
    
    public class IdentificationWithoutSolutionTypeSpecified : CaptchaIdentifierTests
    {
        public IdentificationWithoutSolutionTypeSpecified(WebDriverFixture fixture) : base(fixture)
        {
            
        }

        public class Recaptcha : IdentificationWithoutSolutionTypeSpecified
        {
            public Recaptcha(WebDriverFixture fixture) : base(fixture)
            {
            }
        
            [Theory]
            [MemberData(nameof(TestUris.Recaptcha.V3.Enterprise.Uris), MemberType = typeof(TestUris.Recaptcha.V3.Enterprise))]
            public async Task TestAllRecaptchaV3EnterpriseUrisProxyless(CaptchaUri captchaUri)
            {
                await TestProxylessCaptchaIdentification(captchaUri);
            }
         
         
            [Theory]
            [MemberData(nameof(TestUris.Recaptcha.V3.NonEnterprise.Uris), MemberType = typeof(TestUris.Recaptcha.V3.NonEnterprise))]
            public async Task TestAllRecaptchaV3NonEnterpriseUrisProxyless(CaptchaUri captchaUri)
            {
                await TestProxylessCaptchaIdentification(captchaUri);
            }

         
            [Theory]
            [MemberData(nameof(TestUris.Recaptcha.V2.NonEnterprise.Uris), MemberType = typeof(TestUris.Recaptcha.V2.NonEnterprise))]
            public async Task TestAllRecaptchaV2NonEnterpriseUrisProxyless(CaptchaUri captchaUri)
            {
                await TestProxylessCaptchaIdentification(captchaUri);
            }
         
            [Theory]
            [MemberData(nameof(TestUris.Recaptcha.V2.Enterprise.Uris), MemberType = typeof(TestUris.Recaptcha.V2.Enterprise))]
            public async Task TestAllRecaptchaV2EnterpriseUrisProxyless(CaptchaUri captchaUri)
            {
                await TestProxylessCaptchaIdentification(captchaUri);
            }
        
            [Theory]
            [MemberData(nameof(TestUris.Recaptcha.V3.Enterprise.Uris), MemberType = typeof(TestUris.Recaptcha.V3.Enterprise))]
            public async Task TestAllRecaptchaV3EnterpriseUrisProxy(CaptchaUri captchaUri)
            {
                await TestProxyCaptchaIdentification(captchaUri);
            }
         
         
            [Theory]
            [MemberData(nameof(TestUris.Recaptcha.V3.NonEnterprise.Uris), MemberType = typeof(TestUris.Recaptcha.V3.NonEnterprise))]
            public async Task TestAllRecaptchaV3NonEnterpriseUrisProxy(CaptchaUri captchaUri)
            {
                await TestProxyCaptchaIdentification(captchaUri);
            }

         
            [Theory]
            [MemberData(nameof(TestUris.Recaptcha.V2.NonEnterprise.Uris), MemberType = typeof(TestUris.Recaptcha.V2.NonEnterprise))]
            public async Task TestAllRecaptchaV2NonEnterpriseUrisProxy(CaptchaUri captchaUri)
            {
                await TestProxyCaptchaIdentification(captchaUri);
            }
         
            [Theory]
            [MemberData(nameof(TestUris.Recaptcha.V2.Enterprise.Uris), MemberType = typeof(TestUris.Recaptcha.V2.Enterprise))]
            public async Task TestAllRecaptchaV2EnterpriseUrisProxy(CaptchaUri captchaUri)
            {
                await TestProxyCaptchaIdentification(captchaUri);
            }
        }

        
        public class OtherCaptcha : IdentificationWithoutSolutionTypeSpecified
        {
            public OtherCaptcha(WebDriverFixture fixture) : base(fixture)
            {
            }
            
            [Theory]
            [MemberData(nameof(TestUris.TestableUrisWithoutRecaptcha), MemberType = typeof(TestUris))]
            public async Task TestAllProxylessCaptchaUris(CaptchaUri captchaUri)
            {
                if (captchaUri.ExpectedType.IsProxylessType())
                {
                    await TestNonGenericIdentifier(captchaUri);
                }
            }
         
            [Theory]
            [MemberData(nameof(TestUris.TestableUrisWithoutRecaptcha), MemberType = typeof(TestUris))]
            public async Task TestAllProxyCaptchaUris(CaptchaUri captchaUri)
            {
                var proxyType = captchaUri.ExpectedType.ToProxyType();
                if (!proxyType.IsProxylessType())
                {
                    await TestNonGenericIdentifier(captchaUri with { ExpectedType = proxyType }, new SolverAdditionalArguments
                    {
                        ProxyConfig = TestEnvironment.GetCurrentTestProxyConfig()
                    });
                }
            }
        }
        
    }
    


    public class IdentificationWithSolutionTypeSpecified : CaptchaIdentifierTests
    {
        
        [Theory]
        [InlineData(TestUris.GeeTest.V3.W1, CaptchaType.GeeTestV3Proxyless)]
        public async Task ShouldReturnProperGeeV3TestType_WithoutProxy(string websiteUrl, CaptchaType expectedType)
        {
            await TestIdentifier<GeeTestV3Solution>(websiteUrl, expectedType, null);
        }
        
        
        [Theory]
        [InlineData(TestUris.GeeTest.V3.W1, CaptchaType.GeeTestV3)]
        public async Task ShouldReturnProperGeeV3TestType_WithProxy(string websiteUrl, CaptchaType expectedType)
        {
            await TestIdentifier<GeeTestV3Solution>(websiteUrl, expectedType, new SolverAdditionalArguments
            {
                ProxyConfig = TestEnvironment.GetCurrentTestProxyConfig()
            });
        }
        
        
        [Theory]
        [InlineData(TestUris.GeeTest.V4.W1, CaptchaType.GeeTestV4Proxyless)]
        public async Task ShouldReturnProperGeeV4TestType_WithoutProxy(string websiteUrl, CaptchaType expectedType)
        {
            await TestIdentifier<GeeTestV4Solution>(websiteUrl, expectedType, null);
        }
        
        
        [Theory]
        [InlineData(TestUris.GeeTest.V4.W1, CaptchaType.GeeTestV4)]
        public async Task ShouldReturnProperGeeV4TestType_WithProxy(string websiteUrl, CaptchaType expectedType)
        {
            await TestIdentifier<GeeTestV4Solution>(websiteUrl, expectedType, new SolverAdditionalArguments
            {
                ProxyConfig = TestEnvironment.GetCurrentTestProxyConfig()
            });
        }
        
        [Theory]
        [InlineData(TestUris.Recaptcha.V3.Enterprise.W1, CaptchaType.ReCaptchaV3Enterprise)]
        [InlineData(TestUris.Recaptcha.V3.NonEnterprise.W1, CaptchaType.ReCaptchaV3Proxyless)]
        [InlineData(TestUris.Recaptcha.V2.NonEnterprise.W1, CaptchaType.ReCaptchaV2Proxyless)]
        public async Task ShouldReturnProperRecaptchaType_WithoutProxy(string websiteUrl, CaptchaType expectedType)
        {
            await TestIdentifier<RecaptchaSolution>(websiteUrl, expectedType, null);
        }
        
        [Theory]
        [InlineData(TestUris.Recaptcha.V3.Enterprise.W1, CaptchaType.ReCaptchaV3Enterprise)]
        [InlineData(TestUris.Recaptcha.V2.NonEnterprise.W1, CaptchaType.ReCaptchaV2)]
        public async Task ShouldReturnProperRecaptchaType_WithProxy(string websiteUrl, CaptchaType expectedType)
        {
            await TestIdentifier<RecaptchaSolution>(websiteUrl, expectedType, new SolverAdditionalArguments
            {
                ProxyConfig = TestEnvironment.GetCurrentTestProxyConfig()
            });
        }
        
        
        [Theory]
        [InlineData(TestUris.FunCaptcha.W1, CaptchaType.FunCaptchaProxyless)]
        public async Task ShouldReturnProperFunCaptchaType_WithoutProxy(string websiteUrl, CaptchaType expectedType)
        {
            await TestIdentifier<FunCaptchaSolution>(websiteUrl, expectedType, null);
        }
        
        [Theory]
        [InlineData(TestUris.FunCaptcha.W1, CaptchaType.FunCaptcha)]
        public async Task ShouldReturnProperFunCaptchaType_WithProxy(string websiteUrl, CaptchaType expectedType)
        {
            await TestIdentifier<FunCaptchaSolution>(websiteUrl, expectedType, new SolverAdditionalArguments
            {
                ProxyConfig = TestEnvironment.GetCurrentTestProxyConfig()
            });
        }
        
        
        [Theory]
        [InlineData(TestUris.ImageToText.W1, CaptchaType.ImageToText)]
        public async Task ShouldReturnProperImageToTextType_WithoutProxy(string websiteUrl, CaptchaType expectedType)
        {
            await TestIdentifier<ImageToTextSolution>(websiteUrl, expectedType, null);
        }
        
        [Theory]
        [InlineData(TestUris.AntiGate.W1, CaptchaType.AntiGate)]
        public async Task ShouldReturnProperAntiGateType_WithoutProxy(string websiteUrl, CaptchaType expectedType)
        {
            await TestIdentifier<AntiGateSolution>(websiteUrl, expectedType, null);
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
    
    protected async Task TestNonGenericIdentifier(CaptchaUri captchaUri, SolverAdditionalArguments? additionalArguments = null)
    {
        var solverAdditionalArguments = additionalArguments ?? new SolverAdditionalArguments();
        await SetDriverUrl(captchaUri.Uri);
        var identifiedTypes = await AllCaptchaTypesIdentifier.IdentifyAsync(Driver, solverAdditionalArguments);
        var testFailed = identifiedTypes.Count != 1 || identifiedTypes[0] != captchaUri.ExpectedType;
        
        Assert.False(testFailed, GetTestFailReasonText(captchaUri, solverAdditionalArguments, string.Join(", ", identifiedTypes.Select(x => x.ToString()))));
    }

    private static string GetTestFailReasonText(CaptchaUri captchaUri, SolverAdditionalArguments? additionalArguments, string foundTypesNames)
    {
        return $"Test non generic identifier failed for url: {captchaUri.Uri}. \n" +
               $"Expected type {captchaUri.ExpectedType}, but found {foundTypesNames}\n" +
               $"ProxyConfig was {additionalArguments?.ProxyConfig}";
    }
    
    protected async Task TestIdentifier<TSolution>(string websiteUri, CaptchaType expectedType, SolverAdditionalArguments? additionalArguments = null)
        where TSolution : BaseSolution, new()
    {
        var captchaUri = new CaptchaUri(websiteUri, expectedType);
        await TestIdentifier<TSolution>(captchaUri, additionalArguments);
    }

    protected async Task TestIdentifier<TSolution>(CaptchaUri captchaUri, SolverAdditionalArguments? additionalArguments = null)
        where TSolution : BaseSolution, new()
    {
        var solverAdditionalArguments = additionalArguments ?? new SolverAdditionalArguments();
        await SetDriverUrl(captchaUri.Uri);
        var type = await AllCaptchaTypesIdentifier.IdentifyCaptcha<TSolution>(Driver, solverAdditionalArguments); 
        var testFailed = type == null || type != captchaUri.ExpectedType;
        Assert.False(testFailed, GetTestFailReasonText(captchaUri, solverAdditionalArguments, type.ToString()));
    }
    public CaptchaIdentifierTests(WebDriverFixture fixture) : base(fixture) {}
}