using DMS.Donor;
using System;
using System.Collections.Generic;

namespace DMS.Donation
{
    public class DonationUI
    {
        public DonationService service;

        // Constructor
        public DonationUI(DonationService donationService)
        {
            service = donationService;
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

        // Read a valid number
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

        // Pause screen
        public void Pause()
        {
            Console.WriteLine();
            Console.WriteLine("Press Enter to continue...");
            Console.ReadLine();
        }

        // Display donation details
        public void DisplayDonation(DonationModel donation)
        {
            Console.WriteLine("-------------------------");
            Console.WriteLine("Donation ID: " + donation.DonationID);
            Console.WriteLine("Donor ID: " + donation.DonorID);
            Console.WriteLine("Donation Type: " + donation.DonationType);

            if (donation.DonationType == "Cash")
            {
                Console.WriteLine("Amount (PKR): " + donation.Amount);
            }
            else
            {
                Console.WriteLine("Amount: Not Applicable");
            }

            Console.WriteLine("Date: " + donation.DonationDate);
        }

        // Record new donation
        public void RecordDonation()
        {
            Console.Clear();
            Console.WriteLine("===== RECORD DONATION =====");

            int donorId = ReadNumber("Enter Donor ID: ");

            // Check registered donor
            if (service.CheckDonor(donorId) == false)
            {
                Console.Clear();
                Console.WriteLine("===== DONATION FAILED =====");
                Console.WriteLine("Donor Not Registered!");
                Pause();
                return;
            }

            // Get donor information
            DonorModel donor = service.donorService.SearchDonor(donorId);

            Console.WriteLine();
            Console.WriteLine("Donor Name: " + donor.Name);
            Console.WriteLine("Blood Group: " + donor.BloodGroup);

            Console.WriteLine();
            Console.WriteLine("Select Donation Type:");
            Console.WriteLine("1. Cash");
            Console.WriteLine("2. Blood");

            int choice = ReadNumber("Enter Choice: ");

            string type = "";
            decimal amount = 0;

            if (choice == 1)
            {
                type = "Cash";
                amount = ReadNumber("Enter Cash Amount (PKR): ");
            }
            else if (choice == 2)
            {
                type = "Blood";
            }
            else
            {
                Console.Clear();
                Console.WriteLine("===== DONATION FAILED =====");
                Console.WriteLine("Invalid Donation Type!");
                Pause();
                return;
            }

            // Show details before confirmation
            Console.WriteLine();
            Console.WriteLine("===== CONFIRM DONATION =====");
            Console.WriteLine("Donor: " + donor.Name);
            Console.WriteLine("Blood Group: " + donor.BloodGroup);
            Console.WriteLine("Donation Type: " + type);

            if (type == "Cash")
            {
                Console.WriteLine("Amount (PKR): " + amount);
            }

            Console.Write("Confirm Donation (Y/N): ");
            string confirm = Console.ReadLine();

            if (confirm != "Y" && confirm != "y")
            {
                Console.Clear();
                Console.WriteLine("===== DONATION CANCELLED =====");
                Console.WriteLine("No donation was recorded.");
                Pause();
                return;
            }

            // Record donation after confirmation
            bool result = service.AddDonation(donorId, amount, type);

            Console.Clear();

            if (result == true)
            {
                Console.WriteLine("===== DONATION SUCCESSFUL =====");
                Console.WriteLine();

                List<DonationModel> donations =
                    service.ViewDonations();

                DonationModel savedDonation = donations[donations.Count - 1];

                DisplayDonation(savedDonation);

                Console.WriteLine();
                Console.WriteLine("Donation Recorded Successfully!");
            }
            else
            {
                Console.WriteLine("===== DONATION FAILED =====");
                Console.WriteLine("Invalid Donation Details!");
            }

            Pause();
        }

        // Show all donation records
        public void ShowDonations()
        {
            Console.Clear();
            Console.WriteLine("===== ALL DONATIONS =====");

            List<DonationModel> donations = service.ViewDonations();

            if (donations.Count == 0)
            {
                Console.WriteLine("No Donations Found!");
            }
            else
            {
                foreach (DonationModel donation in donations)
                {
                    DisplayDonation(donation);
                }
            }

            Pause();
        }

        // Show history of one donor
        public void ShowDonorHistory()
        {
            Console.Clear();
            Console.WriteLine("===== DONOR DONATION HISTORY =====");

            int donorId = ReadNumber("Enter Donor ID: ");

            List<DonationModel> history = service.GetDonorDonations(donorId);

            Console.Clear();
            Console.WriteLine("===== DONATION HISTORY =====");
            Console.WriteLine("Donor ID: " + donorId);
            Console.WriteLine();

            if (history.Count == 0)
            {
                Console.WriteLine("No Donation History Found!");
            }
            else
            {
                foreach (DonationModel donation in history)
                {
                    DisplayDonation(donation);
                }
            }

            Pause();
        }
    }
}