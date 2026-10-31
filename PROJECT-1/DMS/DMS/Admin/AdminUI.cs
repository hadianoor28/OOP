using System;
using DMS.Donor;
using DMS.Donation;

namespace DMS.Admin
{
    public class AdminUI
    {
        public AdminService service;
        public DonorUI donorUI;
        public DonationUI donationUI;

        // Constructor
        public AdminUI(AdminService adminService,DonorUI donorUI, DonationUI donationUI)
        {
            service = adminService;
            this.donorUI = donorUI;
            this.donationUI = donationUI;
        }

        // Pause screen
        public void Pause()
        {
            Console.WriteLine();
            Console.WriteLine("Press Enter to continue...");
            Console.ReadLine();
        }

        // Read menu number
        public int ReadChoice()
        {
            while (true)
            {
                Console.Write("Enter Choice: ");
                string input = Console.ReadLine();

                bool valid = true;

                if (input == null || input == "")
                {
                    valid = false;
                }
                else
                {
                    for (int i = 0; i < input.Length; i++)
                    {
                        if (input[i] < '0' || input[i] > '9')
                        {
                            valid = false;
                        }
                    }
                }

                if (valid == true && input.Length <= 2)
                {
                    return Convert.ToInt32(input);
                }

                Console.WriteLine("Invalid input! Enter numbers only.");
            }
        }

        // Register Admin
        public void RegisterAdmin()
        {
            Console.Clear();
            Console.WriteLine("===== ADMIN REGISTRATION =====");

            Console.Write("Enter Username: ");
            string username = Console.ReadLine();

            Console.Write("Enter Password: ");
            string password = Console.ReadLine();

            bool result = service.RegisterAdmin(username, password);

            if (result == true)
            {
                // Clear input screen
                Console.Clear();

                Console.WriteLine("===== REGISTRATION SUCCESSFUL =====");
                Console.WriteLine();
                Console.WriteLine("Username: " + username);
                Console.WriteLine("Admin Registered Successfully!");
            }
            else
            {
                Console.WriteLine();
                Console.WriteLine("Registration Failed!");
            }

            Pause();
        }

        // Admin Login
        public void ShowLogin()
        {
            Console.Clear();
            Console.WriteLine("===== ADMIN LOGIN =====");

            Console.Write("Enter Username: ");
            string username = Console.ReadLine();

            Console.Write("Enter Password: ");
            string password = Console.ReadLine();

            bool result = service.Login(username, password);

            if (result == true)
            {
                Console.WriteLine("Login Successful!");
                ShowDashboard();
            }
            else
            {
                Console.WriteLine("Login Failed!");
                Pause();
            }
        }

        // Admin Dashboard
        public void ShowDashboard()
        {
            int choice = 0;

            while (choice != 11)
            {
                Console.Clear();

                Console.WriteLine("===== ADMIN DASHBOARD =====");
                Console.WriteLine("1. Register Donor");
                Console.WriteLine("2. View All Donors");
                Console.WriteLine("3. Search Donor");
                Console.WriteLine("4. Update Donor");
                Console.WriteLine("5. Delete Donor");
                Console.WriteLine("6. Sort Donors by Age");
                Console.WriteLine("7. View Blood Groups");
                Console.WriteLine("8. Record Donation");
                Console.WriteLine("9. View All Donations");
                Console.WriteLine("10. Donor Donation History");
                Console.WriteLine("11. Logout");

                choice = ReadChoice();

                if (choice == 1)
                {
                    donorUI.RegisterDonor();
                }
                else if (choice == 2)
                {
                    donorUI.ViewAllDonors();
                }
                else if (choice == 3)
                {
                    donorUI.SearchDonor();
                }
                else if (choice == 4)
                {
                    donorUI.UpdateDonor();
                }
                else if (choice == 5)
                {
                    donorUI.DeleteDonor();
                }
                else if (choice == 6)
                {
                    donorUI.SortDonors();
                }
                else if (choice == 7)
                {
                    donorUI.ViewBloodGroups();
                }
                else if (choice == 8)
                {
                    donationUI.RecordDonation();
                }
                else if (choice == 9)
                {
                    donationUI.ShowDonations();
                }
                else if (choice == 10)
                {
                    donationUI.ShowDonorHistory();
                }
                else if (choice == 11)
                {
                    Console.WriteLine("Logged Out Successfully!");
                }
                else
                {
                    Console.WriteLine("Invalid Menu Choice!");
                    Pause();
                }
            }
        }
    }
}