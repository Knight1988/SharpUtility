using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SharpUtility.Net
{
    public class FormData : Dictionary<string, string>
    {
        public override string ToString()
        {
            var values = this.Select(p => p.Key + "=" + HttpUtility.UrlEncode(p.Value));
            return string.Join("&", values);
        }
    }
}
