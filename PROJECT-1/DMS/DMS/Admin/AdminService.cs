using System;

namespace DMS.Admin
{
    public class AdminService
    {
        public AdminRepository repository;

        // Constructor
        public AdminService()
        {
            repository = new AdminRepository();
        }

        // Check Username
        public bool CheckUsername(string username)
        {
            if (username == null)
            {
                return false;
            }

            if (username.Length < 4 || username.Length > 20)
            {
                return false;
            }

            for (int i = 0; i < username.Length; i++)
            {
                char ch = username[i];

                if (!char.IsLetterOrDigit(ch) && ch != '_')
                {
                    return false;
                }
            }

            return true;
        }

        // Check Password
        public bool CheckPassword(string password)
        {
            if (password == null)
            {
                return false;
            }

            if (password.Length < 6 || password.Length > 30)
            {
                return false;
            }

            for (int i = 0; i < password.Length; i++)
            {
                if (password[i] == '\n' || password[i] == '\r')
                {
                    return false;
                }
            }

            return true;
        }

        // Register Admin
        public bool RegisterAdmin(string username, string password)

        {
            // Check if admin already exists
            if (repository.AdminExists())
            {
                Console.WriteLine("Admin is already registered!");

                return false;
            }

            // Validate username
            if (!CheckUsername(username))
            {
                Console.WriteLine("Username must be 4-20 characters.");
                Console.WriteLine("Use letters, numbers or underscore.");
                return false;
            }

            // Validate password
            if (!CheckPassword(password))
            {
                Console.WriteLine("Password must be 6-30 characters.");
                return false;
            }

            // Create Admin Object
            AdminModel admin = new AdminModel( username, password);

            // Save Admin
            repository.SaveAdmin(admin);

            return true;
        }

        // Admin Login
        public bool Login(string username, string password)
        {
            // Check if admin exists
            if (!repository.AdminExists())
            {
                Console.WriteLine("No admin registered yet!");
                return false;
            }

            // Get saved admin
            AdminModel admin = repository.GetAdmin();

            // Compare username and password
            if (admin.Username == username && admin.Password == password)
            {
                return true;
            }
            else
            {
                Console.WriteLine("Incorrect username or password!");
                return false;
            }
        }
    }
}