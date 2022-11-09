using JuleBeer.DB.Context;
using JuleBeer.Dto.Auth;
using JuleBeer.Utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Net;

namespace JuleBeer.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AuthController : ControllerBase
{
    private readonly JwtSecurityTokenHandler _jwtTokenHandler = new JwtSecurityTokenHandler();
    private readonly IDbContextFactory<JuleBeerContext> _dbContextFactory;
    private readonly IConfiguration _config;

    public AuthController(IConfiguration configuration, IDbContextFactory<JuleBeerContext> dbContextFactory)
    {
        _dbContextFactory = dbContextFactory;
        _config = configuration;
    }

    [HttpPost]
    [Route(nameof(Login))]
    [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(TokenDto))]
    public async Task<IActionResult> Login([FromBody] LoginDto dto, CancellationToken cancellationToken = default)
    {

        using var ctx = await _dbContextFactory.CreateDbContextAsync(cancellationToken);

        // finna user ef ekki til búa til
        var user = await ctx.Users
            .Where(x => x.Name == dto.Name)
            .FirstOrDefaultAsync(cancellationToken);

        if (user == null)
        {
            user = new DB.Entities.User
            {
                Name = dto.Name,
            };
            await ctx.Users.AddAsync(user);
            await ctx.SaveChangesAsync(cancellationToken);
        }

        // gefa user token
        var token = Authentication.GenerateJwtToken(user.Id,
                _config["JWT:Secret"], _config["JWT:ValidIssuer"],
                _config["JWT:ValidAudience"], int.Parse(_config["JWT:TokenLifetimeInMin"]));

        string validToken = _jwtTokenHandler.WriteToken(token);
        return Ok(validToken);
    }

    [HttpGet]
    [Authorize]
    [Route(nameof(Me))]
    [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(TokenDto))]
    public async Task<IActionResult> Me(CancellationToken cancellationToken = default)
    {
        using var ctx = await _dbContextFactory.CreateDbContextAsync(cancellationToken);
        var currentUser = await Authentication.GetCurrentUser(HttpContext, ctx, cancellationToken);
        return Ok(currentUser);
    }

}
