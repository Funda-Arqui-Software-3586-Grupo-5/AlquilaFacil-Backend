using Booking.Interfaces.ACL.DTOs;

namespace Booking.Interfaces.ACL.Services;

public class SubscriptionContextFacade(HttpClient httpClient) : ISubscriptionContextFacade
{
    public async Task<IEnumerable<SubscriptionDto>> GetSubscriptionsByUsersId(List<int> usersId)
    {
        // suponiendo que el controller no tiene aún un endpoint por userId, este GET trae todos
        var subscriptions = await httpClient.GetFromJsonAsync<List<SubscriptionDto>>("/api/v1/subscriptions");

        return subscriptions?.Where(s => usersId.Contains(s.UserId)) ?? Enumerable.Empty<SubscriptionDto>();
    }
}