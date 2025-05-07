using AlquilaFacilPlatform.IAM.Interfaces.ACL;

namespace AlquilaFacilPlatform.Locals.Application.Internal.OutboundServices;

public class UserCommentExternalService (IIamContextFacade iamContextFacade) : IUserCommentExternalService
{
    public bool UserExists(int userId)
    {
        return iamContextFacade.UsersExists(userId);
    }    
}