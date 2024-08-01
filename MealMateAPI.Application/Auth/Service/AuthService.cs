using MenuMasterAPI.Application.Auth;
using MenuMasterAPI.Domain.Entities;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace MenuMasterAPI.Application;

public class AuthService : IAuthService
{
    private readonly JwtToken _jwtToken;

    public AuthService(IOptionsMonitor<JwtToken> jwtToken)
    {
        _jwtToken = jwtToken.CurrentValue;
    }

    private Claim[] GetClaims(User user)
    {
        var claims = new[]
        {
            new Claim(ClaimTypes.Email, user.Email),
            new Claim("Id", user.Id.ToString()),
            new Claim(ClaimTypes.Role, user.Role),
            new Claim("FullName", user.FullName)
        };

        return claims;
    }

    public string CreateToken(User user)
    {
        Claim[] claims = GetClaims(user);
        var secret = Encoding.ASCII.GetBytes(_jwtToken.Secret);

        var jwtToken = new JwtSecurityToken(
            _jwtToken.Issuer,
            _jwtToken.Audience,
            claims,
            expires: DateTime.UtcNow.AddMinutes(_jwtToken.AccessTokenExpiration),
            signingCredentials: new SigningCredentials(new SymmetricSecurityKey(secret), SecurityAlgorithms.HmacSha256Signature)
            );

        var accessToken = new JwtSecurityTokenHandler().WriteToken(jwtToken);
        return accessToken;
    }
}
