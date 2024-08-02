namespace Healthchecks;

public class DaprHealthCheck : IHealthCheck
{
    private readonly DaprClient _daprClient;

    public DaprHealthCheck(DaprClient daprClient)
    {
        _daprClient = daprClient ?? throw new System.ArgumentNullException(nameof(daprClient));
    }

    public async Task<HealthCheckResult> CheckHealthAsync(
        HealthCheckContext context,
        CancellationToken cancellationToken = default)
    {
        var healthy = await _daprClient.CheckHealthAsync(cancellationToken);
        
        return healthy ? HealthCheckResult.Healthy("Dapr sidecar is healthy.") :                
                new HealthCheckResult( context.Registration.FailureStatus, "Dapr sidecar is unhealthy.");
    }
}