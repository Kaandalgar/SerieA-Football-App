using Microsoft.EntityFrameworkCore;
using SerieA.API.Entities;

namespace SerieA.API.Context
{
    public class SerieAContext : DbContext
    {
        public SerieAContext(DbContextOptions<SerieAContext> options)
            : base(options)
        {
        }

        public DbSet<Team> Teams { get; set; }

        public DbSet<Match> Matches { get; set; }

        public DbSet<MatchGoal> MatchGoals { get; set; }

        public DbSet<MatchCard> MatchCards { get; set; }

        public DbSet<Substitution> Substitutions { get; set; }
    }
}