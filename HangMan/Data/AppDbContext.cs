using HangMan.Models;
using Microsoft.EntityFrameworkCore;

namespace CommunitySite.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Player> players { get; set; }
        public DbSet<Word> words { get; set; }
        public DbSet<Category> categories { get; set; }
        public DbSet<PlayerPreference> playerPreferences { get; set; } 
    }
}
