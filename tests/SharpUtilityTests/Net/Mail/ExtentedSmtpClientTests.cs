using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using SharpUtility.Core.Net.Mail;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SharpUtilityTests.Net.Mail;

namespace SharpUtility.Core.Net.Mail.Tests
{
    [TestClass()]
    public class ExtentedSmtpClientTests
    {
        [TestMethod()]
        public void SendBulkMailAsyncTest()
        {
            using (var client = new ExtentedSmtpClient(SmtpInfo.Instance.Host, SmtpInfo.Instance.Port))
            {
                // Create a network credential with your SMTP user name and password.
                client.Credentials = new NetworkCredential(SmtpInfo.Instance.Username, SmtpInfo.Instance.Password);

                // Use SSL when accessing Amazon SES. The SMTP session will begin on an unencrypted connection, and then 
                // the client will issue a STARTTLS command to upgrade to an encrypted connection using SSL.
                client.EnableSsl = true;

                var emails = new List<MailMessage>();

                for (int i = 0; i < 10; i++)
                {
                    emails.Add(new MailMessage("test@knight1988.no-ip.info", "Knight1988@gmail.com", "Test " + i, "Test"));
                }

                client.SendBulkMailAsync(emails).Wait();
            }
        }
    }
}
