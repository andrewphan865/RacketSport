
namespace Identity.Api;

public static class Config
{
    public static IEnumerable<IdentityResource> IdentityResources =>
        new IdentityResource[]
        {
            new IdentityResources.OpenId(),
            new IdentityResources.Profile(),
        };

    public static IEnumerable<ApiScope> ApiScopes =>
        new ApiScope[]
        {
            new ApiScope("racketSport", "Full access to the Racket Sport App"),
            new ApiScope("basket", "Access to Basket API"),
            new ApiScope("ordering", "Access to Ordering API"),
            new ApiScope("shoppingaggr", "Access to Shopping Aggregator API")
        };
    public static IEnumerable<ApiResource> ApiResources =>
        new ApiResource[]
        {
                new ApiResource("basket-api", "Basket API")
                {
                    Scopes = { "basket" }

                },
                new ApiResource("ordering-api", "Ordering API")
                {
                    Scopes = { "ordering" }
                },
                new ApiResource("shoppingaggr-api", "Shopping Aggregator API")
                {
                    Scopes = { "shoppingaggr" }
                }
        };

    public static IEnumerable<Client> Clients(ConfigurationManager config) =>
         new Client[]
            {
                new Client
                    {
                        ClientId = "postman",
                        ClientName = "Postman",
                        AllowedScopes = {"openid", "profile", "racketSport", "basket", "ordering", "shoppingaggr"},
                        RedirectUris = {"https://www.getpostman.com/oauth2/callback"},
                        ClientSecrets = new[] {new Secret("NotASecret".Sha256())},
                        AllowedGrantTypes = {GrantType.ResourceOwnerPassword}
                    },
                new Client
                    {
                        ClientId = "nextApp",
                        ClientName = "nextApp",
                        ClientSecrets = {new Secret("secret".Sha256())},
                        AllowedGrantTypes = GrantTypes.CodeAndClientCredentials,
                        RequirePkce = false,
                        RedirectUris = {config["ClientApp"] + "/api/auth/callback/id-server"},
                        AllowOfflineAccess = true,
                        AllowedScopes = {"openid", "profile", "racketSport", "basket", "ordering", "shoppingaggr"},
                        AccessTokenLifetime = 3600*24*30,
                        AlwaysIncludeUserClaimsInIdToken = true
                    }
        };
}
