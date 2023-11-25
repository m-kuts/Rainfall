using System.Text.Json.Serialization;
using RainfallApi.DTOs.EnvironmentData;

namespace RainfallApi.DTOs.Rainfall;

public class RainfallReadingDTO
{
    public RainfallReadingDTO() { }

    public RainfallReadingDTO(ReadingDTO readingDTO)
    {
        DateMeasured = readingDTO.DateTime;
        AmountMeasured = readingDTO.Value;
    }


    [JsonPropertyName("dateMeasured")]
    public DateTime DateMeasured { get; set; }

    [JsonPropertyName("amountMeasured")]
    public decimal AmountMeasured { get; set; }
}


