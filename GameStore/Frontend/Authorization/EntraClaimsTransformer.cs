using System.Security.Claims;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;

namespace GameStore.Frontend.Authorization;

public class EntraClaimsTransformer
{
    private const string ScopeClaimType = "scp";
    private const string OidClaimType = "oid";

    public void Transform(TokenValidatedContext context)
    {
        var identity = context.Principal?.Identity as ClaimsIdentity;

        identity?.TransformScopeClaim(ScopeClaimType);
        identity?.MapUserIdClaim(OidClaimType);
    }
}