namespace TrainingApp.Exceptions;

/// <summary>
/// Class representing 401 Unauthorized exception.
/// </summary>
public class UnauthorizedException: Exception
{
    public UnauthorizedException(string message) : base(message)
    {
        
    }
}