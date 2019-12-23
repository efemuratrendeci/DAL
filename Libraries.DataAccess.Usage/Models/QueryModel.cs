using Libraries.DataAcces.Core.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Libraries.DataAccess.Usage.Models
{
    public class QueryModel : IQuery<QueryParametersModel>
    {
        public QueryModel()
        {
            Parameters = new List<QueryParametersModel>();
        }
        public string QueryText { get ; set; }
        public List<QueryParametersModel> Parameters { get; set; }
    }
}
