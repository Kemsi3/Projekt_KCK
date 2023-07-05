using BowlingAlleyAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace BowlingAlleyAPI.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseSqlServer("server=DESKTOP-BC3PM33\\SQLEXPRESS;database=BowlingAPI;trusted_connection=true;TrustServerCertificate=True;MultipleActiveResultSets=True");
        }

        public DbSet<User> Users { get; set; }

        public DbSet<Reservation> Reservations { get; set; }

        public DbSet<Alley> Alleys { get; set; }

    }
}
