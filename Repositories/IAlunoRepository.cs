using APIAcademia.Models;

namespace APIAcademia.Repositories
{
    public interface IAlunoRepository : IRepository<Aluno>
    {
        Task<IEnumerable<Aluno>> ObterTodosComPlanoAsync();
        Task<Aluno?> ObterPorIdComPlanoAsync(int id);
        Task<IEnumerable<Aluno>> FiltrarAsync(bool? ativo, int? planoId);
        Task<(IEnumerable<Aluno> Itens, int Total)> ObterPaginadoAsync(int pagina, int itensPorPagina);
    }
}
