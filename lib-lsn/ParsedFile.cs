/**
 * 
 *  @author: Sean Hobeck
 * 
 * 
 *  @date: March 3rd, 2023
 * 
 **/

using System;
using System.Collections.Generic;
using System.Formats.Asn1;
using System.IO;
using System.Security.Cryptography;
using System.Threading;

namespace lsn
{
    /// <summary>
    /// Structure for a Parsed (.lsn) file
    /// </summary>
    public class ParsedFile
    {
        /// <summary>
        /// Tokens in the file.
        /// </summary>
        public List<Token_t> l_Tokens { get; set; }

        /// <summary>
        /// Default Constructor.
        /// </summary>
        public ParsedFile() 
        {
            l_Tokens = new List<Token_t>();
        }
        /// <summary>
        /// List Constructor.
        /// </summary>
        /// <param name="l_Tokens">List of tokens to be inputted.</param>
        public ParsedFile(List<Token_t> l_Tokens)
        {
            this.l_Tokens = l_Tokens;
        }

        /// <summary>
        /// Encrypting a file with a XOR Key.
        /// </summary>
        /// <param name="szOutput">The output filename.</param>
        public static void EncryptFile(ParsedFile pFile, string szOutput) 
        {
            /// Getting the output filestream (creating the file.)
            using (FileStream fOutput = new FileStream(szOutput, FileMode.OpenOrCreate)) 
            {
                /// Generating our own encryption.
                using (StreamWriter sWriter = new StreamWriter(fOutput)) 
                {
                    foreach (Token_t _token in pFile.l_Tokens)
                        sWriter.WriteLine(XorCryptor.CryptString(_token.ToFile()));
                }
            }
        }
        /// <summary>
        /// Decrypting a file based on our private key.
        /// </summary>
        /// <param name="pFile">Reference to the output file structure.</param>
        /// <param name="szFilename">Filename of the input file.</param>
        public static void DecryptFile(ref ParsedFile pFile, string szFilename) 
        {
            /// Getting the input filestream (opening the file.)
            using (FileStream fInput = new FileStream(szFilename, FileMode.Open))
            {
                /// Creating our decrypted list.
                List<string> lDecrypted = new List<string>();

                using (StreamReader sReader = new StreamReader(fInput))
                {
                    /// If the next character is readable (basically if we can read another line.)
                    while (sReader.Peek() >= 0)
                    {
                        string szLine = sReader.ReadLine();
                        if (szLine == null)
                            break;
                        lDecrypted.Add(XorCryptor.CryptString(szLine));
                    }
                }

                /// Preparing the referenced file for more.
                pFile.l_Tokens = new List<Token_t>();

                /// Looping through our decrypted strings.
                foreach (string szLine in lDecrypted)
                {
                    /// Creating a temporary token then addding it to our token list on the referenced file.
                    Token_t tAdd = new Token_t();
                    tAdd.FromFile(szLine);
                    pFile.l_Tokens.Add(tAdd);
                }
            }
        }
    }
}
