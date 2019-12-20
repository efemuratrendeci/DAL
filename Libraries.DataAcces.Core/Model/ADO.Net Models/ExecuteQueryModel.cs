using System;
using System.Collections.Generic;
using System.Text;

namespace Libraries.DataAcces.Core.Model.ADO.Net_Models
{
    public class ExecuteQueryModel
    {
        public ExecuteQueryModel()
        {
            Parameters = new List<ExecuteQueryParametersModel>();
        }
        public string QueryText { get; set; }
        public List<ExecuteQueryParametersModel> Parameters { get; set; }
    }
}
