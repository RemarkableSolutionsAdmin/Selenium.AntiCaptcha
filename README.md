# Selenium.AntiCaptcha

### Selenium.AntiCaptcha is an extension library for Selenium Web Driver written in .NET 6. 

Selenium.AntiCaptcha uses anti-captcha.com API to solve captchas. To use it you will need your ClientKey from anti-captcha service.

## LICENSE

MIT - see LICENSE

## INFO

### Adding AntiCaptchaApi.Net to your project

Simply install the nuget package via

`Install-Package Selenium.AntiCaptcha`

### Contributing

1. Clone
1. Branch
1. Make changes
1. Push
1. Make a pull request

### Source

1. Clone the source down to your machine.
   `git clone https://github.com/RemarkableSolutionsAdmin/Selenium.AntiCaptcha.git`
   
# REQUIREMENTS

To run the build, a Visual Studio 2022 compatible environment should be setup.

## Usage

There are 2 main ways of using Selenium.AntiCaptcha:
- Generic solution for known expected captcha type
- Non-generic solving

Although Selenium.AntiCaptcha is capable of identifying captchas and finding required elements on a website,  its better to provide as much information as possible to minimize chances of failure. 
We recommend getting familiar with type **SolverAdditionalArguments**

### Generic
To use this method you have to know what type of captcha you will be solving. As result you will be given concrete type of solution.

```csharp
        var solverAdditionalArguments = new SolverAdditionalArguments
        {
            CaptchaType = CaptchaType.HCaptchaProxyless,
        };
        var result = await Driver.SolveCaptchaAsync<HCaptchaSolution>(ClientKey, solverAdditionalArguments);
```
        
     
### Non-Generic
More flexible approach. Identification of captchas might not always work

```csharp
        var solverAdditionalArguments = new SolverAdditionalArguments
        {
            CaptchaType = CaptchaType.ImageToText,
            ImageElement = Driver.FindElement(By.XPath("//img[contains(@class, 'captcha')]"))
        };
        
        var result = await Driver.SolveCaptchaAsync(ClientKey, solverAdditionalArguments);
```

#### SolverAdditionalArguments

```csharp
public record SolverAdditionalArguments(
    CaptchaType? CaptchaType = null,                //Provide it if you know what captcha to expect
    string? Url = null,                             //The Url where you are trying to solve your captcha
    string? SiteKey = null,                         //SiteKey should be unique per website. If you know what it is you can provide it explicitly otherwise it will be scrapped by captcha solver
    IWebElement? ResponseElement = null,            //This elements has to be filled in before submitting form on a website. 
    IWebElement? SubmitElement = null,              //Element used to submit your form
    IWebElement? ImageElement = null,               //Element where picture for ImageToText captcha resides
    string? UserAgent = null,
    ProxyConfig? ProxyConfig = null,                //Used for captchas requiring Proxies
    string? FunCaptchaApiJsSubdomain = null,
    string? Data = null,
    JObject? Variables = null,
    string? TemplateName = null,
    List<string>? DomainsOfInterest = null,
    string? GeetestApiServerSubdomain = null,
    string? GeetestGetLib = null,
    string? Challenge = null,
    string? Gt = null,                             //Gt is SiteKey for GeeTest captchas
    Dictionary<string, string>? InitParameters = null,
    double? MinScore = null,
    string? PageAction = null,
    string? ApiDomain = null,
    bool? IsEnterprise = null,
    bool? Phrase = null,
    bool? Case = null,
    NumericOption? Numeric = null,
    int? Math = null,
    int? MinLength = null,
    int? MaxLength = null,
    string? Comment = null,
    string? ImageFilePath = null,
    int MaxPageLoadWaitingTimeInMilliseconds = 5000,
    bool? IsInvisible = null,
    Dictionary<string, string>? EnterprisePayload = null,
    int MaxTimeOutTimeInSeconds = 300)
{
}
```

# CREDITS

Copyright (c) 2022 Remarkable Solutions
