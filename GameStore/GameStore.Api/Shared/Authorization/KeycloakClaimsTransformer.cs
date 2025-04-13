using System.Security.Claims;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.JsonWebTokens;

namespace GameStore.Api.Shared.Authorization;

public class KeycloakClaimsTransformer(ILogger<KeycloakClaimsTransformer> logger)
{
    public void Transform(TokenValidatedContext context)
    {
        var identity = context.Principal?.Identity as ClaimsIdentity;

        identity?.TransformScopeClaim(GameStoreClaimTypes.Scope);

        identity?.MapUserIdClaim(JwtRegisteredClaimNames.Sub);

        context.Principal?.LogAllClaims(logger);
    }
}