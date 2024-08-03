using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace Basket.Api.Apis;

public static class BasketApi
{
    public static IEndpointRouteBuilder MapBasketApiV1(this IEndpointRouteBuilder app)
    {
        var api = app.MapGroup("api").HasApiVersion(1.0);

        // CRUD
        api.MapGet("/basket", GetBasket);         
        api.MapPut("/basket", UpdateBasket);
        api.MapPost("/basket", Checkout);
        api.MapDelete("/basket", DeleteBasket);

        return app;
    }

    public static async Task<Results<Ok<CustomerBasket>, BadRequest<string>>> GetBasket(  
        [AsParameters] BasketServices services)
    {
        var userId = services.IdentityService.GetUserIdentity();

        var basket = await services.Repository.GetBasketAsync(userId);

        return TypedResults.Ok(basket ?? new CustomerBasket(userId));
    }

    public static async Task<Results<Ok<CustomerBasket>, BadRequest<string>>> UpdateBasket(
        [AsParameters] BasketServices services,
        CustomerBasket customerBasket)
    {
        var userId = services.IdentityService.GetUserIdentity();
        customerBasket.BuyerId = userId;

        return TypedResults.Ok(await services.Repository.UpdateBasketAsync(customerBasket));
    }

    public static async Task<Results<Ok<bool>, BadRequest<string>>> Checkout(
        [AsParameters] BasketServices services,
        BasketCheckout basketCheckout,
        [FromHeader(Name = "X-Request-Id")] string requestId)
    {
        var userId = services.IdentityService.GetUserIdentity();
        var basket = await services.Repository.GetBasketAsync(userId);
        if (basket == null)
        {
            services.Logger.LogWarning($"Basket for user {userId} not found");

            return TypedResults.BadRequest($"Basket for user {userId} not found");
        }
        var eventRequestId = Guid.TryParse(requestId, out Guid parsedRequestId)
                    ? parsedRequestId : Guid.NewGuid();

           var eventMessage = new UserCheckoutAcceptedIntegrationEvent(
            userId,
            basketCheckout.Email,
            basketCheckout.FirstName,
            basketCheckout.LastName,
            basketCheckout.Address,
            basketCheckout.State,
            basketCheckout.Country,
            basketCheckout.Postcode,
            basketCheckout.PaymentMethod,
            basketCheckout.CardNumber,
            basketCheckout.CardHolderName,
            basketCheckout.Expiration,
            basketCheckout.CVV,
            eventRequestId,
            basket);

        await services.EventBus.PublishAsync(eventMessage);

        return TypedResults.Ok(true);
    }

    public static async Task<Results<NoContent, NotFound>> DeleteBasket(
       [AsParameters] BasketServices services)
    {
        var userId = services.IdentityService.GetUserIdentity();

        var basket = await services.Repository.GetBasketAsync(userId);
        if (basket == null)
        {
            services.Logger.LogWarning($"Basket for user {userId} not found");
            return TypedResults.NotFound();
        }

        await services.Repository.DeleteBasketAsync(userId);
        services.Logger.LogInformation($"Deleting Basket for user {userId}.....");

        return TypedResults.NoContent();
    }

}
