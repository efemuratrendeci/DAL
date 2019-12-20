using System;
using System.Collections.Generic;
using System.Text;

namespace Libraries.DataAcces.Core.Entity
{
    public interface IQuery
    {
        public string QueryText { get; set; }
        public List<IQueryParams> Parameters { get; set; }
    }
}
