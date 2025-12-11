using Duende.IdentityServer;
using Duende.IdentityServer.Models;

namespace MicroShop.IdentiryServer.Configuration;

public class IdentityConfiguration
{
    public const string AdminRole = "Admin";
    public const string ClientRole = "Client";

    public static IEnumerable<IdentityResource> IdentityResources =>
        new List<IdentityResource>
        {
            new IdentityResources.OpenId(),
            new IdentityResources.Email(),
            new IdentityResources.Profile()
        };

    public static IEnumerable<ApiScope> ApiScopes =>
        new List<ApiScope>
        {
            new ApiScope("MicroShop", "MicroShop Server"),
            new ApiScope(name: "read", "Read data."),
            new ApiScope(name: "write", "Write data."),
            new ApiScope(name: "delete", "Delete data.")
        };

    public static IEnumerable<Client> Clients =>
        new List<Client>
        {
            // Usuario geral
            new Client
            {
                ClientId = "client",
                ClientSecrets = { new Secret("marcomelo#2@01MarcoM3L0".Sha256()) },
                AllowedGrantTypes = GrantTypes.ClientCredentials,
                AllowedScopes = { "read", "write", "profile" }
            },
            new Client
            {
                ClientId = "MicroShop",
                ClientSecrets = { new Secret("marcomelo#2@01MarcoM3L0".Sha256()) },
                AllowedGrantTypes = GrantTypes.Code,
                RedirectUris = { "https://localhost:7095/signin-oidc" },// login
                PostLogoutRedirectUris = { "https://localhost:7095/signout-callback-oidc" }, // logout
                AllowedScopes = new List<string>
                {
                    IdentityServerConstants.StandardScopes.OpenId,
                    IdentityServerConstants.StandardScopes.Profile,
                    IdentityServerConstants.StandardScopes.Email,
                    "MicroShop"
                }
            }
        };
}