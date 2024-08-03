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
    string Postcode,

    // Payment
    string PaymentMethod,
    string CardNumber,
    string CardHolderName,
    DateTime Expiration,
    string CVV
);
