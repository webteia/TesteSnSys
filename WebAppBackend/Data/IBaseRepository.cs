using System.Linq.Expressions;

namespace WebAppBackend.Data
{
    public interface IBaseRepository<T> : IDisposable where T : class
    {
        void SetDbContext<TContext>(TContext context);
        T Obter(Expression<Func<T, bool>> where, params Expression<Func<T, object>>[] includes);
        Task<T> AdicionarAsync(T obj);
        Task<T> EditarAsync(T obj);
        Task<IEnumerable<T>> Listar(Expression<Func<T, bool>> where = null, params Expression<Func<T, object>>[] includes);
    }
}
