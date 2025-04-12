using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;

namespace GameStore.Frontend.Authorization;

public class KeycloakClaimsTransformer
{
    public void Transform(TokenValidatedContext context)
    {
        var identity = context.Principal?.Identity as ClaimsIdentity;

        identity?.TransformScopeClaim(GameStoreClaimTypes.Scope);
        identity?.MapUserIdClaim(JwtRegisteredClaimNames.Sub);
    }
}
