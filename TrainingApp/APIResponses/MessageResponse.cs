using System.Text.Json.Serialization;

namespace TrainingApp.APIResponses;

public class MessageResponse
{
    [JsonPropertyName("message")]
    public string Message { get; set; } = string.Empty;
}