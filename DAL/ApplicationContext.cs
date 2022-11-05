using Domain;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace DAL.Context
{
    public class ApplicationContext : IdentityDbContext<ApplicationUser>
    {
        public DbSet<Buyer> Buyer { get; set; }
        public DbSet<House> House { get; set; }
        public DbSet<Mortgage> Mortgage { get; set; }

        public ApplicationContext()
            : base()
        {
            Database.EnsureCreated();
        }

        // Local DB connection string for the Cosmosdb emulator
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
            => optionsBuilder.UseCosmos(
                "https://localhost:8081",
                "C2y6yDjf5/R+ob0N8A7Cgv30VRDJIWEHLM+4QDU5DE2nQ9nDuVTqobD4b8mGGyPMbIZnqyMsEcaGQy67XIw/Jw==",
                databaseName: "ByMyHouse");

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Buyer>()
                .ToContainer("Buyers")
                .HasPartitionKey(b => b.Name)
                .UseETagConcurrency()
                .HasNoDiscriminator();

            modelBuilder.Entity<House>()
                .ToContainer("Houses")
                .HasPartitionKey(h => h.ZipCode)
                .UseETagConcurrency()
                .HasNoDiscriminator();

            modelBuilder.Entity<Mortgage>()
                .ToContainer("Mortgages")
                .HasPartitionKey(m => m.PartitionKey)
                .UseETagConcurrency()
                .HasNoDiscriminator();


            modelBuilder.Entity<ApplicationUser>()
                .Property(b => b.ConcurrencyStamp)
                .IsETagConcurrency();

            modelBuilder.Entity<IdentityRole>()
                .Property(b => b.ConcurrencyStamp)
                .IsETagConcurrency();
        }
    }
}
