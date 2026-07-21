using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace APIAcademia.Services
{
    public class TokenService
    {
        private readonly IConfiguration _config;

        public TokenService(IConfiguration config)
        {
            _config = config;
        }

        public (string Token, DateTime Expiracao) GerarToken(string email)
        {
            var secretKey = _config["Jwt:SecretKey"]!;
            var issuer = _config["Jwt:Issuer"]!;
            var audience = _config["Jwt:Audience"]!;
            var horas = int.Parse(_config["Jwt:ExpiracaoHoras"]!);

            var chave = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));
            var creds = new SigningCredentials(chave, SecurityAlgorithms.HmacSha256);
            var expira = DateTime.UtcNow.AddHours(horas);

            var claims = new[]
            {
                new Claim(ClaimTypes.Email, email),
                new Claim(ClaimTypes.Role, "Admin")
            };

            var token = new JwtSecurityToken(
                issuer: issuer,
                audience: audience,
                claims: claims,
                expires: expira,
                signingCredentials: creds);

            return (new JwtSecurityTokenHandler().WriteToken(token), expira);
        }
    }
}