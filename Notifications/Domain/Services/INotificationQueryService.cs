using Notifications.Domain.Models.Aggregates;
using Notifications.Domain.Models.Queries;

namespace Notifications.Domain.Services;

public interface INotificationQueryService
{
    Task<IEnumerable<Notification>> Handle(GetNotificationsByUserIdQuery query);
}