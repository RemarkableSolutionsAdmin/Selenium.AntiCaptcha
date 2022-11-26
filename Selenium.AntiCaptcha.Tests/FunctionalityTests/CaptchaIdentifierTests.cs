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
    private void TestProxyCaptchaIdentification(CaptchaUri captchaUri)
    {                
        var proxyType = captchaUri.ExpectedType.ToProxyType();
        if (proxyType.IsProxyType())
        {
            TestNonGenericIdentifier(captchaUri with { ExpectedType = proxyType }, new SolverAdditionalArguments
            {
                ProxyConfig = TestEnvironment.GetCurrentTestProxyConfig()
            });
        }
    }

    private void TestProxylessCaptchaIdentification(CaptchaUri captchaUri)
    {
        if (captchaUri.ExpectedType.IsProxylessType())
        {
            TestNonGenericIdentifier(captchaUri);
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
            public void TestAllRecaptchaV3EnterpriseUrisProxyless(CaptchaUri captchaUri)
            {
                TestProxylessCaptchaIdentification(captchaUri);
            }
         
         
            [Theory]
            [MemberData(nameof(TestUris.Recaptcha.V3.NonEnterprise.Uris), MemberType = typeof(TestUris.Recaptcha.V3.NonEnterprise))]
            public void TestAllRecaptchaV3NonEnterpriseUrisProxyless(CaptchaUri captchaUri)
            {
                TestProxylessCaptchaIdentification(captchaUri);
            }

         
            [Theory]
            [MemberData(nameof(TestUris.Recaptcha.V2.NonEnterprise.Uris), MemberType = typeof(TestUris.Recaptcha.V2.NonEnterprise))]
            public void TestAllRecaptchaV2NonEnterpriseUrisProxyless(CaptchaUri captchaUri)
            {
                TestProxylessCaptchaIdentification(captchaUri);
            }
         
            [Theory]
            [MemberData(nameof(TestUris.Recaptcha.V2.Enterprise.Uris), MemberType = typeof(TestUris.Recaptcha.V2.Enterprise))]
            public void TestAllRecaptchaV2EnterpriseUrisProxyless(CaptchaUri captchaUri)
            {
                TestProxylessCaptchaIdentification(captchaUri);
            }
        
            [Theory]
            [MemberData(nameof(TestUris.Recaptcha.V3.Enterprise.Uris), MemberType = typeof(TestUris.Recaptcha.V3.Enterprise))]
            public void TestAllRecaptchaV3EnterpriseUrisProxy(CaptchaUri captchaUri)
            {
                TestProxyCaptchaIdentification(captchaUri);
            }
         
         
            [Theory]
            [MemberData(nameof(TestUris.Recaptcha.V3.NonEnterprise.Uris), MemberType = typeof(TestUris.Recaptcha.V3.NonEnterprise))]
            public void TestAllRecaptchaV3NonEnterpriseUrisProxy(CaptchaUri captchaUri)
            {
                TestProxyCaptchaIdentification(captchaUri);
            }

         
            [Theory]
            [MemberData(nameof(TestUris.Recaptcha.V2.NonEnterprise.Uris), MemberType = typeof(TestUris.Recaptcha.V2.NonEnterprise))]
            public void TestAllRecaptchaV2NonEnterpriseUrisProxy(CaptchaUri captchaUri)
            {
                TestProxyCaptchaIdentification(captchaUri);
            }
         
            [Theory]
            [MemberData(nameof(TestUris.Recaptcha.V2.Enterprise.Uris), MemberType = typeof(TestUris.Recaptcha.V2.Enterprise))]
            public void TestAllRecaptchaV2EnterpriseUrisProxy(CaptchaUri captchaUri)
            {
                TestProxyCaptchaIdentification(captchaUri);
            }
        }

        
        public class OtherCaptcha : IdentificationWithoutSolutionTypeSpecified
        {
            public OtherCaptcha(WebDriverFixture fixture) : base(fixture)
            {
            }
            
            [Theory]
            [MemberData(nameof(TestUris.TestableUrisWithoutRecaptcha), MemberType = typeof(TestUris))]
            public void TestAllProxylessCaptchaUris(CaptchaUri captchaUri)
            {
                if (captchaUri.ExpectedType.IsProxylessType())
                {
                    TestNonGenericIdentifier(captchaUri);
                }
            }
         
            [Theory]
            [MemberData(nameof(TestUris.TestableUrisWithoutRecaptcha), MemberType = typeof(TestUris))]
            public void TestAllProxyCaptchaUris(CaptchaUri captchaUri)
            {
                var proxyType = captchaUri.ExpectedType.ToProxyType();
                if (!proxyType.IsProxylessType())
                {
                    TestNonGenericIdentifier(captchaUri with { ExpectedType = proxyType }, new SolverAdditionalArguments
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
        public void ShouldReturnProperGeeV3TestType_WithoutProxy(string websiteUrl, CaptchaType expectedType)
        {
            TestIdentifier<GeeTestV3Solution>(websiteUrl, expectedType, null);
        }
        
        
        [Theory]
        [InlineData(TestUris.GeeTest.V3.W1, CaptchaType.GeeTestV3)]
        public void ShouldReturnProperGeeV3TestType_WithProxy(string websiteUrl, CaptchaType expectedType)
        {
            TestIdentifier<GeeTestV3Solution>(websiteUrl, expectedType, new SolverAdditionalArguments
            {
                ProxyConfig = TestEnvironment.GetCurrentTestProxyConfig()
            });
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
            TestIdentifier<GeeTestV4Solution>(websiteUrl, expectedType, new SolverAdditionalArguments
            {
                ProxyConfig = TestEnvironment.GetCurrentTestProxyConfig()
            });
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
        [InlineData(TestUris.Recaptcha.V3.Enterprise.W1, CaptchaType.ReCaptchaV3Enterprise)]
        [InlineData(TestUris.Recaptcha.V2.NonEnterprise.W1, CaptchaType.ReCaptchaV2)]
        public void ShouldReturnProperRecaptchaType_WithProxy(string websiteUrl, CaptchaType expectedType)
        {
            TestIdentifier<RecaptchaSolution>(websiteUrl, expectedType, new SolverAdditionalArguments
            {
                ProxyConfig = TestEnvironment.GetCurrentTestProxyConfig()
            });
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
            TestIdentifier<FunCaptchaSolution>(websiteUrl, expectedType, new SolverAdditionalArguments
            {
                ProxyConfig = TestEnvironment.GetCurrentTestProxyConfig()
            });
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
    
    protected void TestNonGenericIdentifier(CaptchaUri captchaUri, SolverAdditionalArguments? additionalArguments = null)
    {
        var solverAdditionalArguments = additionalArguments ?? new SolverAdditionalArguments();
        SetDriverUrl(captchaUri.Uri);
        var identifyTask = AllCaptchaTypesIdentifier.IdentifyAsync(Driver, solverAdditionalArguments);
        Task.WaitAll(identifyTask);
        var identifiedTypes = identifyTask.Result;
        var testFailed = identifiedTypes.Count != 1 || identifiedTypes[0] != captchaUri.ExpectedType;
        
        Assert.False(testFailed, GetTestFailReasonText(captchaUri, solverAdditionalArguments, string.Join(", ", identifiedTypes.Select(x => x.ToString()))));
    }

    private static string GetTestFailReasonText(CaptchaUri captchaUri, SolverAdditionalArguments? additionalArguments, string foundTypesNames)
    {
        return $"Test non generic identifier failed for url: {captchaUri.Uri}. \n" +
               $"Expected type {captchaUri.ExpectedType}, but found {foundTypesNames}\n" +
               $"ProxyConfig was {additionalArguments?.ProxyConfig}";
    }
    
    protected void TestIdentifier<TSolution>(string websiteUri, CaptchaType expectedType, SolverAdditionalArguments? additionalArguments = null)
        where TSolution : BaseSolution, new()
    {
        var captchaUri = new CaptchaUri(websiteUri, expectedType);
        TestIdentifier<TSolution>(captchaUri, additionalArguments);
    }
    

    protected void TestIdentifier<TSolution>(CaptchaUri captchaUri, SolverAdditionalArguments? additionalArguments = null)
        where TSolution : BaseSolution, new()
    {
        var solverAdditionalArguments = additionalArguments ?? new SolverAdditionalArguments();
        SetDriverUrl(captchaUri.Uri);
        var identifyTask = AllCaptchaTypesIdentifier.IdentifyCaptcha<TSolution>(Driver, solverAdditionalArguments);
        Task.WaitAll(identifyTask);
        var type = identifyTask.Result; 
        var testFailed = type == null || type != captchaUri.ExpectedType;
        Assert.False(testFailed, GetTestFailReasonText(captchaUri, solverAdditionalArguments, type.ToString()));
    }
    public CaptchaIdentifierTests(WebDriverFixture fixture) : base(fixture) {}
}