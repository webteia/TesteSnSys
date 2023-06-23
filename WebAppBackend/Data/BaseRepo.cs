using WebAppBackend.Context;
using WebAppBackend.Entidades;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace WebAppBackend.Data
{
    public class BaseRepo<T> : IDisposable, IBaseRepository<T> where T : BaseEntidade
    {
        protected SNSYSContext Db = new SNSYSContext();

        protected readonly IBaseRepository<T> _baseRepository;
        public BaseRepo(IBaseRepository<T> baseRepository)
        {
            _baseRepository = baseRepository;
        }

        public BaseRepo() { }

        public void Dispose()
        {
            Db.Dispose();
            GC.SuppressFinalize(this);
        }

        public void SetDbContext<TContext>(TContext context)
        {
            Db = context as SNSYSContext;
        }

        public T Obter(Expression<Func<T, bool>> where, params Expression<Func<T, object>>[] includes)
        {
            T retorno = null;

            IQueryable<T> query = Db.Set<T>().AsNoTracking();
            if(includes != null)
            {
                foreach(Expression<Func<T, object>> include in includes)
                {
                    query = query.Include(include);
                }

                retorno = query.FirstOrDefault(where);
            }

            return retorno;
        }

        public async Task<T> AdicionarAsync(T obj)
        {
            obj.DataCadastro = DateTime.Now;
            await Db.Set<T>().AddAsync(obj);
            await Db.SaveChangesAsync();

            return obj;
        }

        public async Task<T> EditarAsync(T obj)
        {
            var item = Db.Entry(obj);
            item.State = EntityState.Modified;

            Db.Set<T>().Update(obj);
            await Db.SaveChangesAsync();

            return obj;
        }

        public async Task<IEnumerable<T>> Listar(Expression<Func<T, bool>> where = null, params Expression<Func<T, object>>[] includes)
        {
            List<T> retorno = null;

            IQueryable<T> query = Db.Set<T>().AsNoTracking();
            if (includes != null)
            {
                foreach (Expression<Func<T, object>> include in includes)
                {
                    query = query.Include(include);
                }

                retorno = await query.Where(where).ToListAsync();
            }

            return retorno;
        }
    }
}
