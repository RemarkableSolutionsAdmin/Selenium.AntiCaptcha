using AntiCaptchaApi.Net.Models;
using AntiCaptchaApi.Net.Requests;

namespace Selenium.AntiCaptcha.Models;

public record AntiGateSolverArguments : SolverArguments
{
    public AntiGateSolverArguments(AntiGateRequest request) :
        base(WebsiteUrl: request.WebsiteUrl,
            CaptchaType: Enums.CaptchaType.AntiGate,
            TemplateName: request.TemplateName,
            Variables: request.Variables,
            DomainsOfInterest: request.DomainsOfInterest,
            ProxyConfig: (TypedProxyConfig)request.ProxyConfig)
    {
        
    }
}