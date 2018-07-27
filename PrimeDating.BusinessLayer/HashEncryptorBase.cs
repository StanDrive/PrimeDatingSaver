using System.Security.Cryptography;
using System.Text;

namespace PrimeDating.BusinessLayer
{
    public abstract class HashEncryptorBase
    {
        public string Encrypt(HashAlgorithm algorithm, Encoding encoding, string valueToEncrypt)
        {
            var inputBytes = encoding.GetBytes(valueToEncrypt);

            var hash = algorithm.ComputeHash(inputBytes);

            var sb = new StringBuilder();

            foreach (var t in hash)
            {
                sb.Append(t.ToString("X2"));
            }

            return sb.ToString();
        }
    }
}
