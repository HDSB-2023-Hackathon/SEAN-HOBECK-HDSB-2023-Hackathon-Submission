using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using lsn;

namespace test
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //System.Console.WriteLine(File.Exists("C:\\Users\\main\\Desktop\\mind-safari\\test\\files\\example1.lsn"));
            ParsedFile pFile = Parser.Parse("C:\\Users\\main\\Desktop\\mind-safari\\test\\files\\example1.lsn");
            //ParsedFile.EncryptFile(pFile, "lesson1.lsn");

            while (true) { }
        }
    }
}
