namespace Subscriptions.Application.Internal.OutBoundServices;

public interface IExternalUserWithSubscriptionService
{
    bool UserExists(int id);
}