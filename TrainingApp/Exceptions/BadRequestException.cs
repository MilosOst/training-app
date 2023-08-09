namespace TrainingApp.Exceptions;

public class BadRequestException: Exception
{
    public Dictionary<String, List<String>> Errors { get; set; }

    public BadRequestException(Dictionary<string, List<string>> errors) : base("")
    {
        Errors = errors;
    }

    /// <summary>
    /// Initialize a BadRequestException with a single error message.
    /// </summary>
    /// <param name="param">Parameter where error was found</param>
    /// <param name="errorMessage">Error message for given parameter</param>
    public BadRequestException(string param, string errorMessage) : base("")
    {
        this.Errors = new Dictionary<string, List<string>>();
        this.Errors.Add(param, new List<string>() { errorMessage });
    }
}