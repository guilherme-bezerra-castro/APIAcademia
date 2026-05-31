using APIAcademia.DTOs.Planos;

namespace APIAcademia.Services
{
    public interface IPlanoService
    {
        IEnumerable<PlanoResponseDTO> ObterTodos();
        PlanoResponseDTO? ObterPorId(int id);
        PlanoResponseDTO Criar(PlanoRequestDTO dto);
        PlanoResponseDTO? Atualizar(int id, PlanoRequestDTO dto);
        bool Remover(int id);
    }
}
