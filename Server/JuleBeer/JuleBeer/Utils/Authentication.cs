using JuleBeer.DB.Context;
using JuleBeer.DB.Entities;
using JuleBeer.Extensions;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace JuleBeer.Utils;

public static class Authentication
{
    public static JwtSecurityToken GenerateJwtToken(Guid id, string secret, string issuer, string audience, int tokenLifetimeInMin)
    {
        if (id == Guid.Empty)
        {
            throw new InvalidOperationException("ID is not specified");
        }

        var authClaims = new[] {
            new Claim(TokenSettings.ClaimType_Id, id.ToString()),
        };

        var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secret));

        var token = new JwtSecurityToken(
            issuer: issuer,
            audience: audience,
            expires: DateTime.Now.AddMinutes(tokenLifetimeInMin),
            claims: authClaims,
            signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
            );

        return token;
    }

    public static byte[] GetHash(string password, string salt)
    {
        using (HashAlgorithm algorithm = SHA256.Create())
        {
            return algorithm.ComputeHash(Encoding.UTF8.GetBytes(password + salt));
        }
    }

    public static string GetSecureSalt()
    {
        return new Guid(RandomNumberGenerator.GetBytes(16)).ToString();
    }

    public async static Task<User> GetCurrentUser(HttpContext httpContext, JuleBeerContext ctx, CancellationToken cancellationToken = default)
    {
        if (httpContext.User.Identity.IsAuthenticated)
        {
            var idString = httpContext.User.Claims.First(x => x.Type == TokenSettings.ClaimType_Id).Value;
            if (Guid.TryParse(idString, out Guid id))
            {
                var user = await ctx.Users.FindAsync(id);
                if (user != null)
                {
                    return user;
                }
            }
            throw new UnauthorizedAccessException("Token not valid");
        }
        throw new UnauthorizedAccessException("User is not Authenticated");
    }


}
