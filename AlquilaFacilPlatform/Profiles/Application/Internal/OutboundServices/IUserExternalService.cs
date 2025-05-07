namespace AlquilaFacilPlatform.Profiles.Application.Internal.OutboundServices;

public interface IUserExternalService
{
    bool UserExistsById(int userId);
}