using System;
using System.IO;
using System.Collections.Generic;

namespace DMS.Donor
{
    public class DonorRepository
    {
        public List<DonorModel> donors;
        public string fileName = "donors.txt";
        public string idFile = "donorId.txt";

        // Constructor
        public DonorRepository()
        {
            donors = new List<DonorModel>();
            LoadDonors();
        }

        // Add New Donor
        public void AddDonor(DonorModel donor)
        {
            donors.Add(donor);
            SaveDonors();
        }

        // Get All Donors
        public List<DonorModel> GetAllDonors()
        {
            return donors;
        }

        // Generate Unique Donor ID
        public int GetNextDonorId()
        {
            int newId = 1;

            if (File.Exists(idFile))
            {
                using (StreamReader reader = new StreamReader(idFile))
                {
                    string firstLine = reader.ReadLine();

                    if (firstLine == "===== DONOR ID MANAGEMENT =====")
                    {
                        string line = reader.ReadLine();
                        newId = Convert.ToInt32(line.Substring(15));
                    }
                    else
                    {
                        // Support old number-only file
                        newId = Convert.ToInt32(firstLine);
                    }
                }
            }

            foreach (DonorModel donor in donors)
            {
                if (donor.DonorID >= newId)
                {
                    newId = donor.DonorID + 1;
                }
            }

            using (StreamWriter writer = new StreamWriter(idFile))
            {
                writer.WriteLine("===== DONOR ID MANAGEMENT =====");
                writer.WriteLine("Next Donor ID: " + (newId + 1));
            }

            return newId;
        }

        // Check Duplicate Contact
        public bool ContactExists(string contact, int id)
        {
            foreach (DonorModel donor in donors)
            {
                if (donor.Contact == contact && donor.DonorID != id)
                {
                    return true;
                }
            }

            return false;
        }

        // Save All Donors
        public void SaveDonors()
        {
            using (StreamWriter writer = new StreamWriter(fileName))
            {
                writer.WriteLine("===== DONOR RECORDS =====");
                writer.WriteLine();

                foreach (DonorModel donor in donors)
                {
                    writer.WriteLine("Donor ID: " + donor.DonorID);
                    writer.WriteLine("Name: " + donor.Name);
                    writer.WriteLine("Age: " + donor.Age);
                    writer.WriteLine("Blood Group: " + donor.BloodGroup);
                    writer.WriteLine("Contact: " + donor.Contact);
                    writer.WriteLine();
                    writer.WriteLine("--------------------------");
                    writer.WriteLine();
                }
                writer.WriteLine("===== DONOR SUMMARY =====");
                writer.WriteLine("Total Registered Donors: " + donors.Count);
                writer.WriteLine("Last Updated: " + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
            }
        }

        // Load Saved Donors
        public void LoadDonors()
        {
            if (File.Exists(fileName))
            {
                using (StreamReader reader = new StreamReader(fileName))
                {
                    string line;

                    while ((line = reader.ReadLine()) != null)
                    {
                        if (line.StartsWith("Donor ID: "))
                        {
                            int id = Convert.ToInt32(line.Substring(10));

                            string name = reader.ReadLine().Substring(6);
                            int age = Convert.ToInt32(reader.ReadLine().Substring(5));
                            string blood = reader.ReadLine().Substring(13);
                            string contact = reader.ReadLine().Substring(9);

                            DonorModel donor = new DonorModel(id, name, age, blood, contact);

                            donors.Add(donor);
                        }
                        else
                        {
                            string[] data = line.Split(',');

                            if (data.Length == 5 && data[0] != "DonorID")
                            {
                                DonorModel donor = new DonorModel(Convert.ToInt32(data[0]),data[1],Convert.ToInt32(data[2]),data[3],data[4]);

                                donors.Add(donor);
                            }
                        }
                    }
                }
            }
        }

        // Delete Donor
        public void DeleteDonor(int id)
        {
            for (int i = 0; i < donors.Count; i++)
            {
                if (donors[i].DonorID == id)
                {
                    donors.RemoveAt(i);
                    SaveDonors();
                    break;
                }
            }
        }
    }
}