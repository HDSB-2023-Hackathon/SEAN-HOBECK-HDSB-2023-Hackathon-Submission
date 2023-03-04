/**
 * 
 *  @author: Sean Hobeck
 * 
 * 
 *  @date: March 4th, 2023
 * 
 **/
using System;
using System.Collections.Generic;
using System.IO;
using System.Windows;
using System.Windows.Documents;

namespace lsn
{
    public class Parser
    {
        /// <summary>
        /// Parsing a file into analyzed tokens.
        /// </summary>
        /// <param name="szFilename">The name of the file to be parsed.</param>
        /// <returns>ParsedFile Structure</returns>
        public static ParsedFile Parse(string szFilename) 
        {
            /// Checking if the file exists.
            if (!File.Exists(szFilename))
                throw new FileNotFoundException(szFilename);

            /// Creating the returned ParsedFile.
            ParsedFile sFile = new ParsedFile();

            /// Creating the List of parsed lines.
            List<string> lParsed = new List<string>();

            /// Getting the input streamreader
            using (StreamReader sInput = new StreamReader(szFilename)) 
            {
                /// If there is a next character to be read.
                while (sInput.Peek() >= 0) 
                {
                    /// Should not be null.
                    string szLine = sInput.ReadLine();
                    if (szLine == null)
                        break;
                    lParsed.Add(szLine);
                }
            }

            int iCount = 0;
            /// Looping through the parsed lines.
            foreach (string szLine in lParsed) 
            {
                /// the Split string by the equals delimiter.
                string[] szSplit = szLine.Split('=');
                sbyte sbToken = -1;

                /// Matching the starting split to the attribute.
                switch (szSplit[0])
                {
                    case ("marg_y"):
                    case ("marg_x"):
                    case ("posi_x"):
                    case ("posi_y"):
                        sbToken = (sbyte) EToken.ATTR;
                        break;

                    case ("note"):
                        sbToken = (sbyte) EToken.NOTE;
                        break;

                    case ("link"):
                        sbToken = (sbyte) EToken.LINK;
                        break;

                    case ("question"):
                    case ("field"):
                        sbToken = (sbyte) EToken.QUES;
                        break;

                    default:
                        MessageBox.Show(String.Format("Unrecognized token: " + szSplit[0]));
                        break;
                }

                /// Adding to the list and incrementing.
                try
                {
                    sFile.l_Tokens.Add(new Token_t(new string[] { szSplit[0], szSplit[1] }, sbToken));
                }
                catch (IndexOutOfRangeException except)
                {
                    MessageBox.Show(String.Format("Unrecognized token: " + szSplit[0]));
                }

                iCount++;
            }

            /// Returning the file.
            return sFile;
        }
    }
}
