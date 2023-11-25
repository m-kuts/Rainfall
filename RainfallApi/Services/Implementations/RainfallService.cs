using System.Net;
using RainfallApi.DTOs.EnvironmentData;
using RainfallApi.DTOs.Rainfall;
using RainfallApi.Exceptions;
using RainfallApi.Services.Contracts;
using RainfallApi.Utils;

namespace RainfallApi.Services.Implementations;

public class RainfallService : IRainfallService
{
    private readonly IConfiguration _configuration;
    public RainfallService(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public async Task<RainfallReadingsDTO> GetReadings(string stationId, int count)
    {
        string requestUrl = $"{_configuration.GetValue<string>("RainfallAPI:RootUrl")}/id/stations/{stationId}/readings?_sorted&_limit={count}";

        using var httpReqUtil = new HttpRequestUtility();

        var readingsResponse = await httpReqUtil.GetAsync<FloodMonitoringReadingsDTO>(requestUrl);

        if (readingsResponse == null)
        {
            throw new StatusCodeException("Invalid request", HttpStatusCode.BadRequest);
        }

        if (readingsResponse.Items == null || readingsResponse.Items.Length == 0)
        {
            throw new StatusCodeException("No readings found for the specified stationId", HttpStatusCode.NotFound);
        }

        return new RainfallReadingsDTO(readingsResponse);
    }
}

