using Microsoft.AspNetCore.Mvc.Testing;
using System.Net;
using System.Net.Http.Json;
using Xunit;
using YetAnotherUnfinishedProject.API.Models;

namespace YetAnotherUnfinishedProject.API.IntegrationTests.Features;

public class WeatherForecastTests(WebApplicationFactory<Program> webApplicationFactory) : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly HttpClient _httpClient = webApplicationFactory.CreateDefaultClient();

    [Fact]
    public async Task GetWeatherForecast_ReturnsSuccessStatusCode()
    {
        // Act
        var response = await _httpClient.GetAsync("/WeatherForecast");

        // Assert
        response.EnsureSuccessStatusCode();
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }

    [Fact]
    public async Task GetWeatherForecast_ReturnsCorrectContentType()
    {
        // Act
        var response = await _httpClient.GetAsync("/WeatherForecast");

        // Assert
        response.EnsureSuccessStatusCode();
        Assert.Equal("application/json; charset=utf-8", response.Content.Headers.ContentType?.ToString());
    }

    [Fact]
    public async Task GetWeatherForecast_ReturnsArrayOfWeatherForecast()
    {
        // Act
        var response = await _httpClient.GetFromJsonAsync<WeatherForecast[]>("/WeatherForecast");

        // Assert
        Assert.NotNull(response);
        Assert.Equal(5, response.Length);
    }

    [Fact]
    public async Task GetWeatherForecast_ReturnsDifferentDataEachTime()
    {
        // Act
        var response1 = await _httpClient.GetFromJsonAsync<WeatherForecast[]>("/WeatherForecast");
        var response2 = await _httpClient.GetFromJsonAsync<WeatherForecast[]>("/WeatherForecast");

        // Assert
        Assert.NotNull(response1);
        Assert.NotNull(response2);
        Assert.NotEqual(response1, response2);
    }
}
