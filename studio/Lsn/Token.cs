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
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lsn
{
    /// <summary>
    /// Types of tokens
    /// </summary>
    enum EToken : sbyte
    {
        /// <summary>
        /// Unknown token, not recognized.
        /// </summary>
        UNKN = -1,

        /// <summary>
        /// Link to a video or media. eg: link=youtube.com/idk
        /// </summary>
        LINK = (1 << 0),
        
        /// <summary>
        /// Attribute tokens, eg: x=21, y=22, x=31, y=32
        /// </summary>
        ATTR = (1 << 1),

        /// <summary>
        /// Interactive question / text field. eg: field=What is the 7 to the power of 2?
        /// </summary>
        QUES = (1 << 2),

        /// <summary>
        /// A note.     eg: note=Remember the CAST Rule!
        /// </summary>
        NOTE = (1 << 3),

        /// <summary>
        /// A absolute path to a video     eg: video=C:\\Users\\Teacher\\ICS3U1\\IntroToPython.mp4
        /// </summary>
        VIDEO = (1 << 4),

        /// <summary>
        /// A answer to a interactive question eg: answer=49
        /// </summary>
        ANSWER = (1 << 5),
    };

    /// <summary>
    /// Structure for a token and its important data
    /// </summary>
    public struct Token_t
    {
        /// <summary>
        /// Array of data.
        /// </summary>
        public string[] _data;
        /// <summary>
        /// Length of data.
        /// </summary>
        public int _data_L;
        /// <summary>
        /// If the token has been processed.
        /// </summary>
        public bool _data_P;
        /// <summary>
        /// Type of data
        /// </summary>
        public sbyte _data_T;

        /// <summary>
        /// Default Constructor
        /// </summary>
        public Token_t(int i = 0) 
        {
            _data = null;
            _data_L = 0;
            _data_P = false;
            _data_T = -1;
        }
        /// <summary>
        /// Processed Constructor.
        /// </summary>
        /// <param name="data">Parsed data.</param>
        /// <param name="data_T">Type of processed token.</param>
        public Token_t(string[] data, sbyte data_T) 
        {
            _data_L = data.Length;
            _data = new string[_data_L];
            Array.Copy(data, _data, _data_L);
            _data_P = true;
            _data_T = data_T;
        }
        /// <summary>
        /// Formatting out to a file.
        /// </summary>
        /// <returns>A string of the current object</returns>
        public string ToFile() 
        {
            string strOut = _data_L + "~" + _data_T;
            foreach (string _piece in _data)
                strOut += "~" + _piece;

            Console.WriteLine(strOut);

            return strOut;
        }
        /// <summary>
        /// Creates a token from the string provided.
        /// </summary>
        public void FromFile(string strIn) 
        {
            string[] szSplit = strIn.Split('~');
            _data_L = int.Parse(szSplit[0]);
            _data_T = sbyte.Parse(szSplit[1]);
            _data = new string[_data_L];
            for (int i = 0; i < _data_L; i++)
                _data[i] = szSplit[i + 2];
            _data_P = true;
        }
    }
}
