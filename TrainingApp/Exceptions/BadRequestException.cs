namespace TrainingApp.Exceptions;

public class BadRequestException: Exception
{
    public Dictionary<String, List<String>> Errors { get; set; }

    public BadRequestException(Dictionary<string, List<string>> errors) : base("")
    {
        Errors = errors;
    }
}