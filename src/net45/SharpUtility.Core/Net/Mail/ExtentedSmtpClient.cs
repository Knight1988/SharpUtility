using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Threading;
using System.Threading.Tasks;
using SharpUtility.Threading;

namespace SharpUtility.Net.Mail
{
    public class ExtentedSmtpClient : SmtpClient
    {
        /// <summary>
        ///     Cancel token
        /// </summary>
        private CancellationTokenSource _cancellationTokenSource;

        /// <summary>
        ///     Max send rate
        /// </summary>
        private decimal _maxSendRate = 40;

        /// <summary>
        ///     Pause token
        /// </summary>
        private PauseTokenSource _pauseTokenSource;

        // Summary:
        //     Initializes a new instance of the System.Net.Mail.SmtpClient class by using
        //     configuration file settings.
        public ExtentedSmtpClient()
        {
        }

        //
        // Summary:
        //     Initializes a new instance of the System.Net.Mail.SmtpClient class that sends
        //     e-mail by using the specified SMTP server.
        //
        // Parameters:
        //   host:
        //     A System.String that contains the name or IP address of the host computer
        //     used for SMTP transactions.
        public ExtentedSmtpClient(string host) : base(host)
        {
        }

        //
        // Summary:
        //     Initializes a new instance of the System.Net.Mail.SmtpClient class that sends
        //     e-mail by using the specified SMTP server and port.
        //
        // Parameters:
        //   host:
        //     A System.String that contains the name or IP address of the host used for
        //     SMTP transactions.
        //
        //   port:
        //     An System.Int32 greater than zero that contains the port to be used on host.
        //
        // Exceptions:
        //   System.ArgumentOutOfRangeException:
        //     port cannot be less than zero.
        public ExtentedSmtpClient(string host, int port) : base(host, port)
        {
        }

        /// <summary>
        ///     Max send rate, default 40
        /// </summary>
        public decimal MaxSendRate
        {
            get { return _maxSendRate; }
            set { _maxSendRate = value; }
        }

        /// <summary>
        ///     Pause send mail
        /// </summary>
        public void PauseSendBulkMail()
        {
            _pauseTokenSource.Pause();
        }

        /// <summary>
        ///     Resume send mail
        /// </summary>
        public void ResumeSendBulkMail()
        {
            _pauseTokenSource.Resume();
        }

        public async Task SendBulkMailAsync(IEnumerable<MailMessage> emails)
        {
            _pauseTokenSource = new PauseTokenSource();
            _cancellationTokenSource = new CancellationTokenSource();
            var ct = _cancellationTokenSource.Token;
            var mails = emails.ToArray();
            var sentMail = 0;
            var sendDelay = Task.Delay(1000, ct);
            var sendComplete = 0;

            foreach (var mail in mails)
            {
                await SendMailAsync(mail);
                // Report finish after 
                OnSendBulkMailProgress(++sendComplete, mails.Length);

                // Check send rate
                sentMail++;
                if (sentMail >= MaxSendRate)
                {
                    await sendDelay;
                    sentMail = 0;
                    sendDelay = Task.Delay(1000, ct);
                }

                // Cancel
                if (ct.IsCancellationRequested)
                {
                    break;
                }

                // Pause
                await _pauseTokenSource.Token.WaitWhilePausedAsync();
            }
        }

        /// <summary>
        ///     Stop send mail
        /// </summary>
        public void StopSendBulkMail()
        {
            if (_cancellationTokenSource != null)
                _cancellationTokenSource.Cancel();
        }

        /// <summary>
        ///     Fire event report send bulk mail
        /// </summary>
        /// <param name="e"></param>
        protected virtual void OnSendBulkMailProgress(EmailSenderArgs e)
        {
            var handler = SendBulkMailProgress;
            if (handler != null) handler(this, e);
        }

        /// <summary>
        ///     Report send mail progress
        /// </summary>
        /// <param name="progressValue"></param>
        /// <param name="maxValue"></param>
        protected virtual void OnSendBulkMailProgress(int progressValue, int maxValue)
        {
            OnSendBulkMailProgress(new EmailSenderArgs
            {
                ProgressValue = progressValue,
                MaxValue = maxValue
            });
        }

        protected virtual void OnSendMailError()
        {
            var handler = SendMailError;
            if (handler != null) handler(this, EventArgs.Empty);
        }

        public event EventHandler SendMailError;

        /// <summary>
        ///     Report send mail progress event
        /// </summary>
        public event EventHandler<EmailSenderArgs> SendBulkMailProgress;

        public ExtentedSmtpClient Clone()
        {
            return new ExtentedSmtpClient();
        }
    }

    public class EmailSenderArgs : EventArgs
    {
        public int MaxValue { get; set; }
        public int ProgressValue { get; set; }
    }
}