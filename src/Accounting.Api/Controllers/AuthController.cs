using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Accounting.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly IConfiguration _configuration;

    public AuthController(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    [HttpPost("token")]
    public ActionResult<TokenResponse> CreateToken([FromBody] LoginRequest request)
    {
        // Demo purpose: replace with proper identity provider in production
        if (request.Username != "admin" || request.Password != "P@ssw0rd!")
        {
            return Unauthorized();
        }

        var jwtSection = _configuration.GetSection("Jwt");
        var secret = jwtSection.GetValue<string>("Secret") ?? "ChangeThisDevelopmentSecretKey123!";
        var issuer = jwtSection.GetValue<string>("Issuer") ?? "AccountingSystem";
        var audience = jwtSection.GetValue<string>("Audience") ?? "AccountingSystemClients";

        var claims = new[]
        {
            new Claim(JwtRegisteredClaimNames.Sub, request.Username),
            new Claim(ClaimTypes.Name, request.Username),
            new Claim(ClaimTypes.Role, "Administrator")
        };

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secret));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
        var token = new JwtSecurityToken(
            issuer,
            audience,
            claims,
            expires: DateTime.UtcNow.AddHours(8),
            signingCredentials: creds);

        return Ok(new TokenResponse
        {
            Token = new JwtSecurityTokenHandler().WriteToken(token),
            ExpiresOn = token.ValidTo
        });
    }
}

public record LoginRequest(string Username, string Password);

public record TokenResponse
{
    public string Token { get; init; } = default!;
    public DateTime ExpiresOn { get; init; }
}
