using AlquilaFacilPlatform.Subscriptions.Domain.Model.Commands;
using AlquilaFacilPlatform.Subscriptions.Domain.Model.Queries;
using AlquilaFacilPlatform.Subscriptions.Domain.Services;
using AlquilaFacilPlatform.Subscriptions.Interfaces.REST.Resources;
using AlquilaFacilPlatform.Subscriptions.Interfaces.REST.Transform;
using Microsoft.AspNetCore.Mvc;

namespace AlquilaFacilPlatform.Subscriptions.Interfaces.REST;

[ApiController]
[Route("api/v1/[controller]")]
public class SubscriptionsController(
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