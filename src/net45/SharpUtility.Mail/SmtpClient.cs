using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using SharpUtility.Core;

namespace SharpUtility.Mail
{
    public class SmtpClient : System.Net.Mail.SmtpClient
    {
        // Summary:
        //     Initializes a new instance of the System.Net.Mail.SmtpClient class by using configuration
        //     file settings.
        public SmtpClient()
        {
            
        }
        
        //
        // Summary:
        //     Initializes a new instance of the System.Net.Mail.SmtpClient class that sends
        //     e-mail by using the specified SMTP server.
        //
        // Parameters:
        //   host:
        //     A System.String that contains the name or IP address of the host computer used
        //     for SMTP transactions.
        public SmtpClient(string host) : base(host)
        {
            
        }

        //
        // Summary:
        //     Initializes a new instance of the System.Net.Mail.SmtpClient class that sends
        //     e-mail by using the specified SMTP server and port.
        //
        // Parameters:
        //   host:
        //     A System.String that contains the name or IP address of the host used for SMTP
        //     transactions.
        //
        //   port:
        //     An System.Int32 greater than zero that contains the port to be used on host.
        //
        // Exceptions:
        //   T:System.ArgumentOutOfRangeException:
        //     port cannot be less than zero.
        public SmtpClient(string host, int port) : base(host, port)
        {
            
        }

        /// <summary>
        /// Email sent rate per sec
        /// </summary>
        public int MaxSendRate { get; set; } = 1;

        // Summary:
        //     Sends the specified message to an SMTP server for delivery as an asynchronous
        //     operation.
        //
        // Parameters:
        //   message:
        //     A System.Net.Mail.MailMessage that contains the message to send.
        //
        // Returns:
        //     Returns System.Threading.Tasks.Task.The task object representing the asynchronous
        //     operation.
        //
        // Exceptions:
        //   T:System.ArgumentNullException:
        //     message is null.
        public async Task SendBulkMailAsync(IEnumerable<MailMessage> messages)
        {
            var mailSent = 0;
            var sendRateTask = Task.Delay(0);
            foreach (var mailMessage in messages)
            {
                if (mailSent >= MaxSendRate)
                {
                    await sendRateTask;
                    sendRateTask = Task.Delay(1000);
                }

                await SendMailAsync(mailMessage);
                mailSent++;
            }
        }

        // Summary:
        //     Sends the specified message to an SMTP server for delivery as an asynchronous
        //     operation. . The message sender, recipients, subject, and message body are specified
        //     using System.String objects.
        //
        // Parameters:
        //   from:
        //     A System.String that contains the address information of the message sender.
        //
        //   recipients:
        //     A System.String that contains the addresses that the message is sent to.
        //
        //   subject:
        //     A System.String that contains the subject line for the message.
        //
        //   body:
        //     A System.String that contains the message body.
        //
        // Returns:
        //     Returns System.Threading.Tasks.Task.The task object representing the asynchronous
        //     operation.
        //
        // Exceptions:
        //   T:System.ArgumentNullException:
        //     from is null.-or-recipients is null.
        //
        //   T:System.ArgumentException:
        //     from is System.String.Empty.-or-recipients is System.String.Empty.
        public async Task SendBulkMailAsync(string from, IEnumerable<string> recipients, string subject, string body)
        {
            var mailMessages = recipients.Select(p => new MailMessage(from, p, subject, body));
            await SendBulkMailAsync(mailMessages);
        }

        public event EventHandler<SmtpProgressArgs> ProgressChanged;

        protected virtual void OnProgressChanged(MailMessage mailMessage, int index, int count)
        {
            OnProgressChanged(new SmtpProgressArgs()
            {
                MailMessage = mailMessage,
                Index = index,
                Count = count
            });
        }

        protected virtual void OnProgressChanged(SmtpProgressArgs e)
        {
            ProgressChanged?.Invoke(this, e);
        }
    }

    public class SmtpProgressArgs : EventArgs
    {
        public MailMessage MailMessage { get; set; }
        public int Index { get; set; }
        public int Count { get; set; }
    }
}
