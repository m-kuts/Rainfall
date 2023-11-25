using System;
using System.Text.Json.Serialization;

namespace RainfallApi.DTOs.EnvironmentData;
public class ReadingDTO
{
    [JsonPropertyName("dateTime")]
    public DateTime DateTime { get; set; }

    [JsonPropertyName("value")]
    public decimal Value { get; set; }
}
