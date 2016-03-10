using System.Security.Cryptography;
using NUnit.Framework;
using SharpUtility.Security.Cryptography;

namespace SharpUtility.Core.Tests.Security.Cryptography
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
