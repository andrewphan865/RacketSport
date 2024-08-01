namespace Basket.Api.Models;

public class CustomerBasket
{
    public string BuyerId { get; set; } = string.Empty;

    public List<BasketItem> Items { get; set; } = new List<BasketItem>();
  
    public CustomerBasket()
    {

    }

    public CustomerBasket(string customerId)
    {
        BuyerId = customerId;
    }
}
