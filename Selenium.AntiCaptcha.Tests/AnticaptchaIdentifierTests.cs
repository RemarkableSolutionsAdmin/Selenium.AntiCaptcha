using AntiCaptchaApi.Net.Models;
using AntiCaptchaApi.Net.Models.Solutions;
using Selenium.AntiCaptcha.enums;
using Selenium.AntiCaptcha.Internal;

namespace Selenium.AntiCaptcha.Tests;


public class AnticaptchaIdentifierTests : AnticaptchaTestBase
{
    [Theory]
    [InlineData(TestUris.Recaptcha.V3.EnterpriseW1, CaptchaType.ReCaptchaV3Enterprise)]
    [InlineData(TestUris.Recaptcha.V3.NoEnterpriseW1, CaptchaType.ReCaptchaV3Proxyless)]
    [InlineData(TestUris.Recaptcha.V2.W1, CaptchaType.ReCaptchaV2Proxyless)]
    public void ShouldReturnProperRecaptchaType_WithoutProxy(string websiteUrl, CaptchaType expectedType)
    {
        TestIdentifier(websiteUrl, expectedType, null);
    }
    
    [Theory]
    [InlineData(TestUris.Recaptcha.V3.EnterpriseW1, CaptchaType.ReCaptchaV3Enterprise)]
    [InlineData(TestUris.Recaptcha.V2.W1, CaptchaType.ReCaptchaV2)]
    public void ShouldReturnProperRecaptchaType_WithProxy(string websiteUrl, CaptchaType expectedType)
    {
        TestIdentifier(websiteUrl, expectedType, GetCurrentTestProxyConfig());
    }


    private void TestIdentifier(string websiteUrl, CaptchaType expectedType, ProxyConfig? proxyConfig)
    {
        fixture.Driver.Url = websiteUrl;
        var type = AnticaptchaIdentifier.IdentifyCaptcha<RecaptchaSolution>(Driver, null, proxyConfig);
        Assert.NotNull(type);
        Assert.Equal(expectedType, type.Value);
    }

    public AnticaptchaIdentifierTests(WebDriverFixture fixture) : base(fixture)
    {
    }
}