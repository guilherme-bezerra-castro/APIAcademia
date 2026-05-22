namespace APIAcademia.DTOs.Planos
{
    public record PlanoResponseDTO(
        int PlanoId,
        string PlanoNome,
        string ImagemURL,
        string Descricao,
        decimal Mensalidade,
        int TotalAlunos
    );
}
