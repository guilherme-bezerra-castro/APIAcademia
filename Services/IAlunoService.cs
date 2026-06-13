using APIAcademia.DTOs;
using APIAcademia.DTOs.Alunos;

namespace APIAcademia.Services
{
    public interface IAlunoService
    {
        Task<IEnumerable<AlunoResponseDTO>> ObterTodosAsync();
        Task<AlunoResponseDTO?> ObterPrimeiroAsync();
        Task<AlunoResponseDTO?> ObterPorIdAsync(int id);
        Task<IEnumerable<AlunoResponseDTO>> FiltrarAsync(bool? ativo, int? planoId);
        Task<ResultadoPaginado<AlunoResponseDTO>> ObterPaginadoAsync(int pagina, int itensPorPagina);
        Task<AlunoResponseDTO> CriarAsync(AlunoRequestDTO dto);
        Task<AlunoResponseDTO?> AtualizarAsync(int id, AlunoRequestDTO dto);
        Task<AlunoResponseDTO?> AtualizarStatusAsync(int id, bool ativo);
        Task<bool> RemoverAsync(int id);
    }
}
