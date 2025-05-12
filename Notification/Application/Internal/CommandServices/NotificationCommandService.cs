using Notification.Domain.Models.Commands;
using Notification.Domain.Repositories;
using Notification.Domain.Services;
using Notification.Shared.Domain.Repositories;
using Notification.Domain.Models.Aggregates;

namespace Notification.Application.Internal.CommandServices;

public class NotificationCommandService(IUnitOfWork unitOfWork, INotificationRepository notificationRepository) : INotificationCommandService
{
    public async Task<Domain.Models.Aggregates.Notification> Handle(CreateNotificationCommand command)
    {
        var notification = new Domain.Models.Aggregates.Notification(command);
        await notificationRepository.AddAsync(notification);
        await unitOfWork.CompleteAsync();
        return notification;
    }

    public async Task<Domain.Models.Aggregates.Notification> Handle(DeleteNotificationCommand command)
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