using Libraries.DataAcces.Core.Entity;
using Libraries.DataAcces.Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace Libraries.DataAcces.Core.DataAccess
{
    interface IEntityRepository
    {
        public interface IEntityRepository<T>
            where T : class, IEntity, new()
        {
            T Get(Expression<Func<T, bool>> filter = null);
            List<T> GetList(Expression<Func<T, bool>> filter = null);
            void Action(T entity, LibraryEntityState entityState);
        }
    }
}
