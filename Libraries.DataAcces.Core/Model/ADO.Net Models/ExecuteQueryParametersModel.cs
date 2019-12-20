using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace Libraries.DataAcces.Core.Model.ADO.Net_Models
{
    public class ExecuteQueryParametersModel
    {
        public string Name { get; set; }
        public string StringValue { get; set; }
        public int? IntegerValue { get; set; }
        public decimal? DecimalValue { get; set; }
        public DateTime? DateTimeValue { get; set; }
        public SqlDbType? SqlType { get; set; }
    }
}
