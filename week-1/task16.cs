using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace task16
{
    internal class Program
    {
        static int add(int x, int y)
        {
            return x + y;
        }

        static void Main(string[] args)
        {
            int num1;
            int num2;
            Console.Write("Enter 1st Number: ");
            num1 = int.Parse(Console.ReadLine());
            Console.Write("Enter 2nd Number: ");
            num2 = int.Parse(Console.ReadLine());
            int result = add(num1, num2);
            Console.WriteLine("Sum is {0}",result);
            Console.Read();

        }
    }
}
