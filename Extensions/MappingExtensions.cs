using APIAcademia.DTOs.Alunos;
using APIAcademia.DTOs.Planos;
using APIAcademia.Models;

namespace APIAcademia.Extensions
{
    public static class MappingExtensions
    {
        public static AlunoResponseDTO ToResponseDTO(this Aluno aluno)
        {
            return new AlunoResponseDTO(
                AlunoId: aluno.AlunoId,
                Nome: aluno.Nome ?? "",
                Email: aluno.Email ?? "",
                ImagemURL: aluno.ImagemURL ?? "",
                Ativo: aluno.Ativo,
                DataNascimento: aluno.DataNascimento,
                PlanoId: aluno.PlanoId,
                PlanoNome: aluno.Planos?.PlanoNome ?? "Sem plano"
            );
        }

        public static Aluno ToModel(this AlunoRequestDTO dto)
        {
            return new Aluno
            {
                Nome = dto.Nome,
                ImagemURL = dto.ImagemURL,
                Email = dto.Email,
                DataNascimento = dto.DataNascimento,
                Ativo = dto.Ativo,
                PlanoId = dto.PlanoId
            };
        }

        public static PlanoResponseDTO ToResponseDTO(this Plano plano)
        {
            return new PlanoResponseDTO(
                PlanoId: plano.PlanoId,
                PlanoNome: plano.PlanoNome ?? "",
                ImagemURL: plano.ImagemURL ?? "",
                Descricao: plano.Descricao ?? "",
                Mensalidade: plano.Mensalidade,
                TotalAlunos: plano.Alunos?.Count ?? 0
            );
        }

        public static Plano ToModel(this PlanoRequestDTO dto)
        {
            return new Plano
            {
                PlanoNome = dto.PlanoNome,
                ImagemURL = dto.ImagemURL,
                Descricao = dto.Descricao,
                Mensalidade = dto.Mensalidade
            };
        }
    }
}
