using LocalManagement.Application.External.OutboundServices;

namespace LocalManagement.Infrastructure.IAM;

public class UserCommentExternalService(HttpClient httpClient, IConfiguration configuration) : IUserCommentExternalService
{

    public async Task<bool> UserExists(int userId)
    {
        var endpoint = $"http://localhost:8012/api/v1/users/{userId}/exists";
        var response = await httpClient.GetAsync(endpoint);
        return response.IsSuccessStatusCode;

    }
}