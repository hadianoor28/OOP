using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace square
{
    internal class Task8
    {
        static void Main(string[] args)
        {
            float length;
            float area;

            Console.WriteLine("Enter Length: ");
            string str = Console.ReadLine();
            length = float.Parse(str);
            area = length * length;


            Console.WriteLine("The area is: ");
            Console.Write(area);
            Console.ReadKey();
        }
    }
}
