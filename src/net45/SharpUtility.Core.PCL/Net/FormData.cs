using System.Collections.Generic;
using System.Linq;
using System.Net;

namespace SharpUtility.Net
{
    public class FormData : Dictionary<string, string>
    {
        public override string ToString()
        {
            var values = this.Select(p => p.Key + "=" + WebUtility.UrlEncode(p.Value));
            return string.Join("&", values);
        }
    }
}
