namespace APIAcademia.Models.Auth
{
    public record LoginRequest
    {
        public string Email { get; init; } = string.Empty;
        public string Senha { get; init; } = string.Empty;
    }
}