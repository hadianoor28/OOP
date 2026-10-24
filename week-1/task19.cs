using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace task19
{
    internal class Program
    {
        static void SignUp()
        {
            Console.Write("Enter username: ");
            string username = Console.ReadLine();

            Console.Write("Enter password: ");
            string password = Console.ReadLine();

            StreamWriter file = new StreamWriter("users.txt", true);

            file.WriteLine(username + "," + password);

            file.Close();

            Console.WriteLine("Sign Up Successful!");
        }
        static void SignIn()
        {
            Console.Write("Enter username: ");
            string username = Console.ReadLine();

            Console.Write("Enter password: ");
            string password = Console.ReadLine();

            StreamReader file = new StreamReader("users.txt");

            string line;
            bool found = false;

            while ((line = file.ReadLine()) != null)
            {
                string[] data = line.Split(',');

                if (data[0] == username && data[1] == password)
                {
                    found = true;
                    break;
                }
            }
            file.Close();

            if (found)
                Console.WriteLine("Login Successful!");
            else
                Console.WriteLine("Invalid username or password.");
        }
        static void Main(string[] args)
        {
            Console.WriteLine("1. Sign Up");
            Console.WriteLine("2. Sign In");

            Console.Write("Enter choice: ");
            int choice = int.Parse(Console.ReadLine());

            if (choice == 1)
            {
                SignUp();
            }
            else if (choice == 2)
            {
                SignIn();
            }
            else
            {
                Console.WriteLine("Invalid choice");
            }

        }
    }
}
