using CG.Commons.Util;
using Xunit;
using FluentAssertions;

namespace CG.Commons.Test.Util
{
    public class StringCypherTest
    {
        private const string Passphrase = "1F267C5E-FF0D-4C11-DG23-F0855348D2B8";
        
        [Fact]
        public void TestEncryptDecrypt()
        {
            //arrange
            const string testString = "This is a test string. It will be encrypted and decrypted.";

            //act
            var encrypted = StringCipher.Encrypt(testString, Passphrase);
            var decrypted = StringCipher.Decrypt(encrypted, Passphrase);

            //assert
            encrypted.Should().NotBeNullOrEmpty();
            decrypted.Should().NotBeNullOrEmpty().And.Should().BeEquivalentTo(testString);
        }
    }
}