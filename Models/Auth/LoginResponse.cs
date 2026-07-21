namespace APIAcademia.Models.Auth
{
    public record LoginResponse
    {
        public string Token { get; init; } = string.Empty;
        public DateTime Expiracao { get; init; }
    }
}