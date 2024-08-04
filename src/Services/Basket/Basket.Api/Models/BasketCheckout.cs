namespace Basket.Api.Models;

public record BasketCheckout(
    string UserId,

    // Shipping and BillingAddress
    string FirstName,
    string LastName,
    string Email,
    string Address,
    string Country,
    string State,
    string PostCode,

    // Payment   
    string CardNumber,
    string CardHolderName,
    DateTime Expiration,
    string SecurityNumber
);
