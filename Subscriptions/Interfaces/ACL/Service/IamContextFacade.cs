namespace Subscriptions.Interfaces.ACL.Service;

public class IamContextFacade(HttpClient _httpClient) : IIamContextFacade
{
    public bool UsersExists(int id)
    {
        var response = _httpClient.GetAsync($"api/users/{id}");
        return response.IsCompletedSuccessfully;
    }
}