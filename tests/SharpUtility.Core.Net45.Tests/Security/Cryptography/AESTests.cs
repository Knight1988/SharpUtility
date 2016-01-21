using System.Text;
using NUnit.Framework;
namespace SharpUtility.Core.Security.Cryptography.Tests
{
    [TestFixture()]
    public class AESTests
    {
        [Test()]
        public void AES_EncryptTest()
        {
            var aes = new AES();
            var text = new StringBuilder("1234567890");
            for (int i = 0; i < 1000; i++)
            {
                text.AppendLine("1234567890");
            }
            var encrypted = aes.Encrypt(text.ToString(), "123456", "12345678");
            var decrypted = aes.Decrypt(encrypted, "123456", "12345678");
            Assert.AreEqual(text.ToString(), decrypted);
        }
    }
}
