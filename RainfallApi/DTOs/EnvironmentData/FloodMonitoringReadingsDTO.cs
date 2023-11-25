using System.Text.Json.Serialization;

namespace RainfallApi.DTOs.EnvironmentData;

public class FloodMonitoringReadingsDTO
{
    [JsonPropertyName("items")]
    public ReadingDTO[]? Items { get; set; }
}
