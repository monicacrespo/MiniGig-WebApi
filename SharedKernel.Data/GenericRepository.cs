namespace DisconnectedGenericRepository
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Threading.Tasks;
    using MiniGigWebApi.Domain;
    using MiniGigWebApi.SharedKernel.Data;

    public enum OrderByType
    {
        Ascending,
        Descending
    }

    public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : BaseEntity
    {
        protected readonly DbContext context;
        protected readonly DbSet<TEntity> dbSet;

        public GenericRepository(DbContext context)
        {
            this.context = context;
            this.dbSet = this.context.Set<TEntity>();
        }

        public async Task<TEntity> FindByKeyAsync(int id)
        {
            return await this.dbSet.FindAsync(id);
        }

        public async Task<List<TEntity>> FindByIncludeAsync(
                                   Expression<Func<TEntity, bool>> predicate,
                                   params Expression<Func<TEntity, object>>[] includeProperties)
        {
            var query = this.GetAllIncluding(includeProperties);
            return await query.Where(predicate).ToListAsync();
        }

        public IEnumerable<TEntity> All()
        {
            return this.dbSet.AsNoTracking().ToList();
        }

        public IEnumerable<TEntity> AllInclude
                               (params Expression<Func<TEntity, object>>[] includeProperties)
        {
            return GetAllIncluding(includeProperties).ToList();
        }

        public void Insert(TEntity entity)
        {
            this.dbSet.Add(entity);
            this.context.SaveChanges();
        }

        public async Task InsertAsync(TEntity entity)
        {
            this.dbSet.Add(entity);
            await this.context.SaveChangesAsync();
        }

        public async Task UpdateAsync(TEntity entity)
        {
           // Error: "Attaching an entity of type 'MiniGigWebApi.Domain.Gig' failed because another entity of the same type already has the same primary key value.
           //    This can happen when using the 'Attach' method or setting the state of an entity to 'Unchanged' or 'Modified' if any entities in the graph have conflicting key values.
           //    This may be because some entities are new and have not yet received database-generated key values. 
           //    In this case use the 'Add' method or the 'Added' entity state to track the graph and then set the state of non-new entities to 'Unchanged' or 'Modified' as appropriate.

           // This scenario occurs when you receive the object from a web request and you want to save the entity. 
           //    Between the request that contains the object and the time you change the state to modify(saving code) 
           //    you may have loaded a list that contains your entity or you may have loaded a part of this entity for validation purpose. 
           //    This is the solution to handle this case in order to save this entity.
           // First verify if the entity is present inside the DbSet. If it is not null, then we detach the local entity. 
           // In all case, we set to modify the entity(applicationModel) to have this one considered by Entity Framework to be updated.

           // Start solution
           var local = this.dbSet
                         .Local
                         .FirstOrDefault(f => f.Id == entity.Id);
            if (local != null)
            {
                context.Entry(local).State = EntityState.Detached;
            }
            // End solution

            this.dbSet.Attach(entity);
            this.context.Entry(entity).State = EntityState.Modified;
            await this.context.SaveChangesAsync();
        }

        // As far as DbSet<T>.Remove it internally "queues" a delete operation that is then executed when you call SaveChanges or SaveChangesAsync.It does not "block" anything
        public async Task DeleteAsync(TEntity entity)
        {
            this.context.Set<TEntity>().Remove(entity);
            await this.context.SaveChangesAsync();
        }

        public void Delete(TEntity entity)
        {
            this.context.Set<TEntity>().Remove(entity);
            this.context.SaveChanges();
        }

        public IQueryable<TEntity> GetAllIncluding(params Expression<Func<TEntity, object>>[] includeProperties)
        {
            IQueryable<TEntity> queryable = this.dbSet.AsNoTracking();
            return includeProperties.Aggregate (queryable, (current, includeProperty) => current.Include(includeProperty));
        }
    }
}