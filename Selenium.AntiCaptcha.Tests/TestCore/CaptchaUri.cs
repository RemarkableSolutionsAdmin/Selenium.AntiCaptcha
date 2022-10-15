using Selenium.AntiCaptcha.Enums;

namespace Selenium.Anticaptcha.Tests.TestCore;

public record CaptchaUri(string Uri, CaptchaType ExpectedType);