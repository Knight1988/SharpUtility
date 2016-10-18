using System.Threading.Tasks;
using FluentAssertions;
using NUnit.Framework;
using SharpUtility.Mail;

namespace SharpUtility.Core.Tests.Mail
{
    [TestFixture]
    public class MailPingerTests
    {
        [TestCase("wellchoice.an@gmail.com")]
        [TestCase("kemvinh@gmail.com")]
        public async Task PingTestValid(string email)
        {
            /* Arrange */

            /* Act */
            var actual = await MailPinger.PingAsync(email, "gmail-smtp-in.l.google.com");

            /* Assert */
            actual.Should().Be(PingStatus.Valid);
        }
        
        [TestCase("not.a.valid.emailaddress.1234qa@gmail.com")]
        public async Task PingTestInvalid(string email)
        {
            /* Arrange */

            /* Act */
            var actual = await MailPinger.PingAsync(email, "gmail-smtp-in.l.google.com");

            /* Assert */
            actual.Should().Be(PingStatus.Invalid);
        }
    }
}
