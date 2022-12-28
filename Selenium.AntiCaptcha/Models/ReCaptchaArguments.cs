using AntiCaptchaApi.Net.Models;

namespace Selenium.AntiCaptcha.Models;

public interface IRecaptchaArguments
{
    TypedProxyConfig ProxyConfig { get; set; }
}

public record ReCaptchaArguments : IRecaptchaArguments
{
    public TypedProxyConfig ProxyConfig { get; set; }
    public string UserAgent { get; set; }
    public string Cookies { get; set; }
    public Dictionary<string, string> EnterprisePayload { get; set; }
    public string ApiDomain { get; set; }
}