using System.Text.Json.Serialization;

namespace RainfallApi.DTOs.Error;

public class ErrorResponse
{
    [JsonPropertyName("message")]
    public string? Message { get; set; }

    [JsonPropertyName("detail")]
    public IEnumerable<ErrorDetail>? Detail { get; set; }
}

