using System.Collections.Generic;
using System.Linq;
using System.Net;

namespace SharpUtility.String
{
    public class QueryStringBuilder : Dictionary<string, object>
    {
        public override string ToString()
        {
            var array = from p in this
                        select $"{WebUtility.UrlEncode(p.Key)}={WebUtility.UrlEncode(p.Value.ToString())}";
            return "?" + string.Join("&", array);
        }
    }
}
