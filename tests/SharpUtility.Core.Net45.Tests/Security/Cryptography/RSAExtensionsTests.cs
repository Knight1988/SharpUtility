using System.Security.Cryptography;
using NUnit.Framework;

namespace SharpUtility.Core.Security.Cryptography.Tests
{
    [TestFixture]
    public class RSAExtensionsTests
    {
        [Test]
        public void EncryptDecryptTest()
        {
            var secret = "My secret message";
            var rsa = new RSACryptoServiceProvider(512);  // Key bits length

            var encrypted = rsa.Encrypt(secret);
            var decrypted = rsa.Decrypt(encrypted);
            Assert.AreEqual(secret, decrypted);
        }
    }
}
