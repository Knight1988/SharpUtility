using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SharpUtility.String
{
    public class QueryStringBuilder : Dictionary<string, object>
    {
        public override string ToString()
        {
            var array = from p in this
                select $"{HttpUtility.UrlEncode(p.Key)}={HttpUtility.UrlEncode(p.Value.ToString())}";
            return "?" + string.Join("&", array);
        }
    }
}
