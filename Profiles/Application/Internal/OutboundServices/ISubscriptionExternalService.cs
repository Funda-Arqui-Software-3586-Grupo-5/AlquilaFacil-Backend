namespace AlquilaFacilPlatform.Profiles.Application.Internal.OutboundServices;

public interface ISubscriptionExternalService
{
    Task<bool> IsUserSubscribeAsync(int userId);
}