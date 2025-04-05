using Microsoft.EntityFrameworkCore;
using My_Sport_Club.Models.Domains;

namespace My_Sport_Club.Models
{
    public class SportDbContext: DbContext
    {
        public SportDbContext(DbContextOptions options) : base(options)
        {
        }
        public DbSet<Register> Registers { get; set; }
        public DbSet<Login> Logins { get; set; }
        public DbSet<Sport> Sports { get; set; }
        public DbSet<Coach> Coachs { get; set; }
        public DbSet<Match> Matchs { get; set; }
        public DbSet<MemberShip> MemberShips { get; set; }
        public DbSet<Training> Trainings { get; set; }
        public DbSet<Venue> Venues { get; set; }
        public DbSet<Player> Players { get; set; }
    }
}
