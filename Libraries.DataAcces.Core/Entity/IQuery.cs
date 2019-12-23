using System;
using System.Collections.Generic;
using System.Text;

namespace Libraries.DataAcces.Core.Entity
{
    public interface IQuery<T> where T : class, IQueryParams, new()
    {
        public string QueryText { get; set; }
        public List<T> Parameters { get; set; }
    }
}
