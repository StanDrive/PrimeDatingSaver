using System.Security.Cryptography;
using System.Text;

namespace PrimeDating.BusinessLayer
{
    public class HashEncryptor : HashEncryptorBase
    {
        /// <summary>
        /// Encrypts the with sh a1.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        public string EncryptWithSHA1(string value)
        {
            return Encrypt(new SHA1CryptoServiceProvider(), Encoding.ASCII, value);
        }

        /// <summary>
        /// Encrypts the with m d5.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        public string EncryptWithMD5(string value)
        {
            return Encrypt(MD5.Create(), Encoding.ASCII, value);
        }
    }
}
