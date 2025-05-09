using Notifications.Domain.Models.Aggregates;
using Notifications.Domain.Models.Queries;
using Notifications.Domain.Repositories;
using Notifications.Domain.Services;

namespace Notifications.Application.Internal.QueryServices;

public class NotificationQueryService(INotificationRepository notificationRepository) : INotificationQueryService
{
    public async Task<IEnumerable<Notification>> Handle(GetNotificationsByUserIdQuery query)
    {
        return await notificationRepository.GetNotificationsByUserId(query.UserId);
    }
}