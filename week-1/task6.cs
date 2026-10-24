using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hello
{
    public class Program
    {
        public int num;
        public Program(int Num)
        {
            if (Num % 2 == 0)
            {
                Console.WriteLine("the number you entered is even");
            }
            else
            {
                Console.WriteLine("the number you entered is odd");
            }
            num = Num;
            
            
        }


        static void Main(string[] args)
        {
            
            Console.Write("Enter the Number: ");
            string input = Console.ReadLine();
            int newnum = int.Parse(input);
            Program Number = new Program(newnum);

            Console.Write("The number you stored is: ");
            Console.WriteLine(Number.num);
        }
    }
}
