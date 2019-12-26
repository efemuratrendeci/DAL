using Libraries.DataAcces.Core.Entity;
using Libraries.DataAcces.Core.Enums;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Transactions;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using static Libraries.DataAcces.Core.DataAccess.IEntityRepository;

namespace Libraries.DataAcces.Core.DataAccess.EntityFrameworkCore
{
    public class EfEntityRepositoryBase<TEntity, TContext> : IEntityRepository<TEntity, TContext>
        where TEntity : class, IEntity, new()
        where TContext : DbContext, new()
    {

        /// <summary>
        /// Supports Add, Update, Delete functions that can define by LibraryEntityState
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="entityState"></param>
        public TEntity Action(TEntity entity, LibraryEntityState entityState, TContext context = null)
        {
            try
            {
                if (context == null)
                {
                    using (context = new TContext())
                    {
                        var modifyEntity = context.Entry(entity);
                        switch (entityState)
                        {
                            case LibraryEntityState.Add:
                                modifyEntity.State = EntityState.Added;
                                break;
                            case LibraryEntityState.Update:
                                modifyEntity.State = EntityState.Modified;
                                break;
                            case LibraryEntityState.Delete:
                                modifyEntity.State = EntityState.Deleted;
                                break;
                        }
                        return entity;
                    }
                }
                else
                {
                    var modifyEntity = context.Entry(entity);
                    switch (entityState)
                    {
                        case LibraryEntityState.Add:
                            modifyEntity.State = EntityState.Added;
                            break;
                        case LibraryEntityState.Update:
                            modifyEntity.State = EntityState.Modified;
                            break;
                        case LibraryEntityState.Delete:
                            modifyEntity.State = EntityState.Deleted;
                            break;
                    }
                    return entity;
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        /// <summary>
        /// Returns a entity object from context. Supports Linq.Expressions to give filter
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        public TEntity Get(Expression<Func<TEntity, bool>> filter = null, TContext context = null)
        {
            try
            {
                if (context == null)
                {
                    using (context = new TContext())
                    {
                        return context.Set<TEntity>().SingleOrDefault(filter);
                    }
                }
                else
                {
                    return context.Set<TEntity>().SingleOrDefault(filter);
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        /// <summary>
        /// Returns a entity collection from context. Supports Linq.Expressions to give filter
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        public List<TEntity> GetList(Expression<Func<TEntity, bool>> filter = null, TContext context = null)
        {
            try
            {
                if (context == null)
                {
                    using (context = new TContext())
                    {
                        return filter == null
                            ? context.Set<TEntity>().ToList()
                            : context.Set<TEntity>().Where(filter).ToList();
                    }
                }
                else
                {
                    return filter == null
                        ? context.Set<TEntity>().ToList()
                        : context.Set<TEntity>().Where(filter).ToList();
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }

    }
}
