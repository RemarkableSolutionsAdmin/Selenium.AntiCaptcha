using Selenium.Anticaptcha.Tests.Core.Config;

namespace Selenium.Anticaptcha.Tests.Core.Collections;

[CollectionDefinition(TestEnvironment.DriverBasedTestCollection)]
public class DriverBasedCollection : ICollectionFixture<WebDriverFixture>
{
    
}