namespace MiniGigWebApi.SharedKernel.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Threading.Tasks;
    using MiniGigWebApi.Domain;

    public interface IGenericRepository<TEntity> where TEntity : BaseEntity
    {
        Task<TEntity> FindByKeyAsync(int id);

        Task<List<TEntity>> FindByIncludeAsync
                                   (Expression<Func<TEntity, bool>> predicate,
                                   params Expression<Func<TEntity, object>>[] includeProperties);

        IEnumerable<TEntity> All();

        IQueryable<TEntity> GetAllIncluding(params Expression<Func<TEntity, object>>[] includeProperties);        

        IEnumerable<TEntity> AllInclude(params Expression<Func<TEntity, object>>[] includeProperties);

        void Insert(TEntity entity);

        Task InsertAsync(TEntity entity);

        Task UpdateAsync(TEntity entity);

        Task DeleteAsync(TEntity entity);

        void Delete(TEntity entity);
    }
}