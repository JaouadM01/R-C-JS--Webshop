using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using Microsoft.Extensions.Configuration;
using System.Security.Claims;


namespace Backend.Services
{
    using Backend.Models;
    public interface ITokenService
    {
        string GenerateToken(User user);
    }


    public class TokenService : ITokenService
    {
        private readonly IConfiguration _configuration;

        public TokenService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string GenerateToken(User user)
        {
            var claims = new[]
            {
            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),   // Gebruikers-ID
            new Claim(ClaimTypes.Name, user.Name),                      // Gebruikersnaam
            new Claim(ClaimTypes.Email, user.Email),                    // Gebruikersemail
            new Claim(ClaimTypes.Role, user.Role.ToString())            // Gebruikersrol
        };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:SecretKey"]));  // Haal de geheime sleutel op
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],     // Issuer uit appsettings.json
                audience: _configuration["Jwt:Audience"], // Audience uit appsettings.json
                claims: claims,
                expires: DateTime.Now.AddHours(1),        // Vervalt na 1 uur
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);  // Genereer het JWT-token
        }
    }

}