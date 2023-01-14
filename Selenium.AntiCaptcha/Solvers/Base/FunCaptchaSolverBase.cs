﻿using AntiCaptchaApi.Net.Models.Solutions;
using AntiCaptchaApi.Net.Requests.Abstractions.Interfaces;
using OpenQA.Selenium;
using Selenium.AntiCaptcha.Models;
using Selenium.FramesSearcher.Extensions;

namespace Selenium.AntiCaptcha.Solvers.Base;

public abstract class FunCaptchaSolverBase<TRequest> : Solver<TRequest, FunCaptchaSolution> 
    where TRequest : ICaptchaRequest<FunCaptchaSolution>
{
    protected override string GetSiteKey()
    {
        try
        {
            return Driver.FindElement(By.Id("funcaptcha")).GetAttribute("data-pkey");
        }
        catch (Exception)
        {
            // ignore
        }
 
        return PageSourceSearcher.FindFunCaptchaSiteKey(Driver);
    }

    protected FunCaptchaSolverBase(string clientKey, IWebDriver driver, SolverConfig solverConfig) : base(clientKey, driver, solverConfig)
    {
    }
}