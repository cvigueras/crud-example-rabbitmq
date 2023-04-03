using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Crud.Example.Infrastructure.Auth.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;

namespace Crud.Example.Infrastructure.Auth.Services
{
    public class JwtService : IJwtService
    {
        #region Private Members
        private readonly IConfiguration _configuration;
        private readonly ILogger<JwtService> _logger;
        #endregion

        #region Constructor
        public JwtService(IConfiguration configuration,
                            ILogger<JwtService> logger)
        {
            _configuration = configuration;
            _logger = logger;
        }
        #endregion

        #region Public Methods
        public (string, DateTime?) GetToken(List<Claim> authClaims, IdentityUser user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_configuration["JWT:Secret"]);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[] { new Claim("id", user.Id.ToString()) }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return (tokenHandler.WriteToken(token), tokenDescriptor.Expires);
        }
        public string? ValidateJwtToken(string token)
        {
            if (token == null)
            {
                return null;
            }

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_configuration["JWT:Secret"]);
            try
            {
                tokenHandler.ValidateToken(token, new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ClockSkew = TimeSpan.Zero
                }, out SecurityToken validatedToken);

                var jwtToken = (JwtSecurityToken)validatedToken;
                var userId = jwtToken.Claims.First(x => x.Type == "id").Value;

                return userId;

            }
            catch(SecurityTokenInvalidSignatureException stse)
            {
                _logger.LogError(stse, "An error has courred getting the Token");
                return null;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error has courred getting the Token");
                return null;
            }
        }
        #endregion
    }
}