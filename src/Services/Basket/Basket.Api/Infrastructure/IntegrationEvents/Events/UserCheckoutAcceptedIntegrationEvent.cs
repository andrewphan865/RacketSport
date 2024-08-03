namespace Basket.Api.Infrastructure.IntegrationEvents.Events;

public record UserCheckoutAcceptedIntegrationEvent(
    string UserId,
    string Email,
    string FirstName,
    string LastName,
    string Address,
    string State,
    string Country,
    string Postcode,
    string PaymentMethod,
    string CardNumber,
    string CardHolderName,
    DateTime Expiration,
    string CVV,
    Guid RequestId,
    CustomerBasket Basket)
    : IntegrationEvent;
