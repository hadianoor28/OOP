using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace area
{
    internal class Shape
    {
        public void areashape(int x,int y)
        {
            if (x != 0 && y != 0)
            {
                int Area = (x * y) / 2;
                Console.Write("the area of triangle is:");
                Console.WriteLine(Area);
            }
            else
            {
                Console.WriteLine("Height and base cant be zero!");
            }
        }
        int H;
        int B;
        public Triangle(int h , int b) 
        {
            
            H = h;
            B= b;
        }
        static void Main(string[] args)
        {
            Console.Write("Enter the height of triangle: ");
            string input1 = Console.ReadLine();
            int height = int.Parse(input1);
            Console.Write("Enter the base of triangle: ");
            string input2 = Console.ReadLine();
            int Base = int.Parse(input2);
            Triangle mytriangle = new Triangle(height, Base);
            mytriangle.areatriangle(height,Base);
           



        }
    }
}
