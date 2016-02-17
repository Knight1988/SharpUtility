using System.Threading.Tasks;
using NUnit.Framework;
using SharpUtility.Mail;

namespace SharpUtility.Core.Tests.Mail
{
    [TestFixture]
    public class MailPingerTests
    {
        [Test]
        [TestCase("wellchoice.an@gmail.com")]
        [TestCase("kemvinh@gmail.com")]
        public async Task PingTestValid(string email)
        {
            var actual = await MailPinger.PingAsync(email, "gmail-smtp-in.l.google.com");

            Assert.AreEqual(PingStatus.Valid, actual);
        }

        [Test]
        [TestCase("not.a.valid.emailaddress.1234qa@gmail.com")]
        public async Task PingTestInvalid(string email)
        {
            var actual = await MailPinger.PingAsync(email, "gmail-smtp-in.l.google.com");

            Assert.AreEqual(PingStatus.Invalid, actual);
        }
    }
}
