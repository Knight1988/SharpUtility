using System;
using System.Net;
using System.Text;

namespace SharpUtility.Net
{
    public class UTF8WebClient : WebClient
    {
        private int _timeout = 30000;
        private readonly CookieContainer _cookieContainer = new CookieContainer();
        /// <summary>
        /// Request header only, useful for check url valid without download whole content.
        /// work only with DownloadString method.
        /// </summary>
        public bool HeadOnly { get; set; }

        public int Timeout
        {
            get { return _timeout; }
            set { _timeout = value; }
        }

        public UTF8WebClient()
        {
            Encoding = Encoding.UTF8;
            Headers.Add("user-agent",
                "Mozilla/5.0 (Windows NT 6.3; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/37.0.2062.124 YaBrowser/14.10.2062.12061 Safari/537.36");
        }

        protected override WebRequest GetWebRequest(Uri uri)
        {
            WebRequest webRequest = base.GetWebRequest(uri);
            if (webRequest is HttpWebRequest)
            {
                var request = webRequest as HttpWebRequest;

                // Set web request timeout
                request.Timeout = Timeout;
                // request only header
                if (HeadOnly && webRequest.Method == "GET")
                {
                    request.Method = "HEAD";
                }

                request.CookieContainer = _cookieContainer;
            }
            
            return webRequest;
        }
    }
}