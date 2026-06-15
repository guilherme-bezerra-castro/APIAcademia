using APIAcademia.DTOs.Planos;

namespace APIAcademia.Services
{
    public interface IPlanoService
    {
        public Task<IEnumerable<PlanoResponseDTO>> ObterTodosAsync();
        public Task<PlanoResponseDTO?> ObterPorIdAsync(int id);
        public Task<PlanoResponseDTO> CriarAsync(PlanoRequestDTO dto);
        public Task<PlanoResponseDTO?> AtualizarAsync(int id, PlanoRequestDTO dto);
        public Task<bool> RemoverAsync(int id);
    }
}
