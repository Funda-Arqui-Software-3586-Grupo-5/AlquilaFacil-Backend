using AlquilaFacilPlatform.Subscriptions.Domain.Model.Aggregates;
using AlquilaFacilPlatform.Subscriptions.Domain.Model.Queries;
using AlquilaFacilPlatform.Subscriptions.Domain.Model.ValueObjects;
using AlquilaFacilPlatform.Subscriptions.Domain.Services;

namespace AlquilaFacilPlatform.Subscriptions.Interfaces.ACL.Service;

public class SubscriptionContextFacade(ISubscriptionQueryServices subscriptionQueryServices) : ISubscriptionContextFacade
{
    

    public async Task<IEnumerable<Subscription>> GetSubscriptionByUsersId(List<int> usersId)
    {
        var query = new GetSubscriptionsByUserIdQuery(usersId);
        var subscriptions = await subscriptionQueryServices.Handle(query);  
        return subscriptions;
    }

    public async Task<bool> IsUserSubscribed(int userId)
    {
        var query = new GetSubscriptionByUserIdQuery(userId);
        var subscription = await subscriptionQueryServices.Handle(query);
        if (subscription == null)
        {
            throw new Exception("Subscription not found");
        }
        return subscription.SubscriptionStatusId == (int)ESubscriptionStatus.Active;

    }
}