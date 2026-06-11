using APIAcademia.Models;

namespace APIAcademia.Repositories
{
    public interface IPlanoRepository : IRepository<Plano>
    {
        Task<IEnumerable<Plano>> ObterTodosComAlunosAsync();
        Task<Plano?> ObterPorIdComAlunosAsync(int id);
    }
}
