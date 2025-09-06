namespace POS.Presentation.Models
{
    public class LoginModel
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public bool RememberMe { get; set; }
        public string Key { get; set; }
        public string Audience { get; set; }
        public int TokenExpirationInMinutes { get; set; }
    }
}
