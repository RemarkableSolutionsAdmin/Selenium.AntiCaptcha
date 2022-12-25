using AntiCaptchaApi.Net.Enums;
using AntiCaptchaApi.Net.Models;
using Newtonsoft.Json.Linq;
using OpenQA.Selenium;
using Selenium.AntiCaptcha.Enums;

namespace Selenium.AntiCaptcha.Models;

public record SolverAdditionalArguments(
    CaptchaType? CaptchaType = null, 
    string? Url = null, 
    string? SiteKey = null, 
    IWebElement? ResponseElement = null,
    IWebElement? SubmitElement = null, 
    IWebElement? ImageElement = null, 
    string? UserAgent = null, 
    ProxyConfig? ProxyConfig = null,
    string? FunCaptchaApiJsSubdomain = null,
    string? Data = null,
    JObject? Variables = null,
    string? TemplateName = null,
    List<string>? DomainsOfInterest = null,
    string? GeetestApiServerSubdomain = null,
    string? GeetestGetLib = null,
    string? Challenge = null,
    string? Gt = null,
    Dictionary<string, string>? InitParameters = null,
    double? MinScore = null,
    string? PageAction = null,
    string? ApiDomain = null,
    bool? IsEnterprise = null,
    bool? Phrase = null,
    bool? Case = null,
    NumericOption? Numeric = null,
    bool? Math = null,
    int? MinLength = null,
    int? MaxLength = null,
    string? Comment = null,
    string? ImageFilePath = null,
    int MaxPageLoadWaitingTimeInMilliseconds = 5000, 
    bool? IsInvisible = null,
    Dictionary<string, string>? EnterprisePayload = null,
    int MaxTimeOutTimeInSeconds = 300,
    bool ShouldResetCookiesBeforeAdd = false)
{
}