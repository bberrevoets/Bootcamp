using System.Security.Claims;
using Microsoft.AspNetCore.Authentication.JwtBearer;

namespace GameStore.Api.Shared.Authorization;

public class EntraClaimsTransformer
{
    private readonly ILogger<EntraClaimsTransformer> _logger;

    public EntraClaimsTransformer(ILogger<EntraClaimsTransformer> logger)
    {
        _logger = logger;
    }

    private const string ScopeClaimType = "scp";
    private const string OidClaimType = "oid";

    public void Transform(TokenValidatedContext context)
    {
        var identity = context.Principal?.Identity as ClaimsIdentity;

        identity?.TransformScopeClaim(ScopeClaimType);

        identity?.MapUserIdClaim(OidClaimType);

        context.Principal?.LogAllClaims(_logger);
    }
}
