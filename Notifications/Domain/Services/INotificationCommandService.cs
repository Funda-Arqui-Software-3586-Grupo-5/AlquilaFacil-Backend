using Notifications.Domain.Models.Aggregates;
using Notifications.Domain.Models.Commands;

namespace Notifications.Domain.Services;

public interface INotificationCommandService
{
    Task<Notification> Handle(CreateNotificationCommand command);
    Task<Notification> Handle(DeleteNotificationCommand command);
}