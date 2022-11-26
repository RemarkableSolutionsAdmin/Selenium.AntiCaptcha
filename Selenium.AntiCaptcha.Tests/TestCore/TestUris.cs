using Selenium.AntiCaptcha.Enums;

namespace Selenium.Anticaptcha.Tests.TestCore;


public static class TestUris
{
    public static IEnumerable<object[]> Uris() => 
        Recaptcha.Uris()
            .Concat(FunCaptcha.Uris())
            .Concat(ImageToText.Uris())
            .Concat(HCaptcha.Uris())
            .Concat(AntiGate.Uris())
            .Concat(GeeTest.Uris());

    public static IEnumerable<object[]> TestableUris() =>        
        Recaptcha.Uris()
            .Concat(FunCaptcha.Uris())
            .Concat(HCaptcha.Uris())
            .Concat(GeeTest.Uris());

    public static IEnumerable<object[]> TestableUrisWithoutRecaptcha() =>        
        FunCaptcha.Uris()
            .Concat(HCaptcha.Uris())
            .Concat(GeeTest.Uris());

    public class Recaptcha
    {
        public static IEnumerable<object[]> Uris() => V3.Uris().Concat(V2.Uris());
        public static class V3
        {
            public static IEnumerable<object[]> Uris() => Enterprise.Uris().Concat(NonEnterprise.Uris());

            public static class Enterprise
            {
                public const CaptchaType Type = CaptchaType.ReCaptchaV3Enterprise;
                public static IEnumerable<object[]> Uris()
                {
                    yield return new object[] { new CaptchaUri(W1, Type) };
                    yield return new object[] { new CaptchaUri(W2, Type) };
                }
                public const string W1 = "https://www.netflix.com/pl-en/login";
                public const string W2 = "https://squareup.com/signup";
            }

            public static class NonEnterprise
            {
                public const CaptchaType Type = CaptchaType.ReCaptchaV3Proxyless;
                public static IEnumerable<object[]> Uris()
                {
                    yield return new object[] { new CaptchaUri(W1, Type) };
                    yield return new object[] { new CaptchaUri(W2, Type) };
                }
                public const string W1 = "https://recaptcha-demo.appspot.com/recaptcha-v3-request-scores.php";
                public const string W2 = "https://www.twilio.com/go/twilio-brand-sales-1";
            }
        }

        public static class V2
        {
            public static IEnumerable<object[]> Uris() => Enterprise.Uris().Concat(NonEnterprise.Uris());

            public static class Enterprise
            {
                public const CaptchaType Type = CaptchaType.ReCaptchaV2EnterpriseProxyless;
                public static IEnumerable<object[]> Uris()
                {
                    yield return new object[] { new CaptchaUri(W1, Type) };
                }
                public const string W1 = "https://store.steampowered.com/join";
            }

            public static class NonEnterprise
            {
                public const CaptchaType Type = CaptchaType.ReCaptchaV2Proxyless;

                public static IEnumerable<object[]> Uris()
                {
                    yield return new object[] { new CaptchaUri(W1, Type) };
                    yield return new object[] { new CaptchaUri(W2, Type) };
                    yield return new object[] { new CaptchaUri(W3, Type) };
                    yield return new object[] { new CaptchaUri(W4, Type) };
                }
                public const string W1 = "https://recaptcha-demo.appspot.com/recaptcha-v2-checkbox.php";
                public const string W2 = "http://antigate.com/logintest.php";
                public const string W3 = "https://rescan.io/join/";
                public const string W4 = "https://recaptcha-demo.appspot.com/recaptcha-v2-invisible.php";
            }
        }
    }
    public static class FunCaptcha
    {
        public const CaptchaType Type = CaptchaType.FunCaptchaProxyless;
        public static IEnumerable<object[]> Uris()
        {
            yield return new object[] { new CaptchaUri(W1, Type) };
            yield return new object[] { new CaptchaUri(W2, Type) };
            yield return new object[] { new CaptchaUri(W3, Type) };
        }
        public const string W1 = "https://api.funcaptcha.com/fc/api/nojs/?pkey=69A21A01-CC7B-B9C6-0F9A-E7FA06677FFC";
        public const string W2 = "https://www.roblox.com/";
        public const string W3 = "https://github.com/signup?ref_cta=Sign+up&ref_loc=header+logged+out&ref_page=%2F&source=header-home";
    }

    public static class GeeTest
    {
        public static IEnumerable<object[]> Uris() => V4.Uris().Concat(V3.Uris());
        public static class V4
        {
            public const CaptchaType Type = CaptchaType.GeeTestV4Proxyless;
            public static IEnumerable<object[]> Uris()
            {
                yield return new object[] { new CaptchaUri(W1, Type) };
            }
            public const string W1 = "https://www.geetest.com/en/adaptive-captcha-demo";
        }

        public static class V3
        {
            public const CaptchaType Type = CaptchaType.GeeTestV3Proxyless;
            public static IEnumerable<object[]> Uris()
            {
                yield return new object[] { new CaptchaUri(W1, Type) };
                //yield return new object[] { new CaptchaUri(W2, Type) };
                yield return new object[] { new CaptchaUri(W3, Type) };
            }
            public const string W1 = "https://www.geetest.com/en/demo";
            public const string W2 = "https://www.seloger.com/";
            public const string W3 = "https://zhiyou.smzdm.com/user/login?redirect_to=http%3A%2F%2Fzhiyou.smzdm.com%2Fuser";
        }
    }

    public static class ImageToText
    {
        public const CaptchaType Type = CaptchaType.ImageToText;
        public static IEnumerable<object[]> Uris()
        {
            yield return new object[] { new CaptchaUri(W1, Type) };
            yield return new object[] { new CaptchaUri(W2, Type) };
        }
        public const string W1 = "https://en.wikipedia.org/w/index.php?title=Special:CreateAccount&returnto=Main+Page";
        public const string W2 = "https://democaptcha.com/demo-form-eng/image.html";
    }

    public static class AntiGate
    {
        public const CaptchaType Type = CaptchaType.AntiGate;                
        public static IEnumerable<object[]> Uris()
        {
            yield return new object[] { new CaptchaUri(W1, Type) };
        }
        public const string W1 = "https://anti-captcha.com/tutorials/v2-textarea";
    }

    public static class HCaptcha
    {
        public const CaptchaType Type = CaptchaType.HCaptchaProxyless;         
        public static IEnumerable<object[]> Uris()
        {
            //yield return new object[] { new CaptchaUri(W1, Type) };
            yield return new object[] { new CaptchaUri(W2, Type) };
        }
        public const string W1 = "https://entwickler.ebay.de/signin?tab=register";
        public const string W2 = "https://www.chartboost.com/sign-up/";
    }
    
}