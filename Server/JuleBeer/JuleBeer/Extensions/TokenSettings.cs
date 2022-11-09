using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using System.Text;

namespace JuleBeer.Extensions;

public static class TokenSettings
{
    public const string ClaimType_Id = ClaimTypes.PrimarySid;
    public const string ClaimType_Email = ClaimTypes.Email;
    public const string ClaimType_Role = ClaimTypes.Role;
    public const string ClaimType_TenantId = ClaimTypes.GroupSid;

    public static TokenValidationParameters GetTokenValidationParameters(string secret, string validAudience, string validIssuer)
    {
        return new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secret)),
            ValidateAudience = true,
            ValidAudience = validAudience,
            ValidateIssuer = true,
            ValidIssuer = validIssuer,
            ValidateLifetime = true,
            RequireExpirationTime = true,
        };
    }
}
