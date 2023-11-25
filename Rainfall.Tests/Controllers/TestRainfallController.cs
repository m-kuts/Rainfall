using Microsoft.Extensions.Logging;
using Moq;
using RainfallApi.Controllers;
using RainfallApi.DTOs.Rainfall;
using RainfallApi.Services.Contracts;

namespace Rainfall.Tests.Controllers;

public class TestRainfallController
{
    [Fact]
    public async Task GetStationReadings_OnSuccess_CheckResponse()
    {
		// Arrange
        var rainfallReadings = new RainfallReadingsDTO()
        {
            Readings = new RainfallReadingDTO[]
            {
                    new RainfallReadingDTO {DateMeasured = DateTime.Now, AmountMeasured = 0.2m }
            }
        };

        var mockRainfallService = new Mock<IRainfallService>();
        var mockLoggerService = new Mock<ILogger<RainfallController>>();
        mockRainfallService.Setup(service => service.GetReadings("", 10))
                           .ReturnsAsync(rainfallReadings);
        var rfc = new RainfallController(mockRainfallService.Object, mockLoggerService.Object);

		// Act
        var res = await rfc.GetStationReadings("", 10);

		// Assert
        Assert.Equal(res, rainfallReadings);

    }
}

