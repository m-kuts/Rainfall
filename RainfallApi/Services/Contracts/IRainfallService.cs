using System;
using RainfallApi.DTOs.Rainfall;

namespace RainfallApi.Services.Contracts;

public interface IRainfallService
{
	Task<RainfallReadingsDTO> GetReadings(string stationId, int count);
}

