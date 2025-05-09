namespace Booking.Application.OutBoundService;

public interface IUserReservationExternalService
{
    bool UserExists(int userId);
}