using Libraries.DataAcces.Core.Services;
using Libraries.DataAccess.Usage.Models;
using Libraries.DataAccess.Usage.Repositories;
using System;
using System.IO;

namespace Libraries.DataAccess.Usage
{
    class Program
    {
        static void Main(string[] args)
        {
            DoWithTransaction(); 
        }
        static void DoWithTransaction()
        {
            ConnectionStringService connectionStringService = new ConnectionStringService(Directory.GetCurrentDirectory(), "ApplicationConfig.json", "Main");
            using (TransactionScopeRepository tran = new TransactionScopeRepository(connectionStringService.ConnectionString, true))
            {
                QueryParametersModel queryParametersModel = new QueryParametersModel()
                {
                    Name = "@param",
                    IntegerValue = 1
                };
                QueryModel queryModel = new QueryModel()
                {
                    QueryText = "INSERT INTO dbo.tbl1 ( Col1 ) VALUES ( @param )"
                };
                queryModel.Parameters.Add(queryParametersModel);
                tran.Action(queryModel);

                queryModel.QueryText = "SELECT Col1 FROM tbl1 WHERE tbl1.Col1 = @param";


                var result = tran.Select(queryModel);

                for (int i = 0; i < queryModel.Parameters.Count; i++)
                {
                    queryModel.Parameters[i].IntegerValue = Convert.ToInt32(result[0]["Col1"]) + 1;
                }

                queryModel.QueryText = "INSERT INTO dbo.tbl2 ( Col1 ) VALUES ( @param )";

                tran.Action(queryModel);
            }
        }
    }
}
