using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace input2
{
    internal class Task7
    {
        static void Main(string[] args)
        {
            string str;
            str = Console.ReadLine();
            Console.WriteLine("You have entered: ");
            int num = int.Parse(str);
            Console.WriteLine("The number is: ");
            Console.Write(num);
            Console.ReadKey();
        }
    }
}
