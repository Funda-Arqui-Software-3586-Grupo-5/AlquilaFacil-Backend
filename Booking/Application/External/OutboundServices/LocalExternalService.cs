//using AlquilaFacilPlatform.Locals.Domain.Model.Aggregates;
//using AlquilaFacilPlatform.Locals.Interfaces.ACL;

using Booking.Interfaces.ACL;
using Booking.Interfaces.ACL.DTOs;

namespace Booking.Application.External.OutboundServices;

public class LocalExternalService : ILocalExternalService
{
    private readonly ILocalsContextFacade _localsContextFacade;

    public LocalExternalService(ILocalsContextFacade localsContextFacade)
    {
        _localsContextFacade = localsContextFacade;
    }

    public Task<bool> LocalReservationExists(int reservationId) =>
        _localsContextFacade.LocalExists(reservationId);

    public Task<IEnumerable<LocalDto>> GetLocalsByUserId(int userId) =>
        _localsContextFacade.GetLocalsByUserId(userId);

    public Task<bool> IsLocalOwner(int userId, int localId) =>
        _localsContextFacade.IsLocalOwner(userId, localId);
}
