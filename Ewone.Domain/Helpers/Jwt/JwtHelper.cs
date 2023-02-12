using System.Security.Claims;
using System.Text;
using Ewone.Data.Entities;
using Microsoft.Extensions.Configuration;
using Google.Apis.Auth;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;

namespace Ewone.Domain.Helpers.Jwt;

public class JwtHelper : IJwtHelper
{
    private readonly IConfigurationSection _googleSettings;
    private readonly IConfiguration _configuration;

    public JwtHelper(IConfiguration configuration)
    {
        _configuration = configuration;

        _googleSettings = configuration.GetSection("GoogleAuthSettings");
    }

    public async Task<GoogleJsonWebSignature.Payload> VerifyGoogleToken(string idToken)
    {
        string? clientId = _googleSettings.GetSection("clientId").Value;

        if (clientId == null)
        {
            throw new Exception("clientId is null");
        }

        var settings = new GoogleJsonWebSignature.ValidationSettings()
        {
            Audience = new List<string>
            {
                clientId
            }
        };

        var payload = await GoogleJsonWebSignature.ValidateAsync(idToken, settings);

        return payload;
    }

    public string CreateAccessToken(User user)
    {
        string? key = _configuration["Token:SecurityKey"];

        if (key == null)
        {
            throw new Exception("SecurityKey is null");
        }

        SymmetricSecurityKey securityKey = new(Encoding.UTF8.GetBytes(key));

        SigningCredentials signingCredentials = new(securityKey, SecurityAlgorithms.HmacSha256);

        int expireInMin = 5;

        if (int.TryParse(_configuration["Token:ExpireInMin"], out var expire))
        {
            expireInMin = expire;
        }

        JwtSecurityToken securityToken = new(
            audience: _configuration["Token:Audience"],
            issuer: _configuration["Token:Issuer"],
            claims: new List<Claim>
            {
                new(ClaimTypes.Email, user.Email),
                new(ClaimTypes.Name, user.Id.ToString()),
            },
            expires: DateTime.UtcNow.AddMinutes(expireInMin),
            notBefore: DateTime.UtcNow,
            signingCredentials: signingCredentials
        );

        JwtSecurityTokenHandler tokenHandler = new();
        return tokenHandler.WriteToken(securityToken);
    }
}