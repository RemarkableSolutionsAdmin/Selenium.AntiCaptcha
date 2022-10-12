using AntiCaptchaApi.Net.Models;
using AntiCaptchaApi.Net.Models.Solutions;
using Selenium.AntiCaptcha.enums;
using Selenium.AntiCaptcha.Internal;
using Selenium.Anticaptcha.Tests.TestCore;

namespace Selenium.Anticaptcha.Tests.FunctionalityTests;


public class AnticaptchaIdentifierTests : AnticaptchaTestBase
{

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
    [InlineData(TestUris.GeeTest.V4.W1, CaptchaType.GeeTestV4)]
    public void ShouldReturnProperRecaptchaType_WithProxy(string websiteUrl, CaptchaType expectedType)
    {
        TestIdentifier<RecaptchaSolution>(websiteUrl, expectedType, TestEnvironment.GetCurrentTestProxyConfig());
    }


    private void TestIdentifier<TSolution>(string websiteUrl, CaptchaType expectedType, ProxyConfig? proxyConfig)
        where TSolution : BaseSolution, new()
    {
        fixture.Driver.Url = websiteUrl;
        var type = CaptchaTypeIdentifier.IdentifyCaptcha<TSolution>(Driver, null, proxyConfig);
        Assert.NotNull(type);
        Assert.Equal(expectedType, type.Value);
    }

    public AnticaptchaIdentifierTests(WebDriverFixture fixture) : base(fixture)
    {
    }
}