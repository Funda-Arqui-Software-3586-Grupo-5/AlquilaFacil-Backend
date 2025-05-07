using AlquilaFacilPlatform.IAM.Domain.Model.Aggregates;

namespace AlquilaFacilPlatform.Subscriptions.Application.Internal.OutBoundServices;

public interface IExternalUserWithSubscriptionService
{
    bool UserExists(int id);
}