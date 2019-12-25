using Libraries.DataAcces.Core.Entity;
using Libraries.DataAcces.Core.Enums;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using static Libraries.DataAcces.Core.DataAccess.IEntityRepository;

namespace Libraries.DataAcces.Core.DataAccess.EntityFrameworkCore
{
    public class EfEntityRepositoryBase<TEntity, TContext> : IEntityRepository<TEntity>, IDisposable
        where TEntity : class, IEntity, new()
        where TContext : DbContext, new()
    {
        private DbContext _context;
        private DbContextTransaction _contextTransaction;
        public EfEntityRepositoryBase(bool openTransaction)
        {
            _context = new TContext();
            if (openTransaction)
                _contextTransaction = _context.Database.BeginTransaction();
        }

        /// <summary>
        /// Supports Add, Update, Delete functions that can define by LibraryEntityState
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="entityState"></param>
        public void Action(TEntity entity, LibraryEntityState entityState)
        {
            try
            {
                var modifyEntity = _context.Entry(entity);
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
                _context.SaveChanges();

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
        public TEntity Get(Expression<Func<TEntity, bool>> filter = null)
        {
            try
            {
                return _context.Set<TEntity>().SingleOrDefault(filter);
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
        public List<TEntity> GetList(Expression<Func<TEntity, bool>> filter = null)
        {
            try
            {
                return filter == null
                    ? _context.Set<TEntity>().ToList()
                    : _context.Set<TEntity>().Where(filter).ToList();
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }

        public void Dispose()
        {
            try
            {
                if (_contextTransaction != null)
                {
                    _contextTransaction.Commit();
                    _contextTransaction = null;
                }

                _contextTransaction.Dispose();
                _context.Dispose();
            }
            catch (Exception)
            {
                _contextTransaction.Rollback();
                throw;
            }
        }
    }
}
