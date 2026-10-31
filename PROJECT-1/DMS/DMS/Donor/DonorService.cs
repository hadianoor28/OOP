using System;
using System.Collections.Generic;

namespace DMS.Donor
{
    public class DonorService
    {
        public DonorRepository repository;

        // Constructor
        public DonorService()
        {
            repository = new DonorRepository();
        }

        // Check Donor Name
        public bool CheckName(string name)
        {
            if (name == null || name.Trim() == "")
                return false;

            for (int i = 0; i < name.Length; i++)
            {
                char ch = name[i];

                if (!char.IsLetter(ch) && ch != ' ')
                    return false;
            }

            return true;
        }

        // Check Donor Age
        public bool CheckAge(int age)
        {
            if (age >= 18 && age <= 65)
            {
                return true;
            }
            return false;
        }

        // Check Contact Number
        public bool CheckContact(string contact)
        {
            if (contact == null || contact.Length != 11)
            { 
                return false;
            }

            if (contact[0] != '0' || contact[1] != '3')
            {
                return false;
            }

            for (int i = 0; i < contact.Length; i++)
            {
                if (contact[i] < '0' || contact[i] > '9')
                    return false;
            }

            return true;
        }

        // Check Blood Group
        public bool CheckBloodGroup(string blood)
        {
            if (blood == "A+" || blood == "A-" || blood == "B+" || blood == "B-" ||blood == "AB+" || blood == "AB-" ||blood == "O+" || blood == "O-")
            {
                return true;
            }

            return false;
        }

        // Check All Donor Information
        public bool CheckDonorData(string name, int age, string blood, string contact)
        {
            if (!CheckName(name))
            {
                Console.WriteLine("Invalid name! Use letters only.");
                return false;
            }

            if (!CheckAge(age))
            {
                Console.WriteLine("Age must be between 18 and 65.");
                return false;
            }

            if (!CheckBloodGroup(blood))
            {
                Console.WriteLine("Invalid blood group!");
                return false;
            }

            if (!CheckContact(contact))
            {
                Console.WriteLine("Enter a valid 11-digit contact.");
                return false;
            }

            return true;
        }

        // Register New Donor
        public bool RegisterDonor(string name, int age,string blood, string contact)
        {
            if (!CheckDonorData(name, age, blood, contact))
            {
                return false;
            }
                
            // Check duplicate contact
            if (repository.ContactExists(contact, 0))
            {
                Console.WriteLine("Contact already registered!");
                return false;
            }

            // Generate unique ID
            int newId = repository.GetNextDonorId();

            DonorModel donor = new DonorModel(newId, name.Trim(), age, blood, contact);

            repository.AddDonor(donor);

            Console.WriteLine("Your Donor ID is: " + newId);
            return true;
        }

        // View All Donors
        public List<DonorModel> GetAllDonors()
        {
            return repository.GetAllDonors();
        }

        // Search Donor By ID
        public DonorModel SearchDonor(int id)
        {
            foreach (DonorModel donor in repository.donors)
            {
                if (donor.DonorID == id)
                {
                    return donor;
                }
            }

            return null;
        }

        // Update Donor
        public bool UpdateDonor(int id, string name,int age, string blood, string contact)
        {
            DonorModel donor = SearchDonor(id);

            if (donor == null)
            {
                Console.WriteLine("Donor not found!");
                return false;
            }

            if (!CheckDonorData(name, age, blood, contact))
                return false;

            if (repository.ContactExists(contact, id))
            {
                Console.WriteLine("Contact belongs to another donor!");
                return false;
            }

            donor.Name = name.Trim();
            donor.Age = age;
            donor.BloodGroup = blood;
            donor.Contact = contact;

            repository.SaveDonors();

            return true;
        }

        // Delete Donor
        public bool DeleteDonor(int id)
        {
            DonorModel donor = SearchDonor(id);

            if (donor == null)
            {
                return false;
            }

            repository.DeleteDonor(id);
            return true;
        }

        // Sort Donors By Age
        public List<DonorModel> SortDonorsByAge()
        {
            List<DonorModel> sorted = new List<DonorModel>(repository.donors);

            for (int i = 0; i < sorted.Count; i++)
            {
                for (int j = i + 1; j < sorted.Count; j++)
                {
                    if (sorted[i].Age > sorted[j].Age)
                    {
                        DonorModel temp = sorted[i];
                        sorted[i] = sorted[j];
                        sorted[j] = temp;
                    }
                }
            }

            return sorted;
        }
    }
}