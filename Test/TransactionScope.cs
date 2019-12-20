using Libraries.DataAcces.Core.DataAccess.ADO.Net;
using System;
using System.Collections.Generic;
using System.Text;

namespace Test
{
    public class TransactionScope : ADOEntityRepositioryBase
    {
        public TransactionScope(string connectionString, bool openTransaction) : base(connectionString, openTransaction)
        {

        }
    }
}
