namespace APIAcademia.Models.Auth
{
    public class LoginRequest(string Email, string Senha)
    {
        public string Email { get; internal set; }
        public string Senha { get; internal set; }
    }
}
