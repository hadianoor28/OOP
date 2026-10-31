namespace DMS.Donor
{
    public class DonorModel
    {
        public int DonorID;
        public string Name;
        public int Age;
        public string BloodGroup;
        public string Contact;

        // Default Constructor
        public DonorModel()
        {
        }

        // Parameterized Constructor
        public DonorModel(int id, string name, int age, string blood, string contact)
        {
            DonorID = id;
            Name = name;
            Age = age;
            BloodGroup = blood;
            Contact = contact;
        }
    }
}