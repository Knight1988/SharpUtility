using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using NUnit.Framework;
using SharpUtility.Mail;

namespace SharpUtility.Core.Tests
{
    [TestFixture]
    public class SmtpClientTests
    {
        private const int Index = 0;
        private const int Count = 10;

        [Test]
        public async Task EventTest()
        {
            /* Arrange */
            var client = new SmtpClient("email-smtp.us-east-1.amazonaws.com", 587)
            {
                Credentials =
                    new NetworkCredential("AKIAIU3L5KJH6W777CRA", "Akjr30PTOD1Q2NyqjjkeDuUaxk4ralbrPp2VbT4FnD4u"),
                EnableSsl = true
            };
            client.ProgressChanged += ClientOnProgressChanged;
            var froms = new List<string>();

            /* Act */
            for (var i = 0; i < Count; i++)
            {
                froms.Add("SmtpTest@mt2015.com");
            }
            await client.SendBulkMailAsync("SmtpTest@mt2015.com", froms, "Test", "Test");
        }

        private void ClientOnProgressChanged(object sender, SmtpProgressArgs args)
        {
            /* Assert */
            Assert.AreEqual(Index, args.Index);
            Assert.AreEqual(Count, args.Count);
        }
    }
}