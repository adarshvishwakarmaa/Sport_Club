namespace My_Sport_Club.Models.Domains
{
    public class Match
    {
        public int ID { get; set; }
        public DateTime MatchDate { get; set; }
        public string Venue { get; set; }
        public string HomeTeamScore { get; set; }
    }
}
