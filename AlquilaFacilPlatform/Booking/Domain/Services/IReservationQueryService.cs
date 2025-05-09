using AlquilaFacilPlatform.Booking.Domain.Model.Aggregates;
using AlquilaFacilPlatform.Booking.Domain.Model.Queries;
//using AlquilaFacilPlatform.Subscriptions.Domain.Model.Aggregates;

namespace AlquilaFacilPlatform.Booking.Domain.Services;

public interface IReservationQueryService
{
   
    Task<IEnumerable<Reservation>> GetReservationsByUserIdAsync(GetReservationsByUserId query);
    Task<IEnumerable<Reservation>>GetReservationByStartDateAsync(GetReservationByStartDate query);
    Task<IEnumerable<Reservation>> GetReservationByEndDateAsync(GetReservationByEndDate query);
    
    Task<IEnumerable<Reservation>> GetReservationsByOwnerIdAsync(GetReservationsByOwnerIdQuery query);
    
}