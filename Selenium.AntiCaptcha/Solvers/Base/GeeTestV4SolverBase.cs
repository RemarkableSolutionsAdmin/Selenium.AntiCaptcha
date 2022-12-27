﻿using AntiCaptchaApi.Net.Models.Solutions;
using AntiCaptchaApi.Net.Requests.Abstractions;
using OpenQA.Selenium;
using Selenium.AntiCaptcha.Models;

namespace Selenium.AntiCaptcha.Solvers.Base;

public abstract class GeeTestV4SolverBase<TRequest> : GeeSolverBase <TRequest, GeeTestV4Solution>
    where TRequest : CaptchaRequest<GeeTestV4Solution>
{
    protected GeeTestV4SolverBase(string clientKey, IWebDriver driver, SolverConfig solverConfig) : base(clientKey, driver, solverConfig)
    {
    }
}