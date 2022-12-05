﻿using AntiCaptchaApi.Net.Models;
using AntiCaptchaApi.Net.Models.Solutions;
using OpenQA.Selenium;
using Selenium.AntiCaptcha.Enums;
using Selenium.AntiCaptcha.Models;

namespace Selenium.AntiCaptcha.Internal;

public class AntiGateIdentifier : ProxyCaptchaIdentifier
{    
    public AntiGateIdentifier()
    {
        IdentifiableTypes.Add(CaptchaType.AntiGate);
    }


    public override async Task<CaptchaType?> IdentifyInCurrentFrameAsync(IWebDriver driver, SolverAdditionalArguments additionalArguments,
        CancellationToken cancellationToken)
    {
        return null; //TODO!
    }

    //TODO!
    public override async Task<CaptchaType?> SpecifyCaptcha(CaptchaType originalType, IWebDriver driver,
        SolverAdditionalArguments additionalArguments, CancellationToken cancellationToken)
    {
        return await base.SpecifyCaptcha(originalType, driver, additionalArguments, cancellationToken);
    }
}