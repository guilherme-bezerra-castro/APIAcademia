namespace APIAcademia.Repositories
{
    // Interface genérica do repository para eviar repetição de código
    public interface IRepository<T> where T : class
    {
        Task<IEnumerable<T>> ObterTodosAsync();
        Task<T?> ObterPorIdAsync(int id);
        Task<T> CriarAsync(T entidade);
        Task<T?> AtualizarAsync(T entidade);
        Task RemoverAsync(T entidade);
        Task SalvarAsync();
    }
}
