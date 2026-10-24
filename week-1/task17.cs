using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace task17
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string path = "textfile.txt";
            if (File.Exists(path))
            {
                StreamReader reader = new StreamReader(path);
                string record;
                while ((record = reader.ReadLine()) != null)
                {
                    Console.WriteLine(record);
                }
                reader.Close();
                
            }
            else
            {
                Console.WriteLine("Not Exists");
            }
        }
    }
}
