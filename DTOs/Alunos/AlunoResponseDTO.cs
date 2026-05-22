namespace APIAcademia.DTOs.Alunos
{
    public record AlunoResponseDTO(
        int AlunoId,
        string Nome,
        string Email,
        string ImagemURL,
        bool Ativo,
        DateTime DataNascimento,
        int PlanoId,
        string PlanoNome
    );
}
