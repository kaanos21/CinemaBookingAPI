
using Microsoft.IdentityModel.Tokens;
using MovieReservationSystem.Entities;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace MovieReservationSystem.Services.Token
{
    public class TokenService : ITokenService
    {
        private readonly IConfiguration _configuration;

        public TokenService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public Dtos.JwtDtos.Token CreateAccessTokenForAdmin(ApplicationUser user, string role, int minute)
        {
            Dtos.JwtDtos.Token token = new();

            var claims = new List<Claim>
        {
            new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
            new Claim(JwtRegisteredClaimNames.Email, user.Email),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new Claim(ClaimTypes.Role, role)
        };

            SymmetricSecurityKey securityKey = new(Encoding.UTF8.GetBytes(_configuration["Token:AdminSecurityKey"]));
            SigningCredentials signingCredentials = new(securityKey, SecurityAlgorithms.HmacSha256);

            token.Expiration = DateTime.UtcNow.AddMinutes(minute);
            JwtSecurityToken securityToken = new(
                audience: _configuration["Token:AdminAudience"],
                issuer: _configuration["Token:AdminIssuer"],
                claims: claims,
                expires: token.Expiration,
                notBefore: DateTime.UtcNow,
                signingCredentials: signingCredentials
            );

            JwtSecurityTokenHandler tokenHandler = new();
            token.AccesToken = tokenHandler.WriteToken(securityToken);
            return token;
        }

        public Dtos.JwtDtos.Token CreateAccessTokenForUser(ApplicationUser user, string role, int minute)
        {
            Dtos.JwtDtos.Token token = new();

            var claims = new List<Claim>
        {
            new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
            new Claim(JwtRegisteredClaimNames.Email, user.Email),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new Claim(ClaimTypes.Role, role)
        };

            SymmetricSecurityKey securityKey = new(Encoding.UTF8.GetBytes(_configuration["Token:SecurityKey"]));
            SigningCredentials signingCredentials = new(securityKey, SecurityAlgorithms.HmacSha256);

            token.Expiration = DateTime.UtcNow.AddMinutes(minute);
            JwtSecurityToken securityToken = new(
                audience: _configuration["Token:Audience"],
                issuer: _configuration["Token:Issuer"],
                claims: claims,
                expires: token.Expiration,
                notBefore: DateTime.UtcNow,
                signingCredentials: signingCredentials
            );

            JwtSecurityTokenHandler tokenHandler = new();
            token.AccesToken = tokenHandler.WriteToken(securityToken);
            return token;
        }
    }
}
