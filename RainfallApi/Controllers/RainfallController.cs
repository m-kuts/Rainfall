using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using RainfallApi.Services.Contracts;
using RainfallApi.DTOs.Error;
using RainfallApi.DTOs.Rainfall;

namespace RainfallApi.Controllers;

[ApiController]
[Route("[controller]")]
public class RainfallController : ControllerBase
{
    private readonly IRainfallService _rainfallService;
    private readonly ILogger<RainfallController> _logger;

    public RainfallController(IRainfallService rainfallService, ILogger<RainfallController> logger)
    {
        _rainfallService = rainfallService;
        _logger = logger;
    }

    [HttpGet("id/{stationId}/readings")]
    [ProducesResponseType(typeof(RainfallReadingsDTO), 200)]
    [ProducesResponseType(typeof(ErrorResponse), 400)]
    [ProducesResponseType(typeof(ErrorResponse), 404)]
    [ProducesResponseType(typeof(ErrorResponse), 500)]
    public async Task<RainfallReadingsDTO> GetStationReadings(
        [FromRoute] string stationId,
        [FromQuery, Range(1, int.MaxValue, ErrorMessage = "Count must be greater than 0")] int count = 10)
    {
        var readings = await _rainfallService.GetReadings(stationId, count);
        return readings;
    }
}

