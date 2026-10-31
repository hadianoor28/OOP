using System;
using System.IO;

namespace DMS.Admin
{
    public class AdminRepository
    {
        public AdminModel admin;
        public string fileName = "admin.txt";

        // Constructor
        public AdminRepository()
        {
            admin = null;
            LoadAdmin();
        }

        // Check if admin is registered
        public bool AdminExists()
        {
            if (admin == null)
            {
                return false;
            }

            return true;
        }

        // Save Admin Information
        public void SaveAdmin(AdminModel newAdmin)
        {
            admin = newAdmin;

            using (StreamWriter writer = new StreamWriter(fileName))
            {
                writer.WriteLine("===== ADMIN RECORD =====");
                writer.WriteLine();

                writer.WriteLine("Username: " + admin.Username);
                writer.WriteLine("Password: " + admin.Password);
            }
        }

        // Load Admin Information
        public void LoadAdmin()
        {
            if (File.Exists(fileName))
            {
                using (StreamReader reader = new StreamReader(fileName))
                {
                    string firstLine = reader.ReadLine();
                    string username = "";
                    string password = "";

                    if (firstLine == "===== ADMIN RECORD =====")
                    {
                        reader.ReadLine();

                        username = reader.ReadLine().Substring(10);
                        password = reader.ReadLine().Substring(10);
                    }
                    else
                    {
                        username = firstLine;
                        password = reader.ReadLine();
                    }

                    if (username != null && username != "" && password != null && password != "")
                    {
                        admin = new AdminModel(username, password);
                    }
                }

                // Save existing admin in readable format
                if (admin != null)
                {
                    SaveAdmin(admin);
                }
            }
        }

        // Get Admin Information
        public AdminModel GetAdmin()
        {
            return admin;
        }
    }
}