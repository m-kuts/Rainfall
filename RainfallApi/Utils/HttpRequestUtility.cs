using System.Text.Json;
using RainfallApi.Exceptions;

namespace RainfallApi.Utils;
public class HttpRequestUtility : IDisposable
{
    private readonly HttpClient _httpClient;

    public HttpRequestUtility()
    {
        _httpClient = new HttpClient();
    }

    public async Task<T?> GetAsync<T>(string url)
    {
        if (!Uri.IsWellFormedUriString(url, UriKind.Absolute))
        {
            throw new Exception("Invalid URL");
        }

        HttpResponseMessage response = await _httpClient.GetAsync(url);
        return await HandleResponse<T>(response);
    }

    private async Task<T?> HandleResponse<T>(HttpResponseMessage response)
    {
        if (response.IsSuccessStatusCode)
        {
            using var stream = await response.Content.ReadAsStreamAsync();
            return await JsonSerializer.DeserializeAsync<T>(stream);
        }
        else
        {
            throw new StatusCodeException(response.ReasonPhrase ?? "Something went wrong.", response.StatusCode);
        }
    }

    public void Dispose()
    {
        _httpClient.Dispose();
    }
}

