using Subscriptions.Domain.Model.Commands;
using Subscriptions.Domain.Model.Queries;
using Subscriptions.Domain.Services;
using Subscriptions.Interfaces.REST.Resources;
using Subscriptions.Interfaces.REST.Transform;
using Microsoft.AspNetCore.Mvc;
using Subscriptions.Interfaces.ACL;

namespace Subscriptions.Interfaces.REST;

[ApiController]
[Route("api/v1/[controller]")]
public class SubscriptionsController(
    ISubscriptionContextFacade subscriptionContextFacade,
    ISubscriptionCommandService subscriptionCommandService,
    ISubscriptionQueryServices subscriptionQueryServices)
    : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> CreateSubscription(
        [FromBody] CreateSubscriptionResource createSubscriptionResource)
    {
        var createSubscriptionCommand =
            CreateSubscriptionCommandFromResourceAssembler.ToCommandFromResource(createSubscriptionResource);
        var subscription = await subscriptionCommandService.Handle(createSubscriptionCommand);
        if (subscription is null) return BadRequest();
        var resource = SubscriptionResourceFromEntityAssembler.ToResourceFromEntity(subscription);
        
        return CreatedAtAction(nameof(GetSubscriptionById), new { subscriptionId = resource.Id }, resource);
    }
    
    [HttpGet]
    public async Task<IActionResult> GetAllTutorials()
    {
        var getAllSubscriptionsQuery = new GetAllSubscriptionsQuery();
        var subscriptions = await subscriptionQueryServices.Handle(getAllSubscriptionsQuery);
        var resources = subscriptions.Select(SubscriptionResourceFromEntityAssembler.ToResourceFromEntity);
        return Ok(resources);
    }

    [HttpGet("{userId:int}/subscribed")]

    public async Task<IActionResult> IsUserSubscribed(int userId)
    {
        var response = await subscriptionContextFacade.IsUserSubscribed(userId);
        return Ok(response);
    }
    
    [HttpGet("subscriptions/by-users")]
    public async Task<IActionResult> GetSubscriptionsByUsers([FromQuery] List<int> usersId)
    {
        var subscriptions = await subscriptionContextFacade.GetSubscriptionByUsersId(usersId);
        var resources = subscriptions.Select(SubscriptionResourceFromEntityAssembler.ToResourceFromEntity);
        return Ok(resources);
    }
    
    
    
    [HttpGet("{subscriptionId}")]
    public async Task<IActionResult> GetSubscriptionById([FromRoute] int subscriptionId)
    {
        var subscription = await subscriptionQueryServices.Handle(new GetSubscriptionByIdQuery(subscriptionId));
        if (subscription == null) return NotFound();
        var resource = SubscriptionResourceFromEntityAssembler.ToResourceFromEntity(subscription);
        return Ok(resource);
    }

    [HttpPut("{subscriptionId}/{subscriptionStatusId}")]
    public async Task<IActionResult> UpdateSubscriptionStatus(int subscriptionId, int subscriptionStatusId)
    {
        var updateSubscriptionStatusCommand = new UpdateSubscriptionStatusCommand(subscriptionId, subscriptionStatusId);
        var subscription = await subscriptionCommandService.Handle(updateSubscriptionStatusCommand);
        if (subscription == null) return NotFound();
        var resource = SubscriptionResourceFromEntityAssembler.ToResourceFromEntity(subscription);
        return Ok(resource);
    }
   
}