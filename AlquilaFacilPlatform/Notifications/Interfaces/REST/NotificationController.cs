using System.Net.Mime;
using AlquilaFacilPlatform.Notifications.Domain.Models.Commands;
using AlquilaFacilPlatform.Notifications.Domain.Models.Queries;
using AlquilaFacilPlatform.Notifications.Domain.Services;
using AlquilaFacilPlatform.Notifications.Interfaces.REST.Resources;
using AlquilaFacilPlatform.Notifications.Interfaces.REST.Transforms;
using Microsoft.AspNetCore.Mvc;

namespace AlquilaFacilPlatform.Notifications.Interfaces.REST;

[Produces(MediaTypeNames.Application.Json)]
[ApiController]
[Route("api/v1/[controller]")]
public class NotificationController(INotificationCommandService notificationCommandService, INotificationQueryService notificationQueryService) : ControllerBase
{

    [HttpPost]
    public async Task<IActionResult> SaveNotification([FromBody] CreateNotificationResource createNotificationResource)
    {
        var command = CreateNotificationCommandFromResourceAssembler.ToCommandFromResource(createNotificationResource);
        var notification = await notificationCommandService.Handle(command);
        var notificationResource = NotificationResourceFromEntityAssembler.ToResourceFromEntity(notification);
        return StatusCode(201, notificationResource);
    }
    
    [HttpGet("{userId}")]
    public async Task<IActionResult> GetNotificationsByUserId(int userId)
    {
        var query = new GetNotificationsByUserIdQuery(userId);
        var notifications = await notificationQueryService.Handle(query);
        var notificationResources = notifications.Select(NotificationResourceFromEntityAssembler.ToResourceFromEntity);
        return Ok(notificationResources);
    }
    
    [HttpDelete("{notificationId}")]
    public async Task<IActionResult> DeleteNotification(int notificationId)
    {
        var command = new DeleteNotificationCommand(notificationId);
        var notification = await notificationCommandService.Handle(command);
        return StatusCode(200, notification);
    }
}