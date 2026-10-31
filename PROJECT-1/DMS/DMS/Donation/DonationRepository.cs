using System;
using System.IO;
using System.Collections.Generic;

namespace DMS.Donation
{
    public class DonationRepository
    {
        public List<DonationModel> donations;
        public string fileName = "donations.txt";
        public string idFile = "donationId.txt";

        // Constructor
        public DonationRepository()
        {
            donations = new List<DonationModel>();
            LoadDonations();
        }

        // Add New Donation
        public void AddDonation(DonationModel donation)
        {
            donations.Add(donation);
            SaveDonations();
        }

        // Get All Donations
        public List<DonationModel> GetAllDonations()
        {
            return donations;
        }

        // Check Duplicate Donation ID
        public bool DonationIdExists(int id)
        {
            foreach (DonationModel donation in donations)
            {
                if (donation.DonationID == id)
                {
                    return true;
                }
            }

            return false;
        }

        // Generate Unique Donation ID
        public int GetNextDonationId()
        {
            int newId = 1;

            if (File.Exists(idFile))
            {
                using (StreamReader reader = new StreamReader(idFile))
                {
                    string firstLine = reader.ReadLine();

                    if (firstLine == "===== DONATION ID MANAGEMENT =====")
                    {
                        string line = reader.ReadLine();
                        newId = Convert.ToInt32(line.Substring(18));
                    }
                    else
                    {
                        // Support old number-only file
                        newId = Convert.ToInt32(firstLine);
                    }
                }
            }

            foreach (DonationModel donation in donations)
            {
                if (donation.DonationID >= newId)
                {
                    newId = donation.DonationID + 1;
                }
            }

            using (StreamWriter writer = new StreamWriter(idFile))
            {
                writer.WriteLine("===== DONATION ID MANAGEMENT =====");
                writer.WriteLine("Next Donation ID: " + (newId + 1));
            }

            return newId;
        }

        // Save All Donations
        public void SaveDonations()
        {
            using (StreamWriter writer = new StreamWriter(fileName))
            {
                writer.WriteLine("===== DONATION RECORDS =====");
                writer.WriteLine();

                decimal totalCash = 0;
                int totalBlood = 0;

                foreach (DonationModel donation in donations)
                {
                    writer.WriteLine("Donation ID: " + donation.DonationID);
                    writer.WriteLine("Donor ID: " + donation.DonorID);
                    writer.WriteLine("Amount: " + donation.Amount);
                    writer.WriteLine("Donation Type: " + donation.DonationType);
                    writer.WriteLine("Donation Date: " + donation.DonationDate.ToString("yyyy-MM-dd HH:mm:ss"));

                    writer.WriteLine();
                    writer.WriteLine("--------------------------");
                    writer.WriteLine();

                    if (donation.DonationType == "Cash")
                    {
                        totalCash = totalCash + donation.Amount;
                    }
                    else if (donation.DonationType == "Blood")
                    {
                        totalBlood++;
                    }
                }

                writer.WriteLine("===== DONATION SUMMARY =====");
                writer.WriteLine("Total Donations: " + donations.Count);
                writer.WriteLine("Total Cash Collected: " + totalCash);
                writer.WriteLine("Total Blood Donations: " + totalBlood);
                writer.WriteLine("Last Updated: " + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
            }
        }

        // Load Saved Donations
        public void LoadDonations()
        {
            if (File.Exists(fileName))
            {
                using (StreamReader reader = new StreamReader(fileName))
                {
                    string line;

                    while ((line = reader.ReadLine()) != null)
                    {
                        if (line.StartsWith("Donation ID: "))
                        {
                            int donationId = Convert.ToInt32(line.Substring(13));

                            int donorId = Convert.ToInt32(reader.ReadLine().Substring(10));

                            decimal amount = Convert.ToDecimal( reader.ReadLine().Substring(8));

                            string type = reader.ReadLine().Substring(15);

                            DateTime date = Convert.ToDateTime(reader.ReadLine().Substring(15));

                            DonationModel donation = new DonationModel(donationId, donorId, amount, type, date);

                            if (!DonationIdExists(donationId))
                            {
                                donations.Add(donation);
                            }
                        }
                        else
                        {
                            string[] data = line.Split(',');

                            if (data.Length == 5 && data[0] != "DonationID")
                            {
                                int donationId = Convert.ToInt32(data[0]);

                                DonationModel donation = new DonationModel( donationId,Convert.ToInt32(data[1]),Convert.ToDecimal(data[2]),data[3],Convert.ToDateTime(data[4]) );

                                if (!DonationIdExists(donationId))
                                {
                                    donations.Add(donation);
                                }
                            }
                        }
                    }
                }
            }
        }
    }
}