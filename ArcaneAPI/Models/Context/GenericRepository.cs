using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Web;

namespace ArcaneAPI.Models.Context
{
    public class GenericRepository<TEntity> : IDisposable where TEntity : class 
    {
        internal MainContext context = new MainContext();
        internal DbSet<TEntity> dbSet;

        public GenericRepository()
        {
            dbSet = context.Set<TEntity>();
        }

        public virtual IEnumerable<TEntity> Get(
            Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            string includeProperties = "")
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
                return orderBy(query).ToList();
            }
            else
            {
                return query.ToList();
            }
        }

        public virtual IEnumerable<TEntity> GetAsNoTracking(
            Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            string includeProperties = "")
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
                return orderBy(query).AsNoTracking().ToList();
            }
            else
            {
                return query.AsNoTracking().ToList();
            }
        }

        public virtual TEntity GetByID(object id)
        {
            TEntity entity = dbSet.Find(id);
            if (entity == null)
            {
                throw new Exception("Not found");
            }
            return entity;
        }

        public virtual void Insert(TEntity entity)
        {
            dbSet.Add(entity);
        }

        public virtual void InsertRange(IEnumerable<TEntity> entities)
        {
            dbSet.AddRange(entities);
        }

        public virtual void Delete(object id)
        {
            TEntity entityToDelete = dbSet.Find(id);
            if (entityToDelete == null)
            {
                throw new Exception("Not found");
            }
            Delete(entityToDelete);
        }

        public virtual void Delete(TEntity entityToDelete)
        {
            if (context.IsDetached(entityToDelete))
            {
                dbSet.Attach(entityToDelete);
            }
            dbSet.Remove(entityToDelete);
        }

        public virtual void DeleteRange(IEnumerable<TEntity> entitiesToDelete)
        {
            dbSet.RemoveRange(entitiesToDelete);
        }

        public virtual void Update(TEntity entityToUpdate)
        {

            dbSet.Attach(entityToUpdate);
            context.MarkAsModified(entityToUpdate);
        }

        public virtual void UpdateRange(IEnumerable<TEntity> entitiesToUpdate)
        {
            foreach (var entity in entitiesToUpdate)
            {
                Update(entity);
            }
        }

        public virtual void UpdateFields(TEntity entityToUpdate, string fieldsToUpdate)
        {
            dbSet.Attach(entityToUpdate);
            foreach (var item in fieldsToUpdate.Split(','))
            {
                context.SetPropertyModifiedTrue(entityToUpdate, item.Trim());
            }
        }

        public void Dispose()
        {
            context.Dispose();
        }
    }
}