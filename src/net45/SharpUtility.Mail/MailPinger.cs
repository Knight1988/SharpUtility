using System;
using System.Threading;
using System.Threading.Tasks;
using PrimS.Telnet;

namespace SharpUtility.Mail
{
    public static class MailPinger
    {
        /// <summary>
        /// Ping an email to check if email is valid
        /// </summary>
        /// <param name="address">email address to check</param>
        /// <param name="server">mx server to check</param>
        /// <param name="timeout">check timeout</param>
        /// <returns></returns>
        public static async Task<PingStatus> PingAsync(string address, string server, TimeSpan timeout)
        {
            using (var client = new Client(server, 25, new CancellationToken()))
            {
                if (!client.IsConnected) return PingStatus.NotConnected;
                await client.WriteLine("HELO here.com");
                var s = await client.TerminatedReadAsync(">", timeout);
                if (!s.Contains("250")) return PingStatus.NotConnected;
                await client.WriteLine("MAIL FROM:<me@here.com>");
                s = await client.TerminatedReadAsync(">", timeout);
                if (!s.Contains("250")) return PingStatus.NotConnected;
                await client.WriteLine($"RCPT TO:<{address}>");
                s = await client.TerminatedReadAsync(">", timeout);
                if (!s.Contains("250")) return PingStatus.Invalid;
            }
            return PingStatus.Valid;
        }

        /// <summary>
        /// Ping an email to check if email is valid
        /// </summary>
        /// <param name="address">email address to check</param>
        /// <param name="server">mx server to check</param>
        /// <param name="timeout">check timeout</param>
        /// <returns></returns>
        public static async Task<PingStatus> PingAsync(string address, string server, int timeout)
        {
            return await PingAsync(address, server, TimeSpan.FromMilliseconds(timeout));
        }

        /// <summary>
        /// Ping an email to check if email is valid
        /// </summary>
        /// <param name="address">email address to check</param>
        /// <param name="server">mx server to check</param>
        /// <returns></returns>
        public static async Task<PingStatus> PingAsync(string address, string server)
        {
            return await PingAsync(address, server, 10000);
        }
    }
}
