using System.Text.Json.Serialization;

namespace RainfallApi.DTOs.Error;

public class ErrorDetail
{
    [JsonPropertyName("propertyName")]
    public string? PropertyName { get; set; }

    [JsonPropertyName("message")]
    public string? Message { get; set; }
}

