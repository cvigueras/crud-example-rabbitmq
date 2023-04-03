using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Crud.Example.Infrastructure.Auth.Interfaces;
using Crud.Example.Main.Auth.Interfaces;
using Crud.Example.Main.Auth.Models;
using Microsoft.AspNetCore.Identity;

namespace Crud.Example.Main.Auth.Services
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IJwtService _iJWTService;
        public AuthService(
            UserManager<IdentityUser> userManager,
            IJwtService iJWTService)
        {
            _userManager = userManager;
            _iJWTService = iJWTService;
        }

        /// <summary>
        /// Do login of an user with Identity Claims.
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<TokenModel?> LoginUser(LoginModel model)
        {
            var user = await _userManager.FindByNameAsync(model.Username);
            if (user != null && await _userManager.CheckPasswordAsync(user, model.Password))
            {
                var userRoles = await _userManager.GetRolesAsync(user);

                var authClaims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, user.UserName, user.Id),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                };
                
                foreach (var userRole in userRoles)
                {
                    authClaims.Add(new Claim(ClaimTypes.Role, userRole));
                }
                
                (string token, DateTime? expires) = _iJWTService.GetToken(authClaims, user);
                if (token != null)
                {
                    var tokenModel = new TokenModel()
                    {
                        Token = token,
                        Expiration = expires == null ? DateTime.Now.AddDays(7) : expires.Value,
                    };

                    authClaims.Add(new Claim(ClaimTypes.Authentication, tokenModel.Token));
                    return tokenModel;
                }
            }
            return null;
        }

        public async Task<IdentityResult> Registeruser(RegisterModel model)
        {
            IdentityUser user = new()
            {
                Email = model.Email,
                SecurityStamp = Guid.NewGuid().ToString(),
                UserName = model.Username,
                Id = Guid.NewGuid().ToString(),
            };
            return await _userManager.CreateAsync(user, model.Password);
        }

        public async Task<bool> UserExist(string username)
        {
            var userExists = await _userManager.FindByNameAsync(username);
            if (userExists != null)
                return true;
            return false;
        }

        public bool ValidateToken(string token)
        {
            var idAuth = _iJWTService.ValidateJwtToken(token);
            return idAuth != null;
        }
    }
}