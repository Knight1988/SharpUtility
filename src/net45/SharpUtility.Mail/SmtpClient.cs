// ***********************************************************************
// Assembly         : SharpUtility.Mail
// Author           : Squall Leonhart
// Created          : 01-11-2017
//
// Last Modified By : Squall Leonhart
// Last Modified On : 02-03-2017
// ***********************************************************************
// <copyright file="SmtpClient.cs" company="">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Threading.Tasks;

namespace SharpUtility.Mail
{
    /// <summary>
    /// Class SmtpClient.
    /// </summary>
    /// <seealso cref="System.Net.Mail.SmtpClient" />
    public class SmtpClient : System.Net.Mail.SmtpClient
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SmtpClient"/> class.
        /// </summary>
        public SmtpClient()
        {
            
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SmtpClient"/> class.
        /// </summary>
        /// <param name="host">A <see cref="T:System.String" /> that contains the name or IP address of the host computer used for SMTP transactions.</param>
        public SmtpClient(string host) : base(host)
        {
            
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SmtpClient"/> class.
        /// </summary>
        /// <param name="host">A <see cref="T:System.String" /> that contains the name or IP address of the host used for SMTP transactions.</param>
        /// <param name="port">An <see cref="T:System.Int32" /> greater than zero that contains the port to be used on <paramref name="host" />.</param>
        public SmtpClient(string host, int port) : base(host, port)
        {
            
        }

        /// <summary>
        /// Gets or sets the maximum send rate.
        /// </summary>
        /// <value>The maximum send rate.</value>
        public int MaxSendRate { get; set; } = 1;

        /// <summary>
        /// send bulk mail as an asynchronous operation.
        /// </summary>
        /// <param name="messages">The messages.</param>
        /// <returns>Task.</returns>
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

        /// <summary>
        /// send bulk mail as an asynchronous operation.
        /// </summary>
        /// <param name="from">The address of the sender.</param>
        /// <param name="recipients">The recipients of the e-mail.</param>
        /// <param name="subject">The subject.</param>
        /// <param name="body">The message body.</param>
        /// <returns>Task.</returns>
        public async Task SendBulkMailAsync(string from, IEnumerable<string> recipients, string subject, string body)
        {
            var mailMessages = recipients.Select(p => new MailMessage(from, p, subject, body));
            await SendBulkMailAsync(mailMessages);
        }

        /// <summary>
        /// Occurs when [progress changed].
        /// </summary>
        public event EventHandler<SmtpProgressArgs> ProgressChanged;

        /// <summary>
        /// Called when [progress changed].
        /// </summary>
        /// <param name="mailMessage">The mail message which has been sent.</param>
        /// <param name="index">The index of the message.</param>
        /// <param name="count">The total number email.</param>
        protected virtual void OnProgressChanged(MailMessage mailMessage, int index, int count)
        {
            OnProgressChanged(new SmtpProgressArgs()
            {
                MailMessage = mailMessage,
                Index = index,
                Count = count
            });
        }

        /// <summary>
        /// Called when [progress changed].
        /// </summary>
        /// <param name="e">The agrument.</param>
        protected virtual void OnProgressChanged(SmtpProgressArgs e)
        {
            ProgressChanged?.Invoke(this, e);
        }
    }

    /// <summary>
    /// Class SmtpProgressArgs.
    /// </summary>
    /// <seealso cref="System.EventArgs" />
    public class SmtpProgressArgs : EventArgs
    {
        /// <summary>
        /// Gets or sets the mail message.
        /// </summary>
        /// <value>The mail message.</value>
        public MailMessage MailMessage { get; set; }
        /// <summary>
        /// Gets or sets the index of message.
        /// </summary>
        /// <value>The index.</value>
        public int Index { get; set; }
        /// <summary>
        /// Gets or sets the total number of messages.
        /// </summary>
        /// <value>The count.</value>
        public int Count { get; set; }
    }
}
