using Selenium.CaptchaIdentifier.Enums;

namespace Tests.Common.Core.Models;

public record CaptchaUri(string Uri, CaptchaType ExpectedType);