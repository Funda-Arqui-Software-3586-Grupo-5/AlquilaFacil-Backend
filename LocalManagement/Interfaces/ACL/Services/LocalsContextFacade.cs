using LocalManagement.Domain.Model.Aggregates;

namespace LocalManagement.Interfaces.ACL.Services;

public class LocalsContextFacade(HttpClient httpClient) : ILocalsContextFacade
{

    public async Task<bool> LocalExists(int localId)
    {
        var response = await httpClient.GetAsync($"/api/v1/locals/{localId}");
        if (!response.IsSuccessStatusCode)
        {
            throw new Exception("Local not found");
        }

        return true;
    }

    public async Task<IEnumerable<Local?>> GetLocalsByUserId(int userId)
    {
        var response = await httpClient.GetAsync($"/api/v1/locals/{userId}");
        if (!response.IsSuccessStatusCode)
        {
            throw new Exception("Error retrieving locals");
        }

        var locals = await response.Content.ReadFromJsonAsync<IEnumerable<Local>>();
        return locals!;
    }

    public async Task<bool> IsLocalOwner(int userId, int localId)
    {
        var response = await httpClient.GetAsync($"/api/locals/owner/{localId}");
        if (!response.IsSuccessStatusCode)
        {
            return false;
        }

        var isOwner = await response.Content.ReadFromJsonAsync<bool>();
        return isOwner;
    }
}