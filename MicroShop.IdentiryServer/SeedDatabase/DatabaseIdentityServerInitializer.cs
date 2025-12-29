using IdentityModel;
using MicroShop.IdentiryServer.Configuration;
using MicroShop.IdentiryServer.Data;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace MicroShop.IdentiryServer.SeedDatabase;

public class DatabaseIdentityServerInitializer : IDatabaseSeedInitializer
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly RoleManager<IdentityRole> _roleManager;

    public DatabaseIdentityServerInitializer(UserManager<ApplicationUser> userManager,
                                                RoleManager<IdentityRole> roleManager)
    {
        _userManager = userManager;
        _roleManager = roleManager;
    }

    public void InitializeSeedRoles()
    {
        if (!_roleManager.RoleExistsAsync(IdentityConfiguration.AdminRole).Result)
        {
            IdentityRole adminRole = new IdentityRole();
            adminRole.Name = IdentityConfiguration.AdminRole;
            adminRole.NormalizedName = IdentityConfiguration.AdminRole.ToUpper();
            _roleManager.CreateAsync(adminRole).Wait();
        }
        if (!_roleManager.RoleExistsAsync(IdentityConfiguration.ClientRole).Result)
        {
            IdentityRole clientRole = new IdentityRole();
            clientRole.Name = IdentityConfiguration.ClientRole;
            clientRole.NormalizedName = IdentityConfiguration.ClientRole.ToUpper();
            _roleManager.CreateAsync(clientRole).Wait();
        }
    }

    public void InitializeSeedUsers()
    {
        // Seed Admin User
        if (_userManager.FindByEmailAsync("admin@microshop.com").Result == null)
        {
            // Create Admin User
            ApplicationUser admin = new ApplicationUser()
            {
                UserName = "admin",
                NormalizedUserName = "ADMIN",
                Email = "admin@microshop.com",
                NormalizedEmail = "ADMIN@MICROSHOP.COM",
                EmailConfirmed = true,
                LockoutEnabled = false,
                PhoneNumber = "+55 (79) 98765-4321",
                FirstName = "System",
                LastName = "Administrator",
                SecurityStamp = Guid.NewGuid().ToString()
            };

            // Set user password
            IdentityResult resultAdmin = _userManager.CreateAsync(admin, "Admin@123").Result;
            if (resultAdmin.Succeeded)
            {
                // Assign Admin role to user
                _userManager.AddToRoleAsync(admin, IdentityConfiguration.AdminRole).Wait();

                // Add Claims to Admin user
                var adminClaims = _userManager.AddClaimsAsync(admin, new[]
                {
                    new Claim(JwtClaimTypes.Name, $"{admin.FirstName} {admin.LastName}"),
                    new Claim(JwtClaimTypes.GivenName, admin.FirstName),
                    new Claim(JwtClaimTypes.FamilyName, admin.LastName),
                    new Claim(JwtClaimTypes.Role, IdentityConfiguration.AdminRole)
                }).Result;
            }
        }

        // Seed Client User
        if (_userManager.FindByEmailAsync("client@microshop.com").Result == null)
        {
            // Create Client User
            ApplicationUser client = new ApplicationUser()
            {
                UserName = "client",
                NormalizedUserName = "CLIENT",
                Email = "client@microshop.com",
                NormalizedEmail = "CLIENT@MICROSHOP.COM",
                EmailConfirmed = true,
                LockoutEnabled = false,
                PhoneNumber = "+55 (79) 91234-5678",
                FirstName = "Client",
                LastName = "User",
                SecurityStamp = Guid.NewGuid().ToString()
            };

            // Set user password
            IdentityResult resultClient = _userManager.CreateAsync(client, "Client@123").Result;
            if (resultClient.Succeeded)
            {
                // Assign Client role to user
                _userManager.AddToRoleAsync(client, IdentityConfiguration.ClientRole).Wait();

                // Add Claims to Client user
                var clientClaims = _userManager.AddClaimsAsync(client, new[]
                {
                    new Claim(JwtClaimTypes.Name, $"{client.FirstName} {client.LastName}"),
                    new Claim(JwtClaimTypes.GivenName, client.FirstName),
                    new Claim(JwtClaimTypes.FamilyName, client.LastName),
                    new Claim(JwtClaimTypes.Role, IdentityConfiguration.ClientRole)
                }).Result;
            }
        }
    }
}
