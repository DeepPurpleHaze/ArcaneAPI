using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Web;

namespace ArcaneAPI.Models.Context
{
    internal class GenericRepository<TEntity> : IDisposable where TEntity : class 
    {
        internal MainContext context = new MainContext();
        internal DbSet<TEntity> dbSet;
        private string includePropertyString { get; set; }

        internal GenericRepository()
        {
            dbSet = context.Set<TEntity>();
        }

        internal virtual IEnumerable<TEntity> Get(
            Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            string includeProperties = "", int? take = null)
        {
            IQueryable<TEntity> query = dbSet;
            if (filter == null)
            {
                query = query.Where(q => true);
            }
            else if (filter != null)
            {
                query = query.Where(filter);
            }

            foreach (var includeProperty in includeProperties.Split
                (new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
            {
                query = query.Include(includeProperty);
            }

            if (orderBy != null)
            {
                query = orderBy(query);
            }

            if(take.HasValue)
            {
                return query.Take(take.Value).ToList();
            }
            else 
            {
                return query.ToList();
            }
        }

        internal virtual IEnumerable<TEntity> GetAsNoTracking(
            Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            string includeProperties = "", int? take = null)
        {
            IQueryable<TEntity> query = dbSet;

            if (filter != null)
            {
                query = query.AsNoTracking().Where(filter);
            }

            foreach (var includeProperty in includeProperties.Split
                (new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
            {
                query = query.AsNoTracking().Include(includeProperty);
            }

            if (orderBy != null)
            {
                query = orderBy(query).AsNoTracking();
            }

            if (take.HasValue)
            {
                return query.AsNoTracking().Take(take.Value).ToList();
            }
            else
            {
                return query.AsNoTracking().ToList();
            }
        }

        internal virtual TEntity GetByID(object id)
        {
            TEntity entity = dbSet.Find(id);
            if (entity == null)
            {
                throw new Exception("Not found");
            }
            return entity;
        }

        internal virtual void Insert(TEntity entity)
        {
            dbSet.Add(entity);
        }

        internal virtual void InsertRange(IEnumerable<TEntity> entities)
        {
            dbSet.AddRange(entities);
        }

        internal virtual void Delete(object id)
        {
            TEntity entityToDelete = dbSet.Find(id);
            if (entityToDelete == null)
            {
                throw new Exception("Not found");
            }
            Delete(entityToDelete);
        }

        internal virtual void Delete(TEntity entityToDelete)
        {
            if (context.IsDetached(entityToDelete))
            {
                dbSet.Attach(entityToDelete);
            }
            dbSet.Remove(entityToDelete);
        }

        internal virtual void DeleteRange(IEnumerable<TEntity> entitiesToDelete)
        {
            foreach (var entityToDelete in entitiesToDelete)
            {
                if (context.IsDetached(entityToDelete))
                {
                    dbSet.Attach(entityToDelete);
                }
            }
            dbSet.RemoveRange(entitiesToDelete);
        }

        internal virtual void Update(TEntity entityToUpdate)
        {
            dbSet.Attach(entityToUpdate);
            context.MarkAsModified(entityToUpdate);
        }

        internal virtual void UpdateRange(IEnumerable<TEntity> entitiesToUpdate)
        {
            foreach (var entity in entitiesToUpdate)
            {
                Update(entity);
            }
        }

        internal virtual void UpdateFields(TEntity entityToUpdate, string fieldsToUpdate)
        {
            dbSet.Attach(entityToUpdate);
            foreach (var item in fieldsToUpdate.Split(','))
            {
                context.SetPropertyModifiedTrue(entityToUpdate, item.Trim());
            }
        }

        internal IEnumerable<T> SQLQuery<T>(string sql, params object[] parameters)
        {
            return context.ExecuteStoredProcedure<T>(sql, parameters);
        }

        internal virtual void SaveChanges()
        {
            context.SaveChanges();
        }

        #region IDisposable Support
        private bool disposedValue = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // dispose managed state (managed objects).
                }

                context.Dispose();

                disposedValue = true;
            }
        }

        ~GenericRepository()
        {
            Dispose(false);
        }
                
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        #endregion
    }
}