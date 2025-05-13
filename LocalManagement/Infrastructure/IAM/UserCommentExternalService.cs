using LocalManagement.Application.External.OutboundServices;

namespace LocalManagement.Infrastructure.IAM;

public class UserCommentExternalService(HttpClient httpClient, IConfiguration configuration) : IUserCommentExternalService
{

    public async Task<bool> UserExists(int userId)
    {
        var baseUrl = configuration["BaseUrl"];
        var endpoint = $"{baseUrl}/api/v1/auth/{userId}"; //Provitional endpoint
        var response = await httpClient.GetAsync(endpoint);
        return response.IsSuccessStatusCode;

    }
}