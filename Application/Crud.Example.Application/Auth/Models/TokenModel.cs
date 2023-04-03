namespace Crud.Example.Main.Auth.Models
{
    public class TokenModel
    {
        public string? Token { get; set; }
        public DateTime Expiration { get; set; }
    }
}
