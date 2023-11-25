using System.Text.Json.Serialization;
using RainfallApi.DTOs.EnvironmentData;

namespace RainfallApi.DTOs.Rainfall
{
    public class RainfallReadingsDTO
    {
        public RainfallReadingsDTO() { }
        public RainfallReadingsDTO(FloodMonitoringReadingsDTO readingsDTO)
        {
            Readings = readingsDTO.Items?.Select(i => new RainfallReadingDTO(i));
        }

        [JsonPropertyName("readings")]
        public IEnumerable<RainfallReadingDTO>? Readings { get; set; }
    }
}

