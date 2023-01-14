namespace Selenium.FramesSearcher.Extensions.Exceptions;

public class MultipleHtmlRootElementFoundWhileTraversingSiteException : ArgumentException
{
    
}

public class CouldNotTraverseToFrameException : ArgumentException
{
    
}


public class CouldNotBuildFramesTree : ArgumentException
{

    public CouldNotBuildFramesTree()
    {
        
    }
    public CouldNotBuildFramesTree(Exception innerException) : base($"{nameof(CouldNotBuildFramesTree)}", innerException)
    {
    }
}
