using AlquilaFacilPlatform.Subscriptions.Domain.Model.Commands;
using AlquilaFacilPlatform.Subscriptions.Domain.Model.Entities;
using AlquilaFacilPlatform.Subscriptions.Domain.Model.ValueObjects;

namespace AlquilaFacilPlatform.Subscriptions.Domain.Model.Aggregates;

public partial class Subscription
{
    public int Id { get; }
    
    public int UserId { get; set; }
    
    public int SubscriptionStatusId { get; set; }
    public int PlanId { get; set; }

    public Subscription()
    {
        PlanId = 0;
        UserId = 0;
    }

    public Subscription(CreateSubscriptionCommand command)
    {
        UserId = command.UserId;
        PlanId = command.PlanId;
        SubscriptionStatusId = 2;
    }
    
    
}