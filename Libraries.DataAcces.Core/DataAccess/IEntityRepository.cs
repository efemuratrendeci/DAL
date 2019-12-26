using Libraries.DataAcces.Core.Entity;
using Libraries.DataAcces.Core.Enums;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace Libraries.DataAcces.Core.DataAccess
{
    interface IEntityRepository
    {
        public interface IEntityRepository<TEntity, TContext>
            where TEntity : class, IEntity, new()
            where TContext : DbContext, new()
        {
            TEntity Get(Expression<Func<TEntity, bool>> filter = null, TContext context = null);
            List<TEntity> GetList(Expression<Func<TEntity, bool>> filter = null, TContext context = null);
            TEntity Action(TEntity entity, LibraryEntityState entityState, TContext context = null);
        }
    }
}
