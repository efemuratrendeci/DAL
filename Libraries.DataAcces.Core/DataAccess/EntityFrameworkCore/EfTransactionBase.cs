using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Text;

namespace Libraries.DataAcces.Core.DataAccess.EntityFrameworkCore
{
    public class EfTransactionBase<T> : IDisposable
        where T : DbContext, new()
    {
        public T Context;
        private IDbContextTransaction _contextTransaction;
        public EfTransactionBase()
        {
            try
            {
                Context = new T();
                Context.Database.OpenConnection();
                _contextTransaction = Context.Database.BeginTransaction();
            }
            catch (Exception)
            {
                throw;
            }
        }
        public void Dispose()
        {
            try
            {
                if (_contextTransaction != null)
                {
                    Context.SaveChanges();
                    _contextTransaction.Commit();
                    _contextTransaction.Dispose();
                }
                _contextTransaction = null;
                Context.Database.CloseConnection();
                Context.Dispose();
            }
            catch (Exception)
            {
                try
                {
                    if (_contextTransaction != null)
                        _contextTransaction.Rollback();

                }
                catch (SqlException sqlEx)
                {
                    throw sqlEx;
                }

                throw;
            }
        }
    }
}
