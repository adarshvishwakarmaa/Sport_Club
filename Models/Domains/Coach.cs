namespace My_Sport_Club.Models.Domains
{
    public class Coach
    {
        public int ID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime DateOfBirth { get; set; }  //changes dateonly
        public string YearsOfExperience { get; set; }
    }
}
