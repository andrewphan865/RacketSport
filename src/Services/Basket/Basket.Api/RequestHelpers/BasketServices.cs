using Basket.Api.Services;

namespace Basket.Api.RequestHelpers;


public class BasketServices(IBasketRepository repository, IIdentityService identityService, ILogger<BasketServices> logger)
{
    public IBasketRepository Repository = repository;
    public IIdentityService IdentityService { get; } = identityService;
    public ILogger<BasketServices> Logger { get; } = logger;
};
