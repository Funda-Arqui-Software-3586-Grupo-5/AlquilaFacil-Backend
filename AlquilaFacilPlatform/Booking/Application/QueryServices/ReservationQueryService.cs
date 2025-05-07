using AlquilaFacilPlatform.Booking.Application.OutBoundService;
using AlquilaFacilPlatform.Booking.Domain.Model.Aggregates;
using AlquilaFacilPlatform.Booking.Domain.Model.Queries;
using AlquilaFacilPlatform.Booking.Domain.Repositories;
using AlquilaFacilPlatform.Booking.Domain.Services;

namespace AlquilaFacilPlatform.Booking.Application.QueryServices;

public class ReservationQueryService(IReservationRepository reservationRepository, IReservationLocalExternalService reservationLocalExternalService) : IReservationQueryService
{
    public async Task<IEnumerable<Reservation>> GetReservationsByUserIdAsync(GetReservationsByUserId query)
    {
        return await reservationRepository.GetReservationsByUserIdAsync(query.UserId);
    }

    public async Task<IEnumerable<Reservation>> GetReservationByStartDateAsync(GetReservationByStartDate query)
    {
        return await reservationRepository.GetReservationByStartDateAsync(query.StartDate);
    }

    public async Task<IEnumerable<Reservation>> GetReservationByEndDateAsync(GetReservationByEndDate query)
    {
        return await reservationRepository.GetReservationByEndDateAsync(query.EndDate);
    }

    public async Task<IEnumerable<Reservation>> GetReservationsByOwnerIdAsync(GetReservationsByOwnerIdQuery query)
    {
        var locals = await reservationLocalExternalService.GetLocalsByUserId(query.OwnerId);
        if (locals == null)
        {
            throw new Exception("This user does not have any local registered.");
        }
        var localIds = locals.Select(local => local.Id);
        return await reservationRepository.GetReservationsByLocalIdAsync(localIds.ToList());
    }
}