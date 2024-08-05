using Polly;

namespace Identity.Api;

public class SeedData
{
    public static async Task EnsureSeedData(IServiceScope scope, IConfiguration configuration, Microsoft.Extensions.Logging.ILogger logger)
    {
        var retryPolicy = CreateRetryPolicy(configuration, logger);
        var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

        await retryPolicy.ExecuteAsync(async () =>
        {
            await context.Database.MigrateAsync();
            
            var userMgr = scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();
            var andrewp = await userMgr.FindByNameAsync("andrewp");

            if (andrewp == null)
            {
                andrewp = new ApplicationUser
                {
                    UserName = "andrewp",
                    Email = "AndrewPhan@email.com",
                    EmailConfirmed = true,
                    CardHolderName = "Andrew Phan",
                    CardNumber = "4111111111111111",
                    CardType = 1,
                    Country = "Australia",
                    Expiration = "11/29",
                    Id = Guid.NewGuid().ToString(),
                    LastName = "Phan",
                    FirstName = "Andrew",
                    PhoneNumber = "0423456789",
                    PostCode = "2000",
                    State = "NSW",
                    Street = "456 George St",
                    SecurityNumber = "789"
                };

                var result = userMgr.CreateAsync(andrewp, "Test123!").Result;

                if (!result.Succeeded)
                {
                    throw new Exception(result.Errors.First().Description);
                }

                logger.LogDebug("andrewp created");
            }
            else
            {
                logger.LogDebug("andrewp already exists");
            }

            var bob = await userMgr.FindByNameAsync("bob");

            if (bob == null)
            {
                bob = new ApplicationUser
                {
                    UserName = "bob",
                    Email = "BobSmith@email.com",
                    EmailConfirmed = true,
                    CardHolderName = "Bob Smith",
                    CardNumber = "4012888888881881",
                    CardType = 1,
                    Country = "Australia",
                    Expiration = "12/28",
                    Id = Guid.NewGuid().ToString(),
                    LastName = "Smith",
                    FirstName = "Bob",
                    PhoneNumber = "0412345678",
                    PostCode = "3000",
                    State = "VIC",
                    Street = "123 Collins St",
                    SecurityNumber = "456"
                };

                var result = await userMgr.CreateAsync(bob, "Test123!");

                if (!result.Succeeded)
                {
                    throw new Exception(result.Errors.First().Description);
                }

                logger.LogDebug("bob created");
            }
            else
            {
                logger.LogDebug("bob already exists");
            }
        });
    }

    private static AsyncPolicy CreateRetryPolicy(IConfiguration configuration, Microsoft.Extensions.Logging.ILogger logger)
    {
        var retryMigrations = false;
        bool.TryParse(configuration["RetryMigrations"], out retryMigrations);

        // Only use a retry policy if configured to do so.
        // When running in an orchestrator/K8s, it will take care of restarting failed services.
        if (retryMigrations)
        {
            return Policy.Handle<Exception>().
                WaitAndRetryForeverAsync(
                    sleepDurationProvider: retry => TimeSpan.FromSeconds(5),
                    onRetry: (exception, retry, timeSpan) =>
                    {
                        logger.LogWarning(
                            exception,
                            "Exception {ExceptionType} with message {Message} detected during database migration (retry attempt {retry})",
                            exception.GetType().Name,
                            exception.Message,
                            retry);
                    }
                );
        }

        return Policy.NoOpAsync();
    }
}
