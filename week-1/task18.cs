using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace task18
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string path = "textFile.txt";
            StreamWriter writer = new StreamWriter(path,true);
            writer.WriteLine("hello");
            writer.Flush();
            writer.Close();
            
        }
    }
}
