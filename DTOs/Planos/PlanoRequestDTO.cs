namespace APIAcademia.DTOs.Planos
{
    public record PlanoRequestDTO(
        string PlanoNome,
        string ImagemURL,
        string Descricao,
        decimal Mensalidade
    );
}
