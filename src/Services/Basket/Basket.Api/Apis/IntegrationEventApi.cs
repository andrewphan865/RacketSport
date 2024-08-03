using Basket.Api.Infrastructure.IntegrationEvents.EventHandling;
using EventBus.Extensions;
using Microsoft.AspNetCore.Mvc;

namespace Basket.Api.Apis;

public static class IntegrationEventApi
{
    
    public static IEndpointRouteBuilder MapEventApiV1(this IEndpointRouteBuilder app)
    {
        var api = app.MapGroup("api").HasApiVersion(1.0);

        api.MapPost("/OrderStatusChangedToSubmitted", OrderStatusChangedToSubmitted)
        .WithTopic(EventNames.DAPR_PUBSUB, nameof(OrderStatusChangedToSubmittedIntegrationEvent));

        return app;
    }

    public static async Task OrderStatusChangedToSubmitted(
        OrderStatusChangedToSubmittedIntegrationEvent @event,
        [FromServices] OrderStatusChangedToSubmittedIntegrationEventHandler handler)
    {
        await handler.Handle(@event);
    }
}
