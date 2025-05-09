using Notifications.Domain.Models.Aggregates;
using Notifications.Domain.Models.Commands;
using Notifications.Domain.Repositories;
using Notifications.Domain.Services;
using Notifications.Shared.Domain.Repositories;

namespace Notifications.Application.Internal.CommandServices;

public class NotificationCommandService(IUnitOfWork unitOfWork, INotificationRepository notificationRepository) : INotificationCommandService
{
    public async Task<Notification> Handle(CreateNotificationCommand command)
    {
        var notification = new Notification(command);
        await notificationRepository.AddAsync(notification);
        await unitOfWork.CompleteAsync();
        return notification;
    }

    public async Task<Notification> Handle(DeleteNotificationCommand command)
    {
        var notification = await notificationRepository.FindByIdAsync(command.Id);
        if (notification == null)
        {
            throw new Exception("Notification not found");
        }
        notificationRepository.Remove(notification);
        await unitOfWork.CompleteAsync();
        return notification;
    }
}