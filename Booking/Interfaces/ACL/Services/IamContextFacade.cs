namespace Booking.Interfaces.ACL.Services;


public class IamContextFacade(HttpClient httpClient) : IIamContextFacade
{
    public async Task<bool> UserExists(int userId)
    {
        var response = await httpClient.GetAsync($"/api/v1/users/{userId}");
        return response.IsSuccessStatusCode;
    }
}