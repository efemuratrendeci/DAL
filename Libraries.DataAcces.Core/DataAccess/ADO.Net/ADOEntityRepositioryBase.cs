using Libraries.DataAcces.Core.Model.ADO.Net_Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace Libraries.DataAcces.Core.DataAccess.ADO.Net
{
    public abstract class ADOEntityRepositioryBase : IDisposable
    {
        private SqlConnection _connection;
        private SqlTransaction _transaction;
        public ADOEntityRepositioryBase(string connectionString, bool openTransaction)
        {
            _connection = new SqlConnection(connectionString);
            try
            {
                _connection.Open();

                if (openTransaction)
                    _transaction = _connection.BeginTransaction(CurrentIsolationLevel());

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private IsolationLevel CurrentIsolationLevel()
        {
            DataTable dt = new DataTable();
            SqlCommand command = new SqlCommand()
            {
                CommandText = "SELECT TOP 1 transaction_isolation_level AS IsolationLevel FROM sys.dm_exec_sessions where session_id = @@SPID",
                Connection = _connection,
                CommandType = CommandType.Text
            };
            try
            {
                dt.Load(command.ExecuteReader());
                var isolationLevel = dt.AsEnumerable().ToList()[0]["IsolationLevel"].ToString();
                return isolationLevel == "0" ? IsolationLevel.Unspecified
                        : isolationLevel == "1" ? IsolationLevel.ReadUncommitted
                        : isolationLevel == "2" ? IsolationLevel.ReadCommitted
                        : isolationLevel == "3" ? IsolationLevel.RepeatableRead
                        : isolationLevel == "4" ? IsolationLevel.Serializable
                        : isolationLevel == "5" ? IsolationLevel.Snapshot
                        : IsolationLevel.ReadCommitted;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public List<DataRow> Select(ExecuteQueryModel executeQuery)
        {
            SqlCommand sqlCommand = new SqlCommand()
            {
                Connection = _connection,
                Transaction = _transaction,
                CommandText = executeQuery.QueryText,
                CommandType = CommandType.Text
            };
            foreach (var item in executeQuery.Parameters)
            {
                sqlCommand.Parameters.AddWithValue(item.Name, !string.IsNullOrWhiteSpace(item.StringValue) ? item.StringValue : string.Empty);
            }
            DataTable dt = new DataTable();
            try
            {
                dt.Load(sqlCommand.ExecuteReader());
                return dt.AsEnumerable().ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        public void Action(ExecuteQueryModel executeQuery)
        {
            SqlCommand sqlCommand = new SqlCommand()
            {
                Connection = _connection,
                Transaction = _transaction,
                CommandText = executeQuery.QueryText,
                CommandType = CommandType.Text
            };
            foreach (var item in executeQuery.Parameters)
            {
                sqlCommand.Parameters.AddWithValue(item.Name, item.StringValue != null ? SqlDbType.NVarChar
                    : item.IntegerValue != null ? SqlDbType.Int
                    : item.DecimalValue != null ? SqlDbType.Money
                    : SqlDbType.DateTime);
            }
            try
            {
                sqlCommand.ExecuteNonQuery();
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
                if (_transaction != null)
                    _transaction.Commit();

                _transaction = null;
                _connection.Close();
                _connection.Dispose();
            }
            catch (Exception ex)
            {
                try
                {
                    if (_transaction != null)
                        _transaction.Rollback();
                }
                catch (SqlException sqlEx)
                {
                    throw sqlEx;
                }
                throw ex;
            }
        }
    }
}
