using Duende.IdentityServer.Extensions;
using Duende.IdentityServer.Models;
using Duende.IdentityServer.Services;
using IdentityModel;
using MicroShop.IdentiryServer.Data;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace MicroShop.IdentiryServer.Services;

public class ProfileAppService : IProfileService
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly RoleManager<IdentityRole> _roleManager;
    private readonly IUserClaimsPrincipalFactory<ApplicationUser> _claimsFactory;

    public ProfileAppService(UserManager<ApplicationUser> userManager, 
        RoleManager<IdentityRole> roleManager, 
        IUserClaimsPrincipalFactory<ApplicationUser> claimsFactory)
    {
        _userManager = userManager;
        _roleManager = roleManager;
        _claimsFactory = claimsFactory;
    }

    public async Task GetProfileDataAsync(ProfileDataRequestContext context)
    {
        string subjectId = context.Subject.GetSubjectId();
        ApplicationUser user = await _userManager.FindByIdAsync(subjectId);

        ClaimsPrincipal claimsPrincipal = await _claimsFactory.CreateAsync(user);

        List<Claim> claims = claimsPrincipal.Claims.ToList();
        claims.Add(new Claim(
            JwtClaimTypes.FamilyName,
            user.LastName
        ));
        claims.Add(new Claim(
            JwtClaimTypes.GivenName,
            user.FirstName
        ));

        if(_userManager.SupportsUserRole)
        {
            IList<string> roles = await _userManager.GetRolesAsync(user);
            foreach (string roleName in roles)
            {
                claims.Add(new Claim(JwtClaimTypes.Role, roleName));
                if (_roleManager.SupportsRoleClaims)
                {
                    IdentityRole role = await _roleManager.FindByNameAsync(roleName);
                    
                    if(role is not null)
                    {
                        claims.AddRange(await _roleManager.GetClaimsAsync(role));
                    }
                }
            }
        }

        context.IssuedClaims = claims;
    }

    public async Task IsActiveAsync(IsActiveContext context)
    {
        string subjectId = context.Subject.GetSubjectId();

        ApplicationUser user = await _userManager.FindByIdAsync(subjectId);
        context.IsActive = user is not null;
    }
}
