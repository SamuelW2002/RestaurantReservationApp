using Domain;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options) { }

        protected override void OnConfiguring(DbContextOptionsBuilder options){ }

        public DbSet<Customer> Customers { get; set; }
        public DbSet<Restaurant> Restaurants { get; set; }
        public DbSet<Reservations> Reservations { get; set; }

        public void ResetDatabase()
        {
            Database.EnsureDeleted();

            Database.EnsureCreated();

            Seed();
        }

        private void Seed()
        {
            Database.EnsureCreated();

            Customers.AddRange(
                new Customer ("John", "Doe"),
                new Customer ("Jane", "Doe")
            );
            
            Restaurants.AddRange(
                new Restaurant("Wong Noodle Restaurant", "Chinese"),
                new Restaurant("Mama Mia Pizzaria", "Italian")
            );

            SaveChanges();
        }
    }
}
