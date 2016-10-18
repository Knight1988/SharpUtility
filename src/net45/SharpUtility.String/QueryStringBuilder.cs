using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;

namespace SharpUtility.String
{
    /// <summary>
    /// Build a query string
    /// Add key & value then call ToString to make a querystring
    /// </summary>
    public class QueryStringBuilder : Dictionary<string, object>
    {
        public override string ToString()
        {
            var array = from p in this
                select $"{Uri.EscapeDataString(p.Key)}={Uri.EscapeDataString(p.Value.ToString())}";
            return "?" + string.Join("&", array);
        }
    }
}