using AntiCaptchaApi.Net.Enums;
using AntiCaptchaApi.Net.Models;
using Newtonsoft.Json.Linq;
using OpenQA.Selenium;
using Selenium.AntiCaptcha.Enums;

namespace Selenium.AntiCaptcha.Models;


public record AllPossibleSolverArguments(
    CaptchaType? CaptchaType = null,
    string? Url = null,
    string? SiteKey = null,
    IWebElement? ImageElement = null,
    string? UserAgent = null,
    TypedProxyConfig? ProxyConfig = null,
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
    bool? IsInvisible = null,
    Dictionary<string, string>? EnterprisePayload = null)
{
    
}

public record SolverArguments(
    CaptchaType? CaptchaType = null,
    string? Url = null,
    string? SiteKey = null,
    IWebElement? ImageElement = null,
    string? UserAgent = null,
    TypedProxyConfig? ProxyConfig = null,
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
    bool? IsInvisible = null,
    Dictionary<string, string>? EnterprisePayload = null)
{
    public CaptchaType? CaptchaType { get; set; } = CaptchaType;
    public string? Url { get; set; } = Url;
    public string? SiteKey { get; set; } = SiteKey;
    public IWebElement? ImageElement { get; set; } = ImageElement;
    public string? UserAgent { get; set; } = UserAgent;
    public TypedProxyConfig? ProxyConfig { get; set; } = ProxyConfig;
    public string? FunCaptchaApiJsSubdomain { get; set; } = FunCaptchaApiJsSubdomain;
    public string? Data { get; set; } = Data;
    public JObject? Variables { get; set; } = Variables;
    public string? TemplateName { get; set; } = TemplateName;
    public List<string>? DomainsOfInterest { get; set; } = DomainsOfInterest;
    public string? GeetestApiServerSubdomain { get; set; } = GeetestApiServerSubdomain;
    public string? GeetestGetLib { get; set; } = GeetestGetLib;
    public string? Challenge { get; set; } = Challenge;
    public string? Gt { get; set; } = Gt;
    public Dictionary<string, string>? InitParameters { get; set; } = InitParameters;
    public double? MinScore { get; set; } = MinScore;
    public string? PageAction { get; set; } = PageAction;
    public string? ApiDomain { get; set; } = ApiDomain;
    public bool? IsEnterprise { get; set; } = IsEnterprise;
    public bool? Phrase { get; set; } = Phrase;
    public bool? Case { get; set; } = Case;
    public NumericOption? Numeric { get; set; } = Numeric;
    public bool? Math { get; set; } = Math;
    public int? MinLength { get; set; } = MinLength;
    public int? MaxLength { get; set; } = MaxLength;
    public string? Comment { get; set; } = Comment;
    public string? ImageFilePath { get; set; } = ImageFilePath;
    public bool? IsInvisible { get; set; } = IsInvisible;
    public Dictionary<string, string>? EnterprisePayload { get; set; } = EnterprisePayload;
}