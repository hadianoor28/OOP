using System;

namespace DMS.Donation
{
    public class DonationModel
    {
        // Donation Information
        public int DonationID;
        public int DonorID;
        public decimal Amount;
        public string DonationType;
        public DateTime DonationDate;

        // Default Constructor
        public DonationModel()
        {
        }

        // Parameterized Constructor
        public DonationModel(int donationId, int donorId,decimal amount, string type, DateTime date)
        {
            DonationID = donationId;
            DonorID = donorId;
            Amount = amount;
            DonationType = type;
            DonationDate = date;
        }
    }
}
