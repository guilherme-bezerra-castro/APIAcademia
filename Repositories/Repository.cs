using APIAcademia.Context;
using Microsoft.EntityFrameworkCore;

namespace APIAcademia.Repositories
{
    // Implementação genérica do repository para eviar repetição de código
    public class Repository<T> : IRepository<T> where T : class
    {
        protected readonly AppDbContext _context;
        protected readonly DbSet<T> _dbSet;

        public Repository(AppDbContext context) 
        {
            _context = context;
            _dbSet = _context.Set<T>();
        }

        public Task<T> AtualizarAsync(T entidade)
        {
            _context.Entry(entidade).State = EntityState.Modified;
            return Task.FromResult(entidade);
        }

        public async Task<T> CriarAsync(T entidade)
        {
            await _dbSet.AddAsync(entidade);
            return entidade;
        }

        public async Task<T?> ObterPorIdAsync(int id) => await _dbSet.FindAsync(id);

        public async Task<IEnumerable<T>> ObterTodosAsync() => await _dbSet.AsNoTracking().ToListAsync();

        public Task RemoverAsync(T entidade)
        {
            _dbSet.Remove(entidade);
            return Task.CompletedTask;
        }

        public async Task SalvarAsync() => await _context.SaveChangesAsync();
    }
}
