using HangMan.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace HangMan.Data
{
    public class AppDbContext : IdentityDbContext<PlayerModel>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<WordModel> Words { get; set; }

        public DbSet<CategoryModel> Categories { get; set; }

        public DbSet<PlayerPreferenceModel> PlayerPreferences { get; set; }

    }
}
