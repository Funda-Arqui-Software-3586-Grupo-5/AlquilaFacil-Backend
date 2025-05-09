using Notifications.Domain.Models.Commands;
using Notifications.Interfaces.REST.Resources;

namespace Notifications.Interfaces.REST.Transforms;

public static class CreateNotificationCommandFromResourceAssembler
{
    public static CreateNotificationCommand ToCommandFromResource(CreateNotificationResource createNotificationResource)
    {
        return new CreateNotificationCommand(
            createNotificationResource.Title,
            createNotificationResource.Description,
            createNotificationResource.UserId
        );
    }
}