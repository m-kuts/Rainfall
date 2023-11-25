
using System.Net;
using FluentAssertions;
using Microsoft.Extensions.Configuration;
using Moq;
using RainfallApi.Exceptions;
using RainfallApi.Services.Implementations;

namespace Rainfall.Tests.Services;

public class TestRainfallService
{
	[Theory]
	[InlineData(1)]
	[InlineData(5)]
	public async Task GetReadings_ValidResponse_ReturnsRainfallReadingsDTO(int count)
	{
		// Arrange
		var inMemorySettings = new Dictionary<string, string> {
			{ "RainfallAPI:RootUrl", "http://environment.data.gov.uk/flood-monitoring"},
		};

		IConfiguration configuration = new ConfigurationBuilder()
			.AddInMemoryCollection(inMemorySettings)
			.Build();

		var rainfallService = new RainfallService(configuration);

		// Act
		var result = await rainfallService.GetReadings("E21234", count);

		// Assert
		Assert.NotNull(result);
		Assert.Equal(count, result.Readings.Count());
	}

	[Fact]
	public async Task GetReadings_NoReadingsFound_ThrowsNotFoundException()
	{
		// Arrange
		var inMemorySettings = new Dictionary<string, string> {
			{ "RainfallAPI:RootUrl", "http://environment.data.gov.uk/flood-monitoring"},
		};

		IConfiguration configuration = new ConfigurationBuilder()
			.AddInMemoryCollection(inMemorySettings)
			.Build();

		var rainfallService = new RainfallService(configuration);

		// Act and Assert
		var ex = await Assert.ThrowsAsync<StatusCodeException>(() => rainfallService.GetReadings("not_real_id", 5));
		Assert.Equal(HttpStatusCode.NotFound, ex.StatusCode);
	}

	[Fact]
	public async Task GetReadings_ConfigurationMissing_ThrowsConfigurationException()
	{
		// Arrange
		var inMemorySettings = new Dictionary<string, string> {
			{ "RainfallAPI:RootUrl", ""},
		};

		IConfiguration configuration = new ConfigurationBuilder()
			.AddInMemoryCollection(inMemorySettings)
			.Build();

		var rainfallService = new RainfallService(configuration);

		// Act and Assert
		var ex = await Assert.ThrowsAsync<Exception>(() => rainfallService.GetReadings("stationId", 5));
		Assert.Equal("Invalid URL", ex.Message);
	}

	// Add more test methods for other scenarios
}


