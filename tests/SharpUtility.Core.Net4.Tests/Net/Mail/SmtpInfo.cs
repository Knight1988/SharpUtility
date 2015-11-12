using System;
using System.Configuration;

namespace SharpUtility.Core.Tests.Net.Mail
{
    public class SmtpInfo
    {
        private static SmtpInfo _instance;
        public string Host { get; set; }
        public int Port { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public decimal MaxSendRate { get; set; }
        public decimal MaxThreads { get; set; }

        public static SmtpInfo Instance
        {
            get
            {
                return _instance ?? (_instance = new SmtpInfo
                {
                    Host = ConfigurationManager.AppSettings["Host"],
                    Port = Convert.ToInt32(ConfigurationManager.AppSettings["Port"]),
                    Username = ConfigurationManager.AppSettings["Username"],
                    Password = ConfigurationManager.AppSettings["Password"],
                    MaxSendRate = Convert.ToDecimal(ConfigurationManager.AppSettings["MaxSendRate"]),
                    MaxThreads = Convert.ToDecimal(ConfigurationManager.AppSettings["MaxThreads"])
                });
            }
        }
    }
}