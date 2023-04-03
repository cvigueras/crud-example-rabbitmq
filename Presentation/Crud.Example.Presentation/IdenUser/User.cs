using Microsoft.AspNetCore.Identity;

namespace Crud.Example.Api.IdenUser
{
    public class User : IdentityUser
    {
        public string FirstName { get; set; } = default!;
        public string Password { get; set; } = default!;
    }
}
