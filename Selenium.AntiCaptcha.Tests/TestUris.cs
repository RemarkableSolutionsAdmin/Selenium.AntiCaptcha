namespace Selenium.AntiCaptcha.Tests;

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
        }
    }
}