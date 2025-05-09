using System.Text.Json;
using Booking.Interfaces.ACL.DTOs;

namespace Booking.Interfaces.ACL.Services;

public class LocalsContextFacade : ILocalsContextFacade
{
    private readonly HttpClient _httpClient;

    public LocalsContextFacade(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<bool> LocalExists(int reservationId)
    {
        var response = await _httpClient.GetAsync($"/api/v1/locals/{reservationId}");
        response.EnsureSuccessStatusCode();
        return bool.Parse(await response.Content.ReadAsStringAsync());
    }

    public async Task<IEnumerable<LocalDto>> GetLocalsByUserId(int userId)
    {
        var response = await _httpClient.GetAsync($"/api/v1/locals/{userId}");
        response.EnsureSuccessStatusCode();
        var content = await response.Content.ReadAsStringAsync();

        return JsonSerializer.Deserialize<IEnumerable<LocalDto>>(content, new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        })!;
    }

    public async Task<bool> IsLocalOwner(int userId, int localId)
    {
        var response = await _httpClient.GetAsync($"/api/v1/locals/owner/{localId}");
        response.EnsureSuccessStatusCode();
        return bool.Parse(await response.Content.ReadAsStringAsync());
    }
}
