using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Api.Domain.Entities;
using Microsoft.IdentityModel.Tokens;

namespace Api.Utils.Jwt;

public class TokenService 
{
    public static string Generate (AdministratorsEntity administrator)
    {
        var handler = new JwtSecurityTokenHandler();
        var key = Encoding.ASCII.GetBytes(Configuration.KEY);

        var credentrials = new SigningCredentials(
            new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256
        );

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = GenerateClaims(administrator),     
            SigningCredentials = credentrials,
            Expires = DateTime.UtcNow.AddHours(2)
        };

        var token = handler.CreateToken(tokenDescriptor);

        return handler.WriteToken(token);
    }

    public static ClaimsIdentity GenerateClaims (AdministratorsEntity administrator)
    {
        var ci = new ClaimsIdentity();
        ci.AddClaim(new Claim (ClaimTypes.Name, administrator.User));

        return ci; 
    }
}