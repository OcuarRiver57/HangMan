using HangMan.Models;
using Microsoft.EntityFrameworkCore;

namespace HangMan.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
        public Player CurrentPlayer { get; set; }
        public DbSet<Player> Players { get; set; }
        public DbSet<Word> Words { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<PlayerPreference> PlayerPreferences { get; set; }
    }
}
