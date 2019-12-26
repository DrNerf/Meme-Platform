using System;
using System.Linq;
using System.Threading.Tasks;

namespace Meme_Platform.DAL
{
    public interface IRepository<TEntity> : IDisposable
        where TEntity : class
    {
        IQueryable<TEntity> Get();
        TEntity Add(TEntity entity);
        void Delete(TEntity entity);
        void Update(TEntity entity);
        Task SaveChangesAsync();
    }
}