using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace ArcaneAPI.Models.Context
{
    internal class ModelRepository<TEntity> : GenericRepository<TEntity> where TEntity : class
    {
        internal ModelRepository() : base() { }

        internal ModelRepository(string includePropertyString) : base()
        {
            IncludePropertyString = includePropertyString;
        }

        internal string IncludePropertyString { get; set; } = "";

        internal virtual IEnumerable<TEntity> GetWithIncludes(Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            int? take = null)
        {
            return Get(filter, orderBy, IncludePropertyString, take);
        }

        internal virtual IEnumerable<TEntity> GetWithIncludesAsNoTracking(Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            int? take = null)
        {
            return GetAsNoTracking(filter, orderBy, IncludePropertyString, take);
        }

        internal override void Insert(TEntity entity)
        {
            base.Insert(entity);
            base.SaveChanges();
        }

        internal override void InsertRange(IEnumerable<TEntity> entities)
        {
            base.InsertRange(entities);
            base.SaveChanges();
        }

        internal override void Update(TEntity entityToUpdate)
        {
            base.Update(entityToUpdate);
            base.SaveChanges();
        }

        internal override void UpdateFields(TEntity entityToUpdate, string fieldsToUpdate)
        {
            base.UpdateFields(entityToUpdate, fieldsToUpdate);
            base.SaveChanges();
        }

        internal override void UpdateRange(IEnumerable<TEntity> entitiesToUpdate)
        {
            base.UpdateRange(entitiesToUpdate);
            base.SaveChanges();
        }

        internal override void Delete(object id)
        {
            base.Delete(id);
            base.SaveChanges();
        }

        internal override void Delete(TEntity entityToDelete)
        {
            base.Delete(entityToDelete);
            base.SaveChanges();
        }

        internal override void DeleteRange(IEnumerable<TEntity> entitiesToDelete)
        {
            base.DeleteRange(entitiesToDelete);
            base.SaveChanges();
        }
    }
}