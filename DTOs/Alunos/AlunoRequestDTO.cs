namespace APIAcademia.DTOs.Alunos
{
    public record AlunoRequestDTO(
        string Nome,
        string ImagemURL,
        string Email,
        DateTime DataNascimento,
        bool Ativo,
        int PlanoId
    );
}
