using Libraries.DataAcces.Core.DataAccess.ADO.Net;
using Libraries.DataAccess.Usage.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Libraries.DataAccess.Usage.Repositories
{
    public class TransactionScopeRepository : ADOEntityRepositioryBase<QueryModel, QueryParametersModel>
    {
        public TransactionScopeRepository(string connectionString, bool openTransaction) : base(connectionString, openTransaction)
        {
        }
    }
}
