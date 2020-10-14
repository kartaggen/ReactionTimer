using Microsoft.EntityFrameworkCore;

namespace ReactionTimerWebsite.Data
{
    public class ReactionTimerContext : DbContext
    {
        public ReactionTimerContext(DbContextOptions<ReactionTimerContext> options)
            : base(options)
        {
        }
        public DbSet<ReactionTimerWebsite.Models.User> User { get; set; }
        public DbSet<ReactionTimerWebsite.Models.HighScore> HighScore { get; set; }
        public DbSet<ReactionTimerWebsite.Models.GameScore> GameScore { get; set; }
    }
}
