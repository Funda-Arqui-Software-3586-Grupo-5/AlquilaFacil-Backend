using AlquilaFacilPlatform.IAM.Interfaces.ACL;

namespace AlquilaFacilPlatform.Profiles.Application.Internal.OutboundServices;

public class UserExternalService(IIamContextFacade iamContextFacade) : IUserExternalService
{
    public bool UserExistsById(int userId)
    {
        return iamContextFacade.UsersExists(userId);
    }
}