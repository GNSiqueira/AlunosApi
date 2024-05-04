namespace AlunosAPI.ViewModels
{
    public class UserToken
    {
        public required string Token {get; set; }
        public DateTime Expiration {get; set;}
    }
}