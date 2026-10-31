using System;
using DMS.Donor;
using DMS.Donation;
using DMS.Admin;

namespace DMS
{
    class Program
    {
        static void Main(string[] args)
        {
            // Create Donor objects
            DonorService donorService = new DonorService();
            DonorUI donorUI = new DonorUI(donorService);

            // Create Donation objects
            DonationService donationService = new DonationService(donorService);
            DonationUI donationUI = new DonationUI(donationService);

            // Create Admin objects
            AdminService adminService = new AdminService();
            AdminUI adminUI = new AdminUI(adminService, donorUI, donationUI);

            int choice = 0;

            while (choice != 3)
            {
                Console.Clear();

                Console.WriteLine("==========================");
                Console.WriteLine(" CHARITY & DONOR MANAGEMENT");
                Console.WriteLine("         SYSTEM");
                Console.WriteLine("==========================");
                Console.WriteLine();
                Console.WriteLine("1. Admin Portal");
                Console.WriteLine("2. Donor Portal");
                Console.WriteLine("3. Exit");
                Console.WriteLine();

                Console.Write("Enter Choice: ");
                string input = Console.ReadLine();

                if (input == "1")
                {
                    choice = 1;
                }
                else if (input == "2")
                {
                    choice = 2;
                }
                else if (input == "3")
                {
                    choice = 3;
                }
                else
                {
                    choice = 0;
                }

                if (choice == 1)
                {
                    int adminChoice = 0;

                    while (adminChoice != 3)
                    {
                        Console.Clear();

                        Console.WriteLine("===== ADMIN PORTAL =====");
                        Console.WriteLine("1. Register Admin");
                        Console.WriteLine("2. Admin Login");
                        Console.WriteLine("3. Back");
                        Console.WriteLine();

                        Console.Write("Enter Choice: ");
                        string adminInput = Console.ReadLine();

                        if (adminInput == "1")
                        {
                            adminChoice = 1;
                        }
                        else if (adminInput == "2")
                        {
                            adminChoice = 2;
                        }
                        else if (adminInput == "3")
                        {
                            adminChoice = 3;
                        }
                        else
                        {
                            adminChoice = 0;
                        }

                        if (adminChoice == 1)
                        {
                            adminUI.RegisterAdmin();
                        }
                        else if (adminChoice == 2)
                        {
                            adminUI.ShowLogin();
                        }
                        else if (adminChoice == 3)
                        {
                            // Return to Main Menu
                        }
                        else
                        {
                            Console.WriteLine("Invalid Choice!");
                            adminUI.Pause();
                        }
                    }
                }
                else if (choice == 2)
                {
                    int donorChoice = 0;

                    while (donorChoice != 5)
                    {
                        Console.Clear();

                        Console.WriteLine("===== DONOR PORTAL =====");
                        Console.WriteLine("1. Register as Donor");
                        Console.WriteLine("2. Search Donor");
                        Console.WriteLine("3. View Blood Groups");
                        Console.WriteLine("4. My Donation History");
                        Console.WriteLine("5. Back");
                        Console.WriteLine();

                        Console.Write("Enter Choice: ");
                        string donorInput = Console.ReadLine();

                        if (donorInput == "1")
                        {
                            donorChoice = 1;
                        }
                        else if (donorInput == "2")
                        {
                            donorChoice = 2;
                        }
                        else if (donorInput == "3")
                        {
                            donorChoice = 3;
                        }
                        else if (donorInput == "4")
                        {
                            donorChoice = 4;
                        }
                        else if (donorInput == "5")
                        {
                            donorChoice = 5;
                        }
                        else
                        {
                            donorChoice = 0;
                        }

                        if (donorChoice == 1)
                        {
                            donorUI.RegisterDonor();
                        }
                        else if (donorChoice == 2)
                        {
                            donorUI.SearchDonor();
                        }
                        else if (donorChoice == 3)
                        {
                            donorUI.ViewBloodGroups();
                        }
                        else if (donorChoice == 4)
                        {
                            donationUI.ShowDonorHistory();
                        }
                        else if (donorChoice == 5)
                        {
                            // Return to Main Menu
                        }
                        else
                        {
                            Console.WriteLine("Invalid Choice!");
                            donorUI.Pause();
                        }
                    }
                }
                else if (choice == 3)
                {
                    Console.Clear();
                    Console.WriteLine("Thank you for using DMS!");
                }
                else
                {
                    Console.WriteLine("Invalid Choice!");
                    donorUI.Pause();
                }
            }
        }
    }
}