using Tests.Common.Config;

namespace Tests.Common.Core.Collections;

[CollectionDefinition(TestEnvironment.DriverBasedTestCollection)]
public class DriverBasedCollection : ICollectionFixture<WebDriverFixture>
{
    
}