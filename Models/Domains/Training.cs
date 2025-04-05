namespace My_Sport_Club.Models.Domains
{
    public class Training
    {
        public int Id { get; set; }
        public DateTime SessionDate { get; set; }
        public string Location { get; set; }
        public string DurationInMinutes { get; set; }
    }
}
