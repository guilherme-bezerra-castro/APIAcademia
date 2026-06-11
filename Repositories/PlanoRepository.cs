using APIAcademia.Context;
using APIAcademia.Models;
using Microsoft.EntityFrameworkCore;

namespace APIAcademia.Repositories
{
    public class PlanoRepository : IPlanoRepository, Repository<Plano>
    {
        public PlanoRepository(AppDbContext context) : base(context)
        {
        }

        public async Task<Plano?> ObterPorIdComAlunosAsync(int id) => await _context.Planos!.Include(p => p.Alunos).AsNoTracking().FirstOrDefaultAsync(p => p.PlanoId == id);

        public async Task<IEnumerable<Plano>> ObterTodosComAlunosAsync() => await _context.Planos!.Include(p => p.Alunos).AsNoTracking().ToListAsync();
    }
}
