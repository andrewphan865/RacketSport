using Dapr.Client;

namespace Basket.Api.Infrastructure.Repositories;

public class BasketRepository : IBasketRepository
{
    private const string StateStoreName = "racketsport-statestore";

    private readonly DaprClient _daprClient;
    private readonly ILogger _logger;

    public BasketRepository(DaprClient daprClient, ILogger<BasketRepository> logger)
    {
        _daprClient = daprClient;
        _logger = logger;
    }

    public Task DeleteBasketAsync(string customerId) { 
        _daprClient.DeleteStateAsync(StateStoreName, customerId);
        _logger.LogInformation($"Basket {customerId} deleted successfully.");     
        return Task.CompletedTask;
    }
    public Task<CustomerBasket> GetBasketAsync(string customerId) =>
        _daprClient.GetStateAsync<CustomerBasket>(StateStoreName, customerId);

    public async Task<CustomerBasket> UpdateBasketAsync(CustomerBasket basket)
    {
        var state = await _daprClient.GetStateEntryAsync<CustomerBasket>(StateStoreName, basket.BuyerId);
        state.Value = basket;

        await state.SaveAsync();

        _logger.LogInformation($"Basket {basket.BuyerId} updated successfully.");

        return await GetBasketAsync(basket.BuyerId);
    }
}
