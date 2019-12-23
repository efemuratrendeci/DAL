using Libraries.DataAcces.Core.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Libraries.DataAccess.Usage.Models
{
    public class QueryParametersModel : IQueryParams
    {
        public string Name { get; set; }
        public string StringValue { get; set; }
        public int? IntegerValue { get; set; }
        public decimal? DecimalValue { get; set; }
        public DateTime? DateTimeValue { get; set; }
    }
}
