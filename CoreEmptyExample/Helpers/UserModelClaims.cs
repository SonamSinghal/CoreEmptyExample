using CoreEmptyExample.Model;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace CoreEmptyExample.Helpers
{
    public class UserModelClaims : UserClaimsPrincipalFactory<UserModel, IdentityRole>
    {

        public UserModelClaims(UserManager<UserModel> userManager, RoleManager<IdentityRole> roleManager, IOptions<IdentityOptions> options)
            :base(userManager, roleManager, options)
            {
        }

        protected override async Task<ClaimsIdentity> GenerateClaimsAsync(UserModel user)
        {
            var identity= await base.GenerateClaimsAsync(user);
            identity.AddClaim(new Claim("UserFirstName", user.FirstName ?? " "));
            identity.AddClaim(new Claim("UserLastName", user.LastName ?? " "));

            return identity;
        }
    }
}
