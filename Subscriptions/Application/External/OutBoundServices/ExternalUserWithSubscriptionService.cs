using Subscriptions.Interfaces.ACL;

namespace Subscriptions.Application.Internal.OutBoundServices;

public class ExternalUserWithSubscriptionService(IIamContextFacade iamContextFacade) : IExternalUserWithSubscriptionService
{
    public bool UserExists(int id)
    {
        return iamContextFacade.UsersExists(id);
    }
} 