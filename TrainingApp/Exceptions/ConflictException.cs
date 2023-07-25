namespace TrainingApp.Exceptions;

/// <summary>
/// Class representing 409 Conflict Exception.
/// </summary>
public class ConflictException: Exception
{
    public ConflictException(string message) : base(message)
    {
        
    }
}
