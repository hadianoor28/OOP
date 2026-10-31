using System;
using System.Collections.Generic;

namespace DMS.Donor
{
    public class DonorUI
    {
        public DonorService service;

        // Constructor
        public DonorUI(DonorService donorService)
        {
            service = donorService;
        }

        // Check if input contains only digits
        public bool OnlyDigits(string text)
        {
            if (text == null || text == "")
            {
                return false;
            }
            for (int i = 0; i < text.Length; i++)
            {
                if (text[i] < '0' || text[i] > '9')
                {
                    return false;
                }
            }

            return true;
        }

        // Read a valid integer
        public int ReadNumber(string message)
        {
            while (true)
            {
                Console.Write(message);
                string input = Console.ReadLine();

                if (OnlyDigits(input) && input.Length <= 8)
                {
                    return Convert.ToInt32(input);
                }

                Console.WriteLine("Invalid input! Enter numbers only.");
            }
        }

        // Wait before returning to menu
        public void Pause()
        {
            Console.WriteLine();
            Console.WriteLine("Press Enter to continue...");
            Console.ReadLine();
        }

        // Display donor details
        public void DisplayDonor(DonorModel donor)
        {
            Console.WriteLine("------------------------");
            Console.WriteLine("Donor ID: " + donor.DonorID);
            Console.WriteLine("Name: " + donor.Name);
            Console.WriteLine("Age: " + donor.Age);
            Console.WriteLine("Blood Group: " + donor.BloodGroup);
            Console.WriteLine("Contact: " + donor.Contact);
        }

        // Register a new donor
        public void RegisterDonor()
        {
            Console.Clear();
            Console.WriteLine("===== DONOR REGISTRATION =====");

            string name = "";

            // Keep asking until name is valid
            while (true)
            {
                Console.Write("Enter Name: ");
                name = Console.ReadLine();

                if (service.CheckName(name) == true)
                {
                    break;
                }
                else
                {
                    Console.WriteLine("Invalid Name! Enter letters only. Name cannot be empty.");
                }
            }

            int age = ReadNumber("Enter Age: ");

            Console.Write("Enter Blood Group: ");
            string blood = Console.ReadLine();

            Console.Write("Enter Contact: ");
            string contact = Console.ReadLine();

            bool result = service.RegisterDonor(name, age, blood, contact);

            Console.Clear();

            if (result == true)
            {
                Console.WriteLine("===== REGISTRATION SUCCESSFUL =====");
                Console.WriteLine();

                foreach (DonorModel donor in service.GetAllDonors())
                {
                    if (donor.Contact == contact)
                    {
                        Console.WriteLine("Your Donor ID is: " + donor.DonorID);
                        Console.WriteLine("Name: " + donor.Name);
                        break;
                    }
                }

                Console.WriteLine("Donor Registered Successfully!");
            }
            else
            {
                Console.WriteLine("===== REGISTRATION FAILED =====");
                Console.WriteLine();

                if (service.CheckName(name) == false)
                {
                    Console.WriteLine("Error: Invalid name! Use letters only.");
                }
                else if (service.CheckAge(age) == false)
                {
                    Console.WriteLine("Error: Age must be between 18 and 65.");
                }
                else if (service.CheckBloodGroup(blood) == false)
                {
                    Console.WriteLine("Error: Invalid blood group!");
                }
                else if (service.CheckContact(contact) == false)
                {
                    Console.WriteLine("Error: Enter a valid 11-digit contact.");
                }
                else if (service.repository.ContactExists(contact, 0))
                {
                    Console.WriteLine("Error: Contact already registered!");
                }
            }

            Pause();
        }

        // View all donors
        public void ViewAllDonors()
        {
            Console.Clear();
            Console.WriteLine("===== ALL DONORS =====");

            List<DonorModel> donors = service.GetAllDonors();

            if (donors.Count == 0)
            {
                Console.WriteLine("No Donors Registered Yet!");
            }
            else
            {
                int srNo = 1;

                foreach (DonorModel donor in donors)
                {
                    Console.WriteLine("------------------------");
                    Console.WriteLine("Sr. No: " + srNo);
                    Console.WriteLine("Donor ID: " + donor.DonorID);
                    Console.WriteLine("Name: " + donor.Name);
                    Console.WriteLine("Age: " + donor.Age);
                    Console.WriteLine("Blood Group: " + donor.BloodGroup);
                    Console.WriteLine("Contact: " + donor.Contact);

                    srNo++;
                }
            }

            Pause();
        }



        // Search donor by ID
        public void SearchDonor()
        {
            Console.Clear();
            Console.WriteLine("===== SEARCH DONOR =====");

            int id = ReadNumber("Enter Donor ID: ");

            DonorModel donor = service.SearchDonor(id);

            if (donor == null)
            {
                Console.WriteLine("Donor Not Found!");
            }
            else
            {
                DisplayDonor(donor);
            }

            Pause();
        }

        // Update donor details
        public void UpdateDonor()
        {
            Console.Clear();
            Console.WriteLine("===== UPDATE DONOR =====");

            int id = ReadNumber("Enter Donor ID: ");

            DonorModel donor = service.SearchDonor(id);

            if (donor == null)
            {
                Console.Clear();
                Console.WriteLine("===== UPDATE FAILED =====");
                Console.WriteLine("Donor Not Found!");
                Pause();
                return;
            }

            Console.WriteLine();
            Console.WriteLine("Current Donor Details:");
            DisplayDonor(donor);

            Console.WriteLine();
            Console.WriteLine("Enter New Details:");

            Console.Write("Enter New Name: ");
            string name = Console.ReadLine();

            int age = ReadNumber("Enter New Age: ");

            Console.Write("Enter New Blood Group: ");
            string blood = Console.ReadLine();

            Console.Write("Enter New Contact: ");
            string contact = Console.ReadLine();

            bool result = service.UpdateDonor(id, name, age, blood, contact);

            // Clear previous input screen
            Console.Clear();

            if (result == true)
            {
                Console.WriteLine("===== UPDATE SUCCESSFUL =====");
                Console.WriteLine();
                Console.WriteLine("Donor Updated Successfully!");
                Console.WriteLine();
                DisplayDonor(donor);
            }
            else
            {
                Console.WriteLine("===== UPDATE FAILED =====");
                Console.WriteLine();
                Console.WriteLine("Invalid details or duplicate contact!");
            }

            Pause();
        }

        // Delete donor
        public void DeleteDonor()
        {
            Console.Clear();
            Console.WriteLine("===== DELETE DONOR =====");

            int id = ReadNumber("Enter Donor ID: ");

            DonorModel donor = service.SearchDonor(id);

            if (donor == null)
            {
                Console.Clear();
                Console.WriteLine("===== DELETE FAILED =====");
                Console.WriteLine();
                Console.WriteLine("Donor Not Found!");
                Pause();
                return;
            }

            Console.WriteLine();
            Console.WriteLine("Donor Details:");
            DisplayDonor(donor);

            Console.WriteLine();
            Console.Write("Confirm Delete (Y/N): ");
            string choice = Console.ReadLine();

            if (choice == "Y" || choice == "y")
            {
                bool result = service.DeleteDonor(id);

                // Clear screen after deletion
                Console.Clear();

                if (result == true)
                {
                    Console.WriteLine("===== DELETE SUCCESSFUL =====");
                    Console.WriteLine();
                    Console.WriteLine("Donor ID: " + id);
                    Console.WriteLine("Donor Deleted Successfully!");
                }
                else
                {
                    Console.WriteLine("===== DELETE FAILED =====");
                    Console.WriteLine();
                    Console.WriteLine("Could Not Delete Donor!");
                }
            }
            else
            {
                
                Console.Clear();

                Console.WriteLine("===== DELETION CANCELLED =====");
                Console.WriteLine();
                Console.WriteLine("Donor Record Was Not Deleted.");
            }

            Pause();
        }

        // Sort donors by age
        public void SortDonors()
        {
            Console.Clear();

            List<DonorModel> donors = service.SortDonorsByAge();

            Console.WriteLine("===== DONORS SORTED BY AGE =====");
            Console.WriteLine();

            if (donors.Count == 0)
            {
                Console.WriteLine("No Donors Found!");
            }
            else
            {
                foreach (DonorModel donor in donors)
                {
                    DisplayDonor(donor);
                    Console.WriteLine();
                }
            }

            Pause();
        }

        // View registered blood groups
        public void ViewBloodGroups()
        {
            Console.Clear();
            Console.WriteLine("===== REGISTERED BLOOD GROUPS =====");

            List<DonorModel> donors = service.GetAllDonors();

            if (donors.Count == 0)
            {
                Console.WriteLine("No Donors Available!");
            }
            else
            {
                foreach (DonorModel donor in donors)
                {
                    Console.WriteLine(donor.Name + " - " + donor.BloodGroup);
                }
            }

            Pause();
        }
    }
}