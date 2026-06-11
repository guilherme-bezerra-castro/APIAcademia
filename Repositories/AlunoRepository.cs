using APIAcademia.Context;
using APIAcademia.Models;
using Microsoft.EntityFrameworkCore;

namespace APIAcademia.Repositories
{
    public class AlunoRepository : Repository<Aluno>, IAlunoRepository
    {
        public AlunoRepository(AppDbContext context) : base(context)
        {               
        }

        public async Task<IEnumerable<Aluno>> FiltrarAsync(bool? ativo, int? planoId)
        {
            var query = _context.Alunos!.Include(a => a.Planos).AsNoTracking().AsQueryable();

            if (ativo.HasValue)
                query = query.Where(a => a.Ativo == ativo.Value);

            if (planoId.HasValue)
                query = query.Where(a => a.PlanoId == planoId.Value);

            return await query.ToListAsync();
        }

        public async Task<(IEnumerable<Aluno> Itens, int Total)> ObterPaginadoAsync(int pagina, int itensPorPagina)
        {
            var query = _context.Alunos!.Include(a => a.Planos).AsNoTracking();

            var itens = await query.Skip((pagina - 1) * itensPorPagina).Take(itensPorPagina).ToListAsync();

            var total = await query.CountAsync();

            return (itens, total);
        }

        public async Task<Aluno?> ObterPorIdComPlanoAsync(int id) => await _context.Alunos!.Include(a => a.Planos).AsNoTracking().FirstOrDefaultAsync(a => a.AlunoId == id);

        public async Task<IEnumerable<Aluno>> ObterTodosComPlanoAsync() => await _context.Alunos!.Include(a => a.Planos).AsNoTracking().ToListAsync();
    }
}
