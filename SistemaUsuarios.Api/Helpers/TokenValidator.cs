using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace SistemaUsuarios.Api.Helpers
{
    public class TokenValidator
    {
        private readonly IConfiguration _config;

        public TokenValidator(IConfiguration config)
        {
            _config = config;
        }

        public ClaimsPrincipal Validate(string token)
        {
            var tokenHandler = new JwtSecurityTokenHandler();

            var key = Encoding.UTF8.GetBytes(_config["Jwt:Key"]);

            var parameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = false,
                ValidateIssuerSigningKey = true,

                ValidIssuer = _config["Jwt:Issuer"],
                ValidAudience = _config["Jwt:Audience"],

                IssuerSigningKey = new SymmetricSecurityKey(key)
            };

            var principal = tokenHandler.ValidateToken(token, parameters, out SecurityToken validatedToken);

            return principal;
        }
    }
}