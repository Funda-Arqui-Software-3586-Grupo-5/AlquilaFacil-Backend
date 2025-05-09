//using AlquilaFacilPlatform.Locals.Domain.Model.Aggregates;

namespace Booking.Application.External;

public interface ILocalExternalService
{
    Task<bool> LocalReservationExists(int reservationId);
    //Task<IEnumerable<Local?>> GetLocalsByUserId(int userId);
    Task<bool> IsLocalOwner(int userId, int localId);
}