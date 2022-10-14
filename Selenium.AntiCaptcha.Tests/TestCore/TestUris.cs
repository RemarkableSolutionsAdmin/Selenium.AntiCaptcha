namespace Selenium.Anticaptcha.Tests.TestCore;

public static class TestUris
{
    public static class Recaptcha
    {
        public static class V3
        {
            public const string EnterpriseW1 = "https://www.netflix.com/pl-en/login";
            public const string NoEnterpriseW1 = "https://recaptcha-demo.appspot.com/recaptcha-v3-request-scores.php";
        }

        public static class V2
        {
            public const string W1 = "https://recaptcha-demo.appspot.com/recaptcha-v2-checkbox.php";
            public const string W2 = "http://antigate.com/logintest.php";
            public const string EnterpriseW1 = "https://store.steampowered.com/join";
            //https://www.twilio.com/go/twilio-brand-sales-1?utm_source=google&utm_medium=cpc&utm_term=twilio&utm_campaign=G_S_EMEA_Brand_Emerging_EN_RLSA&cq_plac=&cq_net=g&cq_pos=&cq_med=&cq_plt=gp&gclid=EAIaIQobChMIs5_QmOPg-gIVA18YCh1vaATpEAAYAiAAEgK4dvD_BwE
            //??
        }
    }
    public static class FunCaptcha
    {
        public const string W1 = "https://api.funcaptcha.com/fc/api/nojs/?pkey=69A21A01-CC7B-B9C6-0F9A-E7FA06677FFC";
        public const string W2 = "https://www.roblox.com/";
        public const string W3 = "https://github.com/signup?ref_cta=Sign+up&ref_loc=header+logged+out&ref_page=%2F&source=header-home";
    }

    public static class GeeTest
    {
        public static class V4
        {
            public const string W1 = "https://www.geetest.com/en/adaptive-captcha-demo";
        }

        public static class V3
        {
            public const string W1 = "https://www.geetest.com/en/demo";
            public const string W2 = "https://www.seloger.com/";
        }
    }

    public static class ImageToText
    {
        public const string W1 = "https://en.wikipedia.org/w/index.php?title=Special:CreateAccount&returnto=Main+Page";
    }

    public static class AntiGate
    {
        public const string W1 = "https://anti-captcha.com/tutorials/v2-textarea";
    }

    public static class HCaptcha
    {
        public const string W1 = "https://entwickler.ebay.de/signin?tab=register";
        public const string W2 = "https://www.chartboost.com/sign-up/";
    }
    
}