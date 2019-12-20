using Libraries.DataAcces.Core.DataAccess.ADO.Net;
using Libraries.DataAcces.Core.Model.ADO.Net_Models;
using System;
using System.Data.SqlClient;

namespace Librarys.Cons
{
    class Program
    {
        static void Main(string[] args)
        {
            Test();
        }
        public static void Test()
        {
            using (ADOEntityRepositioryBase transaction = new ADOEntityRepositioryBase("Server=.;Database=V3;Trusted_Connection=True;"))
            {
                ExecuteQueryModel queryModel = new ExecuteQueryModel();
                
            }
        }
    }
}
