namespace Basket.Api.Models;

public record BasketCheckout(
    string UserName, 

    // Shipping and BillingAddress
    string FirstName,
    string LastName,
    string EmailAddress,
    string AddressLine,
    string Country,
    string State,
    string Postcode,

    // Payment
    string PaymentMethod,
    string CardNumber,
    string CardHolderName,
    string Expiration,
    string CVV
);
