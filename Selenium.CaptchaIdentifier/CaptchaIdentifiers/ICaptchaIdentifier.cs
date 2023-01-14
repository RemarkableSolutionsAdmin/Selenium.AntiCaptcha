﻿using AntiCaptchaApi.Net.Models;
using OpenQA.Selenium;
using Selenium.CaptchaIdentifier.Enums;

namespace Selenium.CaptchaIdentifier.CaptchaIdentifiers;

public interface ICaptchaIdentifier
{
    public bool CanIdentify(CaptchaType type);
    public Task<CaptchaType?> IdentifyInCurrentFrameAsync(IWebDriver driver, IWebElement? imageElement, ProxyConfig? proxyConfig,
        CancellationToken cancellationToken);
    public Task<CaptchaType?> IdentifyInAllFramesAsync(IWebDriver driver, IWebElement? imageElement, ProxyConfig? proxyConfig,
        CancellationToken cancellationToken);
    public Task<CaptchaType?> SpecifyCaptcha(CaptchaType originalType, IWebDriver driver, IWebElement? imageElement, ProxyConfig? proxyConfig, CancellationToken cancellationToken);
}