using OpenQA.Selenium;

namespace Selenium.AntiCaptcha.Models;

public record ActionArguments(
    IWebElement? SubmitElement = null,
    IWebElement? ResponseElement = null,
    bool ShouldResetCookiesBeforeAdd = false);