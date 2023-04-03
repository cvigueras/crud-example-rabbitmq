using System.Security.Claims;
using Microsoft.AspNetCore.Identity;

namespace Crud.Example.Infrastructure.Auth.Interfaces
{
    public interface IJwtService
    {
        (string, DateTime?) GetToken(List<Claim> authClaims, IdentityUser user);
        string? ValidateJwtToken(string token);
    }
}