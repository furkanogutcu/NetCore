using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace Core.DataAccess.EntityFramework
{
    public class EfEntityRepositoryBase<TEntity, TContext> : IEntityRepository<TEntity>
        where TEntity : class, IEntity, new()
        where TContext : DbContext, new()
    {
        /// <summary>
        /// This method fetches all entities by including them.
        /// To use this method, you must enable AutoInclude() for the entity you are calling this method on in the OnModelCreating() method in your Context class.
        /// </summary>
        public List<TEntity> GetAllByInclude(Expression<Func<TEntity, bool>> filter = null)
        {
            using (TContext context = new TContext())
            {
                if (typeof(ISoftDeleteEntity).IsAssignableFrom(typeof(TEntity)))
                    return filter == null
                        ? context.Set<TEntity>()
                            .Where(t => ((ISoftDeleteEntity)t).DeletedAt == null)
                            .ToList()
                        : context.Set<TEntity>()
                            .Where(filter)
                            .Where(t => ((ISoftDeleteEntity)t).DeletedAt == null)
                            .ToList();

                return filter == null
                    ? context.Set<TEntity>().ToList()
                    : context.Set<TEntity>().Where(filter).ToList();
            }
        }

        /// <summary>
        /// This method fetches the filtered entity by including it.
        /// To use this method, you must enable AutoInclude() for the entity you are calling this method on in the OnModelCreating() method in your Context class.
        /// </summary>
        public TEntity GetByInclude(Expression<Func<TEntity, bool>> filter)
        {
            using (TContext context = new TContext())
            {
                if (typeof(ISoftDeleteEntity).IsAssignableFrom(typeof(TEntity)))
                    return context.Set<TEntity>()
                        .Where(t => ((ISoftDeleteEntity)t).DeletedAt == null)
                        .SingleOrDefault(filter);

                return context.Set<TEntity>().SingleOrDefault(filter);
            }
        }

        public List<TEntity> GetAll(Expression<Func<TEntity, bool>> filter = null)
        {
            using (TContext context = new TContext())
            {
                if (typeof(ISoftDeleteEntity).IsAssignableFrom(typeof(TEntity)))
                    return filter == null
                        ? context.Set<TEntity>()
                            .Where(t => ((ISoftDeleteEntity)t).DeletedAt == null)
                            .IgnoreAutoIncludes()
                            .ToList()
                        : context.Set<TEntity>()
                            .Where(filter)
                            .Where(t => ((ISoftDeleteEntity)t).DeletedAt == null)
                            .IgnoreAutoIncludes()
                            .ToList();

                return filter == null
                    ? context.Set<TEntity>().IgnoreAutoIncludes().ToList()
                    : context.Set<TEntity>().Where(filter).IgnoreAutoIncludes().ToList();
            }
        }

        public TEntity Get(Expression<Func<TEntity, bool>> filter)
        {
            using (TContext context = new TContext())
            {
                if (typeof(ISoftDeleteEntity).IsAssignableFrom(typeof(TEntity)))
                    return context.Set<TEntity>()
                        .Where(t => ((ISoftDeleteEntity)t).DeletedAt == null)
                        .IgnoreAutoIncludes()
                        .SingleOrDefault(filter);

                return context.Set<TEntity>().IgnoreAutoIncludes().SingleOrDefault(filter);
            }
        }

        public void Add(TEntity entity)
        {
            using (TContext context = new TContext())
            {
                if (entity is IAuditedEntity auditedEntity) auditedEntity.CreatedAt = DateTime.UtcNow;
                var entityToAdd = context.Entry(entity);
                entityToAdd.State = EntityState.Added;
                context.SaveChanges();
            }
        }

        public void Update(TEntity entity)
        {
            using (TContext context = new TContext())
            {
                if (entity is IAuditedEntity auditedEntity) auditedEntity.ModifiedAt = DateTime.UtcNow;
                var entityToUpdate = context.Entry(entity);
                entityToUpdate.State = EntityState.Modified;
                context.SaveChanges();
            }
        }

        public void Delete(TEntity entity)
        {
            using (TContext context = new TContext())
            {
                var entityToDelete = context.Entry(entity);
                if (entity is ISoftDeleteEntity softDeleteEntity)
                {
                    softDeleteEntity.DeletedAt = DateTime.UtcNow;
                    entityToDelete.State = EntityState.Modified;
                }
                else
                {
                    entityToDelete.State = EntityState.Deleted;
                }

                context.SaveChanges();
            }
        }
    }
}
