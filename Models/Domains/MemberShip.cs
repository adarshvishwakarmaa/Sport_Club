namespace My_Sport_Club.Models.Domains
{
    public class MemberShip
    {
        public int Id { get; set; }
        public string MembershipType { get; set; } // e.g., Annual, Lifetime, VIP
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string Fee { get; set; }
    }
}
