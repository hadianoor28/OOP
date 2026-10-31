using System;
using System.Collections.Generic;
using DMS.Donor;

namespace DMS.Donation
{
    public class DonationService
    {
        public DonationRepository repository;
        public DonorService donorService;

        // Constructor
        public DonationService(DonorService donorService)
        {
            repository = new DonationRepository();
            this.donorService = donorService;
        }

        // Check Donation Type
        public bool CheckDonationType(string type)
        {
            if (type == "Cash" || type == "Blood")
            {
                return true;
            }

            return false;
        }

        // Check Donation Amount
        public bool CheckAmount(decimal amount)
        {
            if (amount > 0)
            {
                return true;
            }

            return false;
        }

        // Check Registered Donor
        public bool CheckDonor(int donorId)
        {
            DonorModel donor = donorService.SearchDonor(donorId);

            if (donor == null)
            {
                return false;
            }

            return true;
        }

        // Check All Donation Information
        public bool CheckDonationData(int donorId,decimal amount, string type)
        {
            // Donor must exist
            if (!CheckDonor(donorId))
            {
                Console.WriteLine("Donor not registered!");
                return false;
            }

            // Only Cash or Blood
            if (!CheckDonationType(type))
            {
                Console.WriteLine("Invalid type! Enter Cash or Blood.");
                return false;
            }

            // Cash donation must have positive amount
            if (type == "Cash")
            {
                if (!CheckAmount(amount))
                {
                    Console.WriteLine("Cash amount must be greater than 0.");
                    return false;
                }
            }

            // Blood donation has no cash amount
            if (type == "Blood")
            {
                if (amount != 0)
                {
                    Console.WriteLine("Blood donation amount must be 0.");
                    return false;
                }
            }

            return true;
        }

        // Record New Donation
        public bool AddDonation(int donorId,decimal amount, string type)
        {
            // Validate before saving
            if (!CheckDonationData(donorId, amount, type))
            {
                return false;
            }

            // Generate unique Donation ID
            int newId = repository.GetNextDonationId();

            // Create donation object
            DonationModel donation = new DonationModel(newId,donorId,amount,type,DateTime.Now);

            // Save donation
            repository.AddDonation(donation);

            Console.WriteLine("Your Donation ID is: " + newId);

            return true;
        }

        // View All Donations
        public List<DonationModel> ViewDonations()
        {
            return repository.GetAllDonations();
        }

        // View Donation History of One Donor
        public List<DonationModel> GetDonorDonations(int donorId)
        {
            List<DonationModel> history = new List<DonationModel>();

            foreach (DonationModel donation in repository.donations)
            {
                if (donation.DonorID == donorId)
                {
                    history.Add(donation);
                }
            }

            return history;
        }
    }
}