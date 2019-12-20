using System;
using System.Collections.Generic;
using System.Text;

namespace Libraries.DataAcces.Core.Entity
{
    public interface IQueryParams
    {
        public string Name { get; set; }
        public string StringValue { get; set; }
        public int? IntegerValue { get; set; }
        public decimal? DecimalValue { get; set; }
        public DateTime? DateTimeValue { get; set; }
    }
}
