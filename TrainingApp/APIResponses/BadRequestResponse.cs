using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace TrainingApp.APIResponses;

public class BadRequestResponse
{
    [JsonPropertyName("errors")]
    public Dictionary<string, List<String>> Errors { get; set; }
}