using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using MeuPontoMongoDb.Interface;
using MeuPontoMongoDb.Models;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace MeuPontoMongoDb.Service
{
    /// <summary>Configurações do JWT, lidas da seção "Jwt" (a chave fica em User Secrets / variável de ambiente).</summary>
    public class JwtSettings
    {
        public string Key { get; set; } = string.Empty;
        public string Issuer { get; set; } = "MeuPontoApi";
        public string Audience { get; set; } = "MeuPontoClient";
        public int ExpiraEmHoras { get; set; } = 8;
    }

    public class TokenService : ITokenService
    {
        private readonly JwtSettings _settings;

        public TokenService(IOptions<JwtSettings> settings)
        {
            _settings = settings.Value;
        }

        public (string Token, DateTime ExpiraEm) GerarToken(Cadastro usuario)
        {
            var expiraEm = DateTime.UtcNow.AddHours(_settings.ExpiraEmHoras);

            var claims = new List<Claim>
            {
                new(JwtRegisteredClaimNames.Sub, usuario.UserId.ToString()),
                new(JwtRegisteredClaimNames.Email, usuario.Email ?? string.Empty),
                new(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new(ClaimTypes.NameIdentifier, usuario.UserId.ToString()),
                new(ClaimTypes.Name, usuario.Nome ?? string.Empty),
            };

            var credenciais = new SigningCredentials(
                new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_settings.Key)),
                SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _settings.Issuer,
                audience: _settings.Audience,
                claims: claims,
                expires: expiraEm,
                signingCredentials: credenciais);

            return (new JwtSecurityTokenHandler().WriteToken(token), expiraEm);
        }
    }
}
