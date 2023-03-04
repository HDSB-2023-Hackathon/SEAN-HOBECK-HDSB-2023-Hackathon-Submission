using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lsn
{
    /// <summary>
    /// Class for a Xor Cryptor with a private key.
    /// </summary>
    public class XorCryptor
    {
        /// <summary>
        /// Our private key.
        /// </summary>
        private static char[] Key =
        {
            (char)0xAF, (char)0x22, (char)0x6A, (char)0xD1, (char)0x3C, (char)0x5D, (char)0xB5, (char)0xC3,
            (char)0x5E, (char)0xC7, (char)0x0B, (char)0x91, (char)0x1B, (char)0x81, (char)0x11, (char)0x2E
        };

        /// <summary>
        /// En/Decrypting a string with XOR operation.
        /// </summary>
        /// <param name="szFull">The full string.</param>
        /// <returns>The encrypted string.</returns>
        public static string CryptString(string szFull) 
        {
            char[] buffer = new char[szFull.Length];
            for (int i = 0; i < szFull.Length; i++)
                buffer[i] = (char)(Key[i % Key.Length] ^ szFull[i]);
            return new string(buffer);
        }
    }
}
