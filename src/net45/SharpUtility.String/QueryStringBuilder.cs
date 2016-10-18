using System;
using System.Collections.Generic;
using System.Linq;

namespace SharpUtility.String
{
    /// <summary>
    ///     Build a query string
    ///     Add key & value then call ToString to make a querystring
    /// </summary>
    public class QueryStringBuilder : Dictionary<string, object>
    {
        public override string ToString()
        {
            var array = from p in this
                select
                $"{Uri.EscapeDataString(p.Key).Replace("%20", "+")}={Uri.EscapeDataString(p.Value.ToString()).Replace("%20", "+")}";
            return "?" + string.Join("&", array);
        }
    }
}