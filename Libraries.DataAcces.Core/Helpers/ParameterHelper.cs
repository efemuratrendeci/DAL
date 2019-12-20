using Libraries.DataAcces.Core.Entity;
using Libraries.DataAcces.Core.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Libraries.DataAcces.Core.Helpers
{
    public class ParameterHelper
    {
        public dynamic GetValue(IQueryParams parameter)
        {
            if (!string.IsNullOrWhiteSpace(parameter.StringValue))
                return parameter.StringValue;

            if (parameter.IntegerValue != null)
                return parameter.IntegerValue;

            if (parameter.DecimalValue != null)
                return parameter.DecimalValue;

            if (parameter.DateTimeValue != null)
                return parameter.DateTimeValue;

            return null;
        }
    }
}
