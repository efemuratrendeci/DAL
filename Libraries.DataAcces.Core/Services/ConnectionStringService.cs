using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Libraries.DataAcces.Core.Services
{
    public class ConnectionStringService
    {
        private static string _path;
        private static string _connectionStringName;
        public ConnectionStringService(string fileDirectoryPath, string fileName, string connectionStringName)
        {
            _connectionStringName = connectionStringName;
            try
            {
                _path = Path.Combine(fileDirectoryPath, fileName);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public static string ConnectionString { get { return new ConfigurationBuilder().AddJsonFile(_path).Build()["ConnectionStrings:" + _connectionStringName]; } }
        
    }
}
