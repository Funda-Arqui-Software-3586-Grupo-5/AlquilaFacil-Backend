using Notifications.Domain.Models.Aggregates;
using Notifications.Shared.Domain.Repositories;

namespace Notifications.Domain.Repositories;

public interface INotificationRepository : IBaseRepository<Notification>
{
    Task<IEnumerable<Notification>> GetNotificationsByUserId(int userId);
}