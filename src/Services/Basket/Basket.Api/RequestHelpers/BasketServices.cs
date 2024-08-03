using Basket.Api.Services;

namespace Basket.Api.RequestHelpers;


public class BasketServices(IBasketRepository repository, IIdentityService identityService, IEventBus eventBus, ILogger<BasketServices> logger)
{
    public IBasketRepository Repository { get; } = repository;
    public IIdentityService IdentityService { get; } = identityService;
    public IEventBus EventBus { get; } = eventBus;
    public ILogger<BasketServices> Logger { get; } = logger;
};
