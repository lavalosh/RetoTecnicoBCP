using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace RetoTecnicoBCP.Util
{
    public interface ITokenAuth
    {
        string GenerateToken(string usuario);
    }
    public class TokenAuth : ITokenAuth
    {
        private readonly IConfiguration _configuration;

        public TokenAuth(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string GenerateToken(string usuario)
        {
            var secretKey = _configuration.GetValue<string>("SecretKey");
            var key = Encoding.ASCII.GetBytes(secretKey);
            var expireTime = _configuration.GetValue<string>("ExpireMinutes");

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, usuario)
                }),
                Expires = DateTime.UtcNow.AddMinutes(Convert.ToInt32(expireTime)),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var createdToken = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(createdToken);
        }
    }
}
