using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using SharpUtility.Core.Net.Mail;
using Microsoft.VisualStudio.TestTools.UnitTesting;
namespace SharpUtility.Core.Net.Mail.Tests
{
    [TestClass()]
    public class ExtentedSmtpClientTests
    {
        [TestMethod()]
        public void SendBulkMailAsyncTest()
        {
            using (var client = new ExtentedSmtpClient())
            {
                // Create a network credential with your SMTP user name and password.
                client.Credentials = new NetworkCredential(Username, Password);

                // Use SSL when accessing Amazon SES. The SMTP session will begin on an unencrypted connection, and then 
                // the client will issue a STARTTLS command to upgrade to an encrypted connection using SSL.
                client.EnableSsl = true;

                // Send the email. 
                try
                {
                    log.Information(
                        "Attempting to send an email to {to} through the Amazon SES SMTP interface...", email.To);
                    client.Send(email.ToMailMessage());
                    log.Information("Email sent!");
                }
                catch (Exception ex)
                {
                    log.Error("The email was not sent.");
                    log.Error("Error message: " + ex.Message);
                    OnSendMailError();
                }
            }
        }
    }
}
