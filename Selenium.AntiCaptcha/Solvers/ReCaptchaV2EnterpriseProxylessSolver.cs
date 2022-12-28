﻿using AntiCaptchaApi.Net.Requests;
using AntiCaptchaApi.Net.Requests.Abstractions.Interfaces;
using OpenQA.Selenium;
using Selenium.AntiCaptcha.Models;
using Selenium.AntiCaptcha.Solvers.Base;

namespace Selenium.AntiCaptcha.Solvers
{
    internal class ReCaptchaV2EnterpriseProxylessSolver : RecaptchaSolverBase <IRecaptchaV2EnterpriseProxylessRequest>
    {
        protected override IRecaptchaV2EnterpriseProxylessRequest BuildRequest(SolverArguments arguments)
        {
            return new RecaptchaV2EnterpriseProxylessRequest(arguments);
        }

        public ReCaptchaV2EnterpriseProxylessSolver(string clientKey, IWebDriver driver, SolverConfig solverConfig) : base(clientKey, driver, solverConfig)
        {
        }
    }
}
