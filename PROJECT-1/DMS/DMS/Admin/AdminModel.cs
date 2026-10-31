using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DMS.Admin
{
    public class AdminModel
    {
        // Admin Information
        public string Username;
        public string Password;

        // Default Constructor
        public AdminModel()
        {
        }

        // Parameterized Constructor
        public AdminModel(string username, string password)
        {
            Username = username;
            Password = password;
        }
    }
}